using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Cinematique : MonoBehaviour
{

    public GameObject panelCinematique;
    public GameObject toile;

    public GameObject exo;

    private string titreExo;

    public GameObject boutonCinematique; 
	public GameObject boutonFini;
    // Start is called before the first frame update
    public void checkDessinLibre()
    {

        getNomExo();
        Debug.Log(titreExo);
        if(titreExo!="Dessin libre")
        {
            panelCinematique.SetActive(true);
            toile.GetComponent<Drawable>().setAllowedDrawing(false);
            boutonCinematique.SetActive(true);
            boutonFini.SetActive(true);
        }else
        {
            
            panelCinematique.SetActive(false);
            toile.GetComponent<Drawable>().setAllowedDrawing(true);
            boutonCinematique.SetActive(false);
            boutonFini.SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClosePanel()
    {
        panelCinematique.SetActive(false);
        toile.GetComponent<Drawable>().setAllowedDrawing(true);
    }

    public void OpenPanel()
    {
        panelCinematique.SetActive(true);
        toile.GetComponent<Drawable>().setAllowedDrawing(false);
    }

    public void getNomExo()
    {
        Debug.Log("cc");
        titreExo = exo.GetComponent<AffichageText>().getTitre();
        Debug.Log("cccc");

    }

    
}
