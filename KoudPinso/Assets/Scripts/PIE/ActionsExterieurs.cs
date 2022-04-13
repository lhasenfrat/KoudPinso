using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionsExterieurs : MonoBehaviour
{
    
    public GameObject quitPanel;

    void Start()
    {
        hideQuitPanel();
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
    public void showQuitPanel(){
        quitPanel.SetActive(true);
    }

    //Ferme la demande de confirmation de sortie
    public void hideQuitPanel(){
        quitPanel.SetActive(false);
    }

    //Quitte le canvas
    public void quitter(){
        Debug.Log("Je quitte !");
    }

    
}
