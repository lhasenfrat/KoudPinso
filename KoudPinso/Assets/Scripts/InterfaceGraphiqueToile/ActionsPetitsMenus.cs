using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ActionsPetitsMenus : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    
    bool inContext;
    public GameObject myGO;
    public GameObject toile;
    public GameObject panelCouleur;
    public GameObject panelOutil;

    public GameObject panelGomme;

    public GameObject canva;


    private void Awake()
    {
        myGO = gameObject;

    }

    void Update()
    {
        
        if (Input.GetMouseButtonUp(0) && !inContext)
        {
           
            myGO.SetActive(false);
            toile.GetComponent<Drawable>().CoroutineAllowDrawing();

        }
 
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
