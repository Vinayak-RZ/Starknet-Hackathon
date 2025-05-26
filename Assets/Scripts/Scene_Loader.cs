using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    //Load a scene by name
    public GameObject UsernameScreen = null;
    public void LoadScene(string sceneName)
    {
        PlayerPrefs.SetString("lastscene", SceneManager.GetActiveScene().name);
        SceneManager.LoadScene(sceneName);
    }
    public void SetNumberPlayerScene(int number)
    {
        PlayerPrefs.SetString("lastscene", SceneManager.GetActiveScene().name);
        PlayerPrefs.SetInt("numberOfPlayers", number);
        PlayerPrefs.Save();
        SceneManager.LoadScene("Map 1");
        // if (UsernameScreen != null)
        // {
        //     UsernameScreen.SetActive(true);
        // }
    }

    //Reload the current scene
    public void ReloadScene()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName);
        PlayerPrefs.Save();
    }

    //Load previous scene (if needed)
    public void LoadPreviousScene()
    {
        string lastsceneee = PlayerPrefs.GetString("lastscene");
        PlayerPrefs.SetString("lastscene", SceneManager.GetActiveScene().name);
        SceneManager.LoadScene(lastsceneee);
        PlayerPrefs.Save();
    }

    //Quit the game (for builds)
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game Quit!"); // Just for testing in the editor
    }
    
}
