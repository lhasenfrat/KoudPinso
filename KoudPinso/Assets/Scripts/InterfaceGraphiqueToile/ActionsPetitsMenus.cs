using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ActionsPetitsMenus : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    
    bool inContext;
    public GameObject myGO;
    public GameObject toile;


    private void Awake()
    {
        myGO = gameObject;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !inContext)
        {
            
            myGO.SetActive(inContext);
            toile.GetComponent<Drawable>().CoroutineAllowDrawing();
        }
    }
 
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("incontext true");
        inContext = true;
    }
 
    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("incontext false");
        inContext = false;
    }

    public void setInContextFalse()
    {
        Debug.Log("incontext false par bouton");
        inContext = false;
    }
}
