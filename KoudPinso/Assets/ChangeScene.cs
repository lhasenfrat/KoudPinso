using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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


}
