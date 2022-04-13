using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AffichageText : MonoBehaviour
{
    // Start is called before the first frame update

    public Text textTitre;
    public Text textDescription;
    public Text textObj; 
    public GameObject exo;

    void Start()
    {
        string[] lines = exo.GetComponent<Text>().text.Split('\n');
        textTitre.text = lines[0];
        textDescription.text = lines[1];
        textObj.text="";
        int nbPoint = Int32.Parse(lines[2]);
        for(int i = 0; i<nbPoint;i++){
            textObj.text += "○ "+lines[3+i]+"\n";

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
