using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Affiche le texte sur le PIE

public class AffichageText : MonoBehaviour
{
    /*
    textTitre : UI_Text titre du niveau
    textDescription : UI_Text Description du niveau
    textObj : Ui_Text objectifs du niveau (sous forme de liste)
    exo: GameObject avec un component text qui comporte les infos du niveau
    */
    public Text textTitre;
    public Text textDescription;
    public Text textObj; 
    public GameObject exo;
    public GameObject cinematique;


    //recupère les infos dans exo et les met dans les objects text
    void Start()
    {
        ChangeText();
    }

    public void ChangeText(){
        string[] lines = exo.GetComponent<Text>().text.Split('\n');
        textTitre.text = lines[0];
        textDescription.text = lines[1];
        textObj.text="";
        int nbPoint = Int32.Parse(lines[2]);
        for(int i = 0; i<nbPoint;i++){
            textObj.text += "○ "+lines[3+i]+"\n";

        }
        cinematique.GetComponent<Cinematique>().checkDessinLibre();

    }

    public string getTitre()
    {
        return textTitre.text;
    }

}
