using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Math;
using System;
using UnityEngine.UI;

public class ImageComparison : MonoBehaviour
{

    public GameObject imageConteneur;
    public TextAsset pic;
    

    // Start is called before the first frame update
    void Start()
    {
        Texture2D tex = new Texture2D(2, 2);
        tex.LoadImage(pic.bytes);
        
        tex = compression(tex,10);

        Debug.Log(tex.width);
        Debug.Log(tex.height);

        tex.Apply();

        bool[,] edges = edgeDetection(tex);

        int[,] edgesDist = edgeDistComputing(edges);

        tex = edgeDistToTex(edgesDist);
        tex.Apply();

        Sprite s = Sprite.Create(tex,new Rect(0,0,tex.width,tex.height),new Vector2(0.5f,0.5f));
        imageConteneur.GetComponent<Image>().sprite = s;
        
    }

    float getValue(Color c){
        return (c.r+c.g+c.b)/3;
    }

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

    bool[,] edgeDetection(Texture2D tex){

        bool[,] edgeArr = new bool[tex.width,tex.height];

        
        for(int i=0;i<tex.width;i++){
            for(int j=0;j<tex.height;j++){
                if(j==0 || i==0 || j==tex.height-1 || i == tex.width-1){
                    edgeArr[i,j]=false;
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

                    edgeArr[i,j]=(Sqrt(comp*comp+comp2*comp2)>0.4);
                }
                

            }
        }

        return edgeArr;
    }

    Texture2D edgeToTex(bool[,] edges){
        Texture2D tex = new Texture2D(edges.GetLength(0),edges.GetLength(1));

        for(int i=0;i<tex.width;i++){
            for(int j=0;j<tex.height;j++){
                if(edges[i,j]){
                    tex.SetPixel(i,j,Color.white);
                }else{
                    tex.SetPixel(i,j,Color.black);
                }

            }
        }

        return tex;

    }

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

    Texture2D edgeDistToTex(int[,] edges){
        Texture2D tex = new Texture2D(edges.GetLength(0),edges.GetLength(1));

        for(int i=0;i<tex.width;i++){
            for(int j=0;j<tex.height;j++){
                    Color c = new Color((float)(edges[i,j]/20.0),(float)(edges[i,j]/20.0),(float)(edges[i,j]/20.0),1);
                    tex.SetPixel(i,j,c);
                

            }
        }

        return tex;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}   
