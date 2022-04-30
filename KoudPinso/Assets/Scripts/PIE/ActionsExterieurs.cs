using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionsExterieurs : MonoBehaviour
{
    
    public GameObject panel;

    void Start()
    {
        hidePanel();
    }

    void Update()
    {
        
    }

    //Permet de revoir l'intro
    public void revoirIntro(){
        Debug.Log("Cinematique");
    }

    //Permet d'effacer la zone de dessin
    public void reinitialiser(){
        Debug.Log("Effaçage");
    }

    //Affiche la demande de confirmation de sortie
    public void showPanel(){
        if(!panel.activeSelf)
        {
            panel.SetActive(true);
        }
     
    }

    //Ferme la demande de confirmation de sortie
    public void hidePanel(){
        if(panel.activeSelf)
        {
            panel.SetActive(false);
        }
     
    }

    //Quitte le canvas
    public void quitter(){
        Debug.Log("Je quitte !");
    }

    //Ferme et ouvre au clic sur le meme bouton
    public void showHidePanel(){
        if(panel.activeSelf)
        {
            panel.SetActive(false);
        }else 
        {

            panel.SetActive(true);
        }
     
    }

    
}
