using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private GameObject resumeButton;

    private void Awake()
    {
        Debug.Log(PlayerPrefs.GetInt("currentLevel"));
        if (PlayerPrefs.GetInt("currentLevel") == 0)
        {
            resumeButton.SetActive(false);
        }
    }

    public void NewGame()
    {
        Progress.NewGame();
        SwitchScene("Main");
    }
    
    public void Resume()
    {
        SwitchScene("Main");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting");
        Application.Quit();
    }

    private void SwitchScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
