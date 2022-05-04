using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Cinematique : MonoBehaviour
{

    public GameObject panelCinematique;
    public GameObject toile;

    public GameObject pie;

    private string titreExo;
    // Start is called before the first frame update
    void Start()
    {
        getNomExo();
        if(titreExo!="Dessin libre")
        {
            panelCinematique.SetActive(true);
            toile.GetComponent<Drawable>().setAllowedDrawing(false);
        }else
        {
            panelCinematique.SetActive(false);
            toile.GetComponent<Drawable>().setAllowedDrawing(true);
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

    public void getNomExo()
    {
        Debug.Log("cc");
        titreExo = pie.GetComponent<AffichageText>().getTitre();
        Debug.Log("cccc");

    }
}
