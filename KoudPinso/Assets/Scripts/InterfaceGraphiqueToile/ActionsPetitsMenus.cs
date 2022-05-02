using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ActionsPetitsMenus : MonoBehaviour
{
    EventSystem eventSystem;
    
    private void Awake()
    {
        eventSystem = FindObjectOfType<EventSystem>();
    }

    private void OnEnable()
    {
        eventSystem.SetSelectedGameObject(this.gameObject);
    }
}
