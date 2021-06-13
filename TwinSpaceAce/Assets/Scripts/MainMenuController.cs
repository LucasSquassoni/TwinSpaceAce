using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public string levelToPlay;

    private void Start()
    {
        LoadingData.sceneToLoad = levelToPlay;
    }

    public void ChangeLevel()
    {        
        SceneManager.LoadScene("Loading");
    }

    public void CloseGame()
    {
        Application.Quit();
    }
}
