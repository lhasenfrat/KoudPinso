using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        /*foreach(var e in tex.GetRawTextureData<Color32>()){
            Debug.Log(e);
        }*/
        
        for(int i=0;i<20;i++){
            for(int j=0;j<20;j++){
                tex.SetPixel(20+i,20+j,Color.gray);
            }
        }
        tex.Apply();
        Debug.Log(tex.height);
        Debug.Log(tex.width);
        Debug.Log(tex.GetRawTextureData<Color32>().Length);
        Sprite s = Sprite.Create(tex,new Rect(0,0,tex.width,tex.height),new Vector2(0.5f,0.5f));
        imageConteneur.GetComponent<Image>().sprite = s;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}   
