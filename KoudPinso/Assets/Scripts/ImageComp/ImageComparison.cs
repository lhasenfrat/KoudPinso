/* Compare the submitted drawing with a reference image and calculate a score*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Math;
using System;
using UnityEngine.UI;
using System.IO;

public class ImageComparison : MonoBehaviour
{

    //public GameObject imageConteneur; 
    public GameObject UIScore;

    /*The methods below are for the progression bar*/


    // Update is called once per frame
    void Update()
    {  
    }



    /*The methods below are for the image comparison and calculate a score*/

    public void calculScore(){
        
        StartCoroutine(comparImage());

    }

    IEnumerator comparImage(){

        UIScore.GetComponent<UIScoring>().OpenPanel();
        yield return null;

        
        //Load the drawing 
        Texture2D tex =GameObject.Find("Toile").GetComponent<SpriteRenderer>().sprite.texture;

        Texture2D refTex = new Texture2D(2,2);
        refTex.LoadImage(File.ReadAllBytes(Application.persistentDataPath + "/../currentRef.png"));
        //Compress both image
        tex = compression(tex,10);
        refTex = compression(refTex,10);

        //Update both image
        tex.Apply();
        refTex.Apply();

        //Apply the edge detection algorithm
        bool[,] refEdges = edgeDetection(refTex);
        refEdges = trimAndScale(refEdges,400,200);
        bool[,] edges = edgeDetection(tex);
        edges = trimAndScale(edges,400,200);

        //Apply the distance-to-edge formula to obtain an int matrix for each of them
        int[,] edgesDist = edgeDistComputing(edges);
        int[,] edgesDistRef = edgeDistComputing(refEdges);

        //Calculate scores. One score using the ref as a reference, the other one using the drawing as a reference
        (int score, int scoreMax) scores = scoring(edgesDist,refEdges);
        (int score, int scoreMax) scores2 = scoring(edgesDistRef,edges);
        float score1 = 1-(float)scores.score/(float)scores.scoreMax;
        float score2 = 1-(float)scores2.score/(float)scores2.scoreMax;

        Debug.Log(score1);
        Debug.Log(score2);

        //The final score will be the minimum of both scores with some modification
        //(we look only at values between 0.25 and 0.75)
        double scoreFinal = Min(score1,score2)-0.25;
        scoreFinal = scoreFinal *2;
        scoreFinal = Max(scoreFinal,0);
        scoreFinal = Min(scoreFinal,1);
        if(double.IsNaN(scoreFinal)){
            scoreFinal=0;
        }
        Debug.Log(scoreFinal);
        UIScore.GetComponent<UIScoring>().setDrawableFalse();
        UIScore.GetComponent<UIScoring>().updateScore((float)scoreFinal);
        
        //Show the edge-detection image on the GameComponent
        //tex = edgeDistToTex(edgesDist);
        
        //tex.Apply();

        //Sprite s = Sprite.Create(tex,new Rect(0,0,tex.width,tex.height),new Vector2(0.5f,0.5f));
        //imageConteneur.GetComponent<Image>().sprite = s;
        
    }

    float getValue(Color c){
        return (c.r+c.g+c.b)/3;
    }

    //Compress a Texture2D image using a compression coefficient
    Texture2D compression(Texture2D textBase,int coefCompression){

        Texture2D newtex = new Texture2D(textBase.width/coefCompression,textBase.height/coefCompression);

        for(int x=0; x<newtex.width;x++){
            for(int y=0; y<newtex.height;y++){

                Color compressedColor = new Color(0,0,0,0);
                for(int i=0;i<coefCompression;i++){
                    for(int j=0;j<coefCompression;j++){
                        compressedColor += textBase.GetPixel(coefCompression*x+i,coefCompression*y+j);
                    }
                }
                
                newtex.SetPixel(x,y,compressedColor/(coefCompression*coefCompression));
            }
            
        }
        return newtex;
    }

    //Detecte les bornes 
    bool[,] trimAndScale(bool[,] textBase, int width, int height){

        int minY = textBase.GetLength(0);
        int minX = textBase.GetLength(1);
        int maxY = 0;
        int maxX = 0;
        
        for(int x=0;x<textBase.GetLength(1);x++){
            for(int y = 0; y<textBase.GetLength(0);y++){
                if(textBase[y,x]){
                    minY = Min(minY,y);
                    minX = Min(minX,x);
                    maxY = Max(maxY, y);
                    maxX = Max(maxX,x);
                }
            }
        }

        bool[,] newtex =  new bool[height,width];

        float deltaX = maxX-minX;
        float deltaY = maxY-minY;
        if(deltaX<0){
            return new bool[height,width];
        }

        for(int x=0;x<width;x++){
            for(int y=0;y<height;y++){
                newtex[y,x] = textBase[(int)Round(y*deltaY/(float)height)+minY,(int)Round(x*deltaX/(float)width)+minX];
            }
        }

        return newtex;
    }

    //Edge Detection algorithm 
    bool[,] edgeDetection(Texture2D tex){

        bool[,] edgeArr = new bool[tex.height,tex.width];

        
        for(int i=0;i<tex.width;i++){
            for(int j=0;j<tex.height;j++){
                if(j==0 || i==0 || i==tex.width-1 || j == tex.height-1){
                    edgeArr[j,i]=false;
                }else{
                    Color edge = Color.black;
                    edge+=tex.GetPixel(i-1,j-1);
                    edge+=2*tex.GetPixel(i-1,j);
                    edge+=tex.GetPixel(i-1,j+1);
                    edge-=tex.GetPixel(i+1,j-1);
                    edge-=2*tex.GetPixel(i+1,j);
                    edge-=tex.GetPixel(i+1,j+1);
                    edge/=4;

                    float comp = Max(edge.maxColorComponent,(-1*edge).maxColorComponent);
                    

                    edge = Color.black;
                    edge+=tex.GetPixel(i-1,j-1);
                    edge+=2*tex.GetPixel(i,j-1);
                    edge+=tex.GetPixel(i+1,j-1);
                    edge-=tex.GetPixel(i-1,j+1);
                    edge-=2*tex.GetPixel(i,j+1);
                    edge-=tex.GetPixel(i+1,j+1);
                    edge/=4;

                    float comp2 = Max(edge.maxColorComponent,(-1*edge).maxColorComponent);

                    edgeArr[j,i]=(Sqrt(comp*comp+comp2*comp2)>0.4);
                }
                

            }
        }

        return edgeArr;
    }

    //Transform a bool matrix into a Texture2D B&W image
    Texture2D edgeToTex(bool[,] edges){
        Texture2D tex = new Texture2D(edges.GetLength(1),edges.GetLength(0));

        for(int x=0;x<tex.width;x++){
            for(int y=0;y<tex.height;y++){
                if(edges[y,x]){
                    tex.SetPixel(x,y,Color.white);
                }else{
                    tex.SetPixel(x,y,Color.black);
                }

            }
        }

        return tex;

    }

    //Return an int matrix using the distance to each edge.
    int[,] edgeDistComputing (bool[,] edgesMap){
        int [,] distToEdge = new int[edgesMap.GetLength(0),edgesMap.GetLength(1)];
        for(int i=0;i<edgesMap.GetLength(0);i++){
            for(int j=0;j<edgesMap.GetLength(1);j++){
                distToEdge[i,j]=20;
            }
        }

        for(int i=0;i<edgesMap.GetLength(0);i++){
            for(int j=0;j<edgesMap.GetLength(1);j++){
                if(edgesMap[i,j]){
                    explore(i,j,ref distToEdge,0);
                }
            }
        }
        return distToEdge;
    }

    //Calculate the score
    (int,int) scoring(int[,] drawing, bool[,] reference){
        int score =0;
        int scoreMax = 0;
        for(int i=0;i<reference.GetLength(0);i++){
            for(int j=0;j<reference.GetLength(1);j++){
                if(reference[i,j]){
                    score+=drawing[i,j];
                    scoreMax+=20;
                }
            }
        }
        return (score,scoreMax);
    }

    //Explore the matrix
    void explore(int x, int y,ref int[,] arr, int dist){
        if(x<0||y<0||x>=arr.GetLength(0)||y>=arr.GetLength(1)||arr[x,y]<=dist|| dist>=20){
            return;
        }else{
            arr[x,y]=dist;
            explore(x-1,y,ref arr,dist+1);
            explore(x+1,y,ref arr,dist+1);
            explore(x,y-1,ref arr,dist+1);
            explore(x,y+1,ref arr,dist+1);
            return;
        }

    }

    //Transform an int matrix into a Texture2D Image
    Texture2D edgeDistToTex(int[,] edges){
        Texture2D tex = new Texture2D(edges.GetLength(1),edges.GetLength(0));

        for(int x=0;x<tex.width;x++){
            for(int y=0;y<tex.height;y++){
                    Color c = new Color((float)(edges[y,x]/20.0),(float)(edges[y,x]/20.0),(float)(edges[y,x]/20.0),1);
                    tex.SetPixel(x,y,c);
                

            }
        }

        return tex;
    }

}   
