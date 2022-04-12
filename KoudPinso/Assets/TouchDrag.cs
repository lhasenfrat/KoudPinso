using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchDrag : MonoBehaviour
{
    bool canScroll;
    Vector3 firstMousePos; //The position of the mouse at the begining

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); //Mouse position every frame
        float x = transform.position.x; //Camera x position
        float y = transform.position.y; //Camera y position
        float z = transform.position.z; //Camera z position

        if (Input.GetMouseButtonDown(0))
        {
            firstMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            canScroll = true;
        }

        if (Input.GetMouseButtonUp (0))
        {
            canScroll = false;
        }
        if (canScroll)
        {
            transform.position = new Vector3(x + firstMousePos.x - mousePos.x, y + firstMousePos.y - mousePos.y, z); //Make the scroll movement
        }
    }
    
}
