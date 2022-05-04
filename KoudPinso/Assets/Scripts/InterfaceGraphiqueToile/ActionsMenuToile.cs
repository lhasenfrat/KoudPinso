using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ActionsMenuToile : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
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
       

    }
 
    public void OnPointerEnter(PointerEventData eventData)
    {
        inContext = true;
        Debug.Log("contexte "+ inContext);
    
        toile.GetComponent<Drawable>().setAllowedDrawing(!inContext);
    }
 
    public void OnPointerExit(PointerEventData eventData)
    {
        inContext = false;
        Debug.Log("contexte "+ inContext);
    
        toile.GetComponent<Drawable>().setAllowedDrawing(!inContext);
    }

    public void setInContextFalse()
    {
        Debug.Log("incontext false par bouton");
        inContext = false;
    }
}
