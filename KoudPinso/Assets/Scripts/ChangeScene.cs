using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
public class ChangeScene : MonoBehaviour
{

    //Fonction pour changer de sc�ne en utilisant le sceneID (ID par ordre de sc�ne dans Build Setting)
    public void MoveToSceneInt(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }

    //Fonction pour changer de sc�ne en utilisant leur nom
    public void MoveToSceneStr(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }


    public void OpenExercice(string exerciceName)
    {
        Debug.Log(Application.dataPath+"/GameData/"+exerciceName+"data.json"+"\n"+Application.persistentDataPath + "/../currentexercise.json");
        CopyFile(Application.dataPath+"/GameData/"+exerciceName+"data.json",Application.persistentDataPath + "/../currentexercise.json");
        SceneManager.LoadScene("ToileScene");

    }
	public static void CopyFile(string filePathSource,string filePathDestination)
	{
		// If the file doesn't exist then just return the default object.
		if (!File.Exists(filePathSource))
		{
			Debug.LogErrorFormat("ReadFromFile({0}) -- file not found, returning new object", filePathSource);
		}
		else
		{
			string contents = File.ReadAllText(filePathSource);
			File.WriteAllText(filePathDestination, contents);
		}
	}


}
