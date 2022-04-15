using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
public class ExerciceText : MonoBehaviour
{
    // Start is called before the first frame update
    public Text exotext;
    public string titre = "template";
	public string description = "superdescription";

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
        this=ReadFromFile(Application.persistentDataPath + "/../currentexercise.json");
        Text.text = this.titre +"\n" + this.description;
    }

}
