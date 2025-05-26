using UnityEngine;
using UnityEngine.UI;

public class MapScreeen : MonoBehaviour
{
    //public Button pauseButton;
    public GameObject DraftButton1;

    public GameObject MapScreen;
    private bool isShowing = false;

    void Start()
    {
        MapScreen.SetActive(false);
    }

    public void ToggleMap()
    {
        isShowing = !isShowing;
        AudioListener.pause = isShowing; // Pause all audio when game is paused
        Debug.Log("Pause Toggled: " + isShowing);
        if (isShowing)
        {
            MapScreen.SetActive(true);
            DraftButton1.SetActive(false);
        }
        else
        {
            MapScreen.SetActive(false);
            DraftButton1.SetActive(true);
        }
    }
}
