using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class HomeUIManager : MonoBehaviour
{
    RectTransform rectTransform;

    #region Getter
    static HomeUIManager instance;
    public static HomeUIManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<HomeUIManager>();
            if (instance == null)
                Debug.LogError("HomeUIManager not found");
            return instance;

        }
    }
    #endregion Getter

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        rectTransform.DOAnchorPosY(0, 0f);
    }

    void Update(){
        if (Application.platform == RuntimePlatform.Android) {
        
        // Check if Back was pressed this frame
        if (Input.GetKeyDown(KeyCode.Escape)) {
            
            // Quit the application
            ShowSettingsMenu();
        }
    }
    }

    public void Show(float delay = 0f)
    {
        rectTransform.DOAnchorPosY(0, 0.3f).SetDelay(delay);
    }

    public void Hide(float delay = 0f)
    {
        rectTransform.DOAnchorPosY(rectTransform.rect.height * -1, 0.3f).SetDelay(delay);
    }

    public void ShowSettingsMenu()
    {
        Hide();
        SettingsUIManager.Instance.Show();
    }

}