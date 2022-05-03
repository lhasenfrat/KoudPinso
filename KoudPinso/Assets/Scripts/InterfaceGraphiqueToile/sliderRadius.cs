using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class sliderRadius : MonoBehaviour
{

    public GameObject slider;
    public GameObject circle;
    public Drawable myScript;
    float EpaisseurSlider;
    public float defaultRadius;
    RectTransform rectTransform;


    void Start()
    {
        EpaisseurSlider = slider.GetComponent <Slider> ().value;
        EpaisseurSlider = 0.5f;

    } 

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnValueChanged(float newValue)
    {
        rectTransform = circle.GetComponent<RectTransform>();
        Vector3 newSize = new Vector3 (newValue*defaultRadius, newValue*defaultRadius,0 );
        rectTransform.DOScale(newSize, 0f);
        myScript.SetThickness(newValue); 
    }
}
