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
        Debug.Log("contexte : inContext");
    
        toile.GetComponent<Drawable>().setAllowedDrawing(!inContext);

    }
 
    public void OnPointerEnter(PointerEventData eventData)
    {
        inContext = true;
    }
 
    public void OnPointerExit(PointerEventData eventData)
    {
        inContext = false;
    }

    public void setInContextFalse()
    {
        Debug.Log("incontext false par bouton");
        inContext = false;
    }
}
