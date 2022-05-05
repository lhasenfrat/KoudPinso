using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
public class GenerateExerciceText : MonoBehaviour
{
	public Text exotext;
	public GameObject mypanel;
	public GameObject Base;

	public GameObject ExempleExo1_1;

    public static ExerciceText ReadFromFile(string filePath)
	{
		// If the file doesn't exist then just return the default object.
		if (!File.Exists(filePath))
		{
			Debug.LogErrorFormat("ReadFromFile({0}) -- file not found, returning new object", filePath);
			return new ExerciceText();
		}
		else
		{
			// If the file does exist then read the entire file to a string.
			string contents = File.ReadAllText(filePath);
 
			// If it happens that the file is somehow empty then tell us and return a new SaveData object.
			if (string.IsNullOrEmpty(contents))
			{
				Debug.LogErrorFormat("File: '{0}' is empty. Returning default SaveData");
				return new ExerciceText();
			}
 
			// Otherwise we can just use JsonUtility to convert the string to a new SaveData object.
			return JsonUtility.FromJson<ExerciceText>(contents);
		}
	}
    

    void Start()
    {
        ExerciceText etext = ReadFromFile(Application.persistentDataPath + "/../currentexercise.json");

        exotext.text = etext.title +"\n" + etext.description+ "\n"+etext.nbetape;
		for(int i=0;i<etext.nbetape;i++){
			exotext.text+="\n"+etext.etapes[i];
		}
		Debug.Log(etext.path);
		if (etext.path=="Monde1/Exo1/"){
			ExempleExo1_1.SetActive(true);

		}else{
			ExempleExo1_1.SetActive(false);
		}
		mypanel.GetComponent<AffichageText>().ChangeText();
		Texture2D skin;
		skin = Base.GetComponent<IMG2Sprite>().LoadTexture(Application.persistentDataPath + "/../currentbase.png");
		MaterialPropertyBlock block = new MaterialPropertyBlock();
     	block.SetTexture("_MainTex",skin);
		Base.GetComponent<SpriteRenderer>().SetPropertyBlock(block);


    }

}