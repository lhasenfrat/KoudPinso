using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudio : MonoBehaviour
{
    // Start is called before the first frame update
    public void playMusic()
    {
        BGSoundScript.Instance.gameObject.GetComponent<AudioSource>().Play();
    }
}