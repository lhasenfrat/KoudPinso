using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
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
        
        string filePath = Application.streamingAssetsPath+"/GameData/"+exerciceName+"data.json";
        string fileImage = Application.streamingAssetsPath+"/GameData/"+exerciceName+"Base.png";
        string fileRef = Application.streamingAssetsPath+"/GameData/"+exerciceName+"Ref.png";

        if (Application.platform == RuntimePlatform.Android)
        {
            WWW reader = new WWW(filePath);
            while (!reader.isDone) { }
            string jsonString = reader.text;
            StreamWriter writer = new StreamWriter(Application.persistentDataPath + "/../currentexercise.json", false);
            writer.Write(jsonString);
            writer.Close();
        }
        else
        {
            CopyFile(Application.streamingAssetsPath+"/GameData/"+exerciceName+"data.json",Application.persistentDataPath + "/../currentexercise.json");
            
        }
        UnityWebRequest webRequest = UnityWebRequest.Get(fileImage);
        webRequest.SendWebRequest();
        while(!webRequest.isDone){}
        if (webRequest.result==UnityWebRequest.Result.Success)
        {
            System.IO.File.WriteAllBytes(Application.persistentDataPath + "/../currentbase.png",webRequest.downloadHandler.data);
        } else {
            Debug.Log("not working");
        }

        UnityWebRequest webRequestRef = UnityWebRequest.Get(fileRef);
        webRequestRef.SendWebRequest();
        while(!webRequestRef.isDone){}
        if (webRequestRef.result==UnityWebRequest.Result.Success)
        {
            System.IO.File.WriteAllBytes(Application.persistentDataPath + "/../currentRef.png",webRequestRef.downloadHandler.data);
        } else {
            Debug.Log("not working");
        }
             
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
