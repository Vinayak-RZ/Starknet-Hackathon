using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    //public Button pauseButton;
    public GameObject DraftButton;

    public GameObject PauseScreen;
    private bool isPaused = false;

    void Start()
    {
        PauseScreen.SetActive(false);
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        AudioListener.pause = isPaused; // Pause all audio when game is paused
        Debug.Log("Pause Toggled: " + isPaused);
        if (isPaused)
        {
            PauseScreen.SetActive(true);
            DraftButton.SetActive(false);
        }
        else
        {
            PauseScreen.SetActive(false);
            DraftButton.SetActive(true);
        }
    }
}
