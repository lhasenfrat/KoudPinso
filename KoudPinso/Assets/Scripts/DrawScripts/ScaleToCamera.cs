using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleToCamera : MonoBehaviour
{
    
    Vector2 res;
    public Camera cam;
    public GameObject myreference;
    Vector3 myextents;
    Vector3 myCamCoord;
    int countdown;

    void OnDrawGizmos() {
        Bounds bounds = myreference.GetComponent<SpriteRenderer>().bounds;        
        Gizmos.matrix = Matrix4x4.identity;
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(bounds.center, bounds.extents * 2);
    }

    // Start is called before the first frame update
    void Awake()
    {
        res=new Vector2(cam.pixelWidth,cam.pixelHeight);
        myextents = myreference.GetComponent<SpriteRenderer>().bounds.extents;
        myCamCoord = cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth,cam.pixelHeight,0));
        countdown=-1;
        this.transform.localScale= new Vector3(myCamCoord.x/myextents.x,myCamCoord.y/myextents.y,1);
    
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(myextents);
        Debug.Log(myCamCoord);
        Debug.Log("      ");

        /* changement dynamique, surement inutile et buggé
        countdown--;
    
        if (res.x!=cam.pixelWidth || res.y!=cam.pixelHeight){
            countdown=100;
            res.x=cam.pixelWidth;
            res.y=cam.pixelHeight;
        }
        
        if (countdown==0){
            myextents = myreference.GetComponent<SpriteRenderer>().bounds.extents;
            myCamCoord = cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth,cam.pixelHeight,0));
        
            this.transform.localScale= new Vector3(myCamCoord.x/myextents.x,myCamCoord.y/myextents.y,1);
    
            
            countdown=-1;
        }

        */
        
        
    }
}
