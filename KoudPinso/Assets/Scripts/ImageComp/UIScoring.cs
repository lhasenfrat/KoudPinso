using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

using static System.Math;

public class UIScoring : MonoBehaviour
{
    private GameObject panel;
    private GameObject toile;
    private GameObject bar;
    // Start is called before the first frame update
    void Start()
    {
        panel = gameObject;
        toile = GameObject.Find("Toile");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClosePanel()
    {   
        panel.SetActive(false);
        toile.GetComponent<Drawable>().setAllowedDrawing(true);
    }

    public void OpenPanel()
    {   
        
        panel = gameObject;
        toile = GameObject.Find("Toile");
        panel.SetActive(true);
        bar = GameObject.Find("ProgressBar");
        GameObject.Find("TextScore").GetComponent<Text>().text="";
        GameObject.Find("TextEtatScoring").GetComponent<Text>().text="Calcul du score en cours...";
        bar.GetComponent<Slider>().value=0;
       // toile.GetComponent<Drawable>().setAllowedDrawing(false);
    }

    public void updateScore(float score){
        bar.GetComponent<Slider>().DOValue(score,(float)2);
        GameObject.Find("TextScore").GetComponent<Text>().text=((int)Round(score*100)).ToString()+" sur 100";
        GameObject.Find("TextEtatScoring").GetComponent<Text>().text="Votre score est:";
    }

    public void setDrawableFalse(){
        toile.GetComponent<Drawable>().setAllowedDrawing(false);
    }
}
