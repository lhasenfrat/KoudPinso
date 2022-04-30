using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class SettingsUIManager : MonoBehaviour
{

	public GameObject toile;
    private Vector2 fingerDownPos;
	private Vector2 fingerUpPos;

	private bool isShown = false;

	public bool detectSwipeAfterRelease = false;

	public float SWIPE_THRESHOLD = 500f;
    RectTransform rectTransform;
    #region Getter
    static SettingsUIManager instance;
    public static SettingsUIManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<SettingsUIManager>();
            if (instance == null)
                Debug.LogError("HomeUIManager not found");
            return instance;
        }
    }
    #endregion Getter

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        rectTransform.DOAnchorPosY(rectTransform.rect.height, 0f);
    }

    void Update ()
	{

		foreach (Touch touch in Input.touches) {
			if (touch.phase == TouchPhase.Began) {
				fingerUpPos = touch.position;
				fingerDownPos = touch.position;
			}

			//Detects Swipe while finger is still moving on screen
			if (touch.phase == TouchPhase.Moved) {
				if (!detectSwipeAfterRelease) {
					fingerDownPos = touch.position;
					DetectSwipe ();
				}
			}

			//Detects swipe after finger is released from screen
			if (touch.phase == TouchPhase.Ended) {
				fingerDownPos = touch.position;
				DetectSwipe ();
			}
		}
	}

    public void Show(float delay = 0f)
    {
        rectTransform.DOAnchorPosY(0, 0.3f).SetDelay(delay);
		isShown = true;
    }

    public void Hide(float delay = 0f)
    {
		isShown = false;
        rectTransform.DOAnchorPosY(rectTransform.rect.height, 0.3f).SetDelay(delay);
    }

    public void ShowHomeScreen()
    {
        Hide();
        HomeUIManager.Instance.Show();
    }

    void DetectSwipe ()
	{
		
		if (VerticalMoveValue () > SWIPE_THRESHOLD && VerticalMoveValue () > HorizontalMoveValue ()) {
			Debug.Log ("Vertical Swipe Detected!");
			if (fingerDownPos.y - fingerUpPos.y > 0) {
				OnSwipeUp ();
			} 
			fingerUpPos = fingerDownPos;
		}
	}

    float VerticalMoveValue ()
	{
		return Mathf.Abs (fingerDownPos.y - fingerUpPos.y);
	}

	float HorizontalMoveValue ()
	{
		return Mathf.Abs (fingerDownPos.x - fingerUpPos.x);
	}

    void OnSwipeUp ()
	{	
		if(isShown == true)
		{
			toile.GetComponent<Drawable>().CoroutineAllowDrawing();
			ShowHomeScreen();
		}


	}



}