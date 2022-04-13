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

    // Update is called once per frame
    void Update()
    {
        
    }

    public void revoirIntro(){
        Debug.Log("Cinematique");
    }

    public void reinitialiser(){
        Debug.Log("Effaçage");
    }

    public void showQuitPanel(){
        quitPanel.SetActive(true);
    }

    public void hideQuitPanel(){
        quitPanel.SetActive(false);
    }

    public void quitter(){
        Debug.Log("Je quitte !");
    }

    
}
