using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public Text levelLabel;
    public Text levelHighscore;
    public Text p1Highscore;
    public Text p2Highscore;

    // Start is called before the first frame update
    void Start()
    {
        levelLabel.text = "The Only Level (so far)";
        if(levelHighscore != null)
            levelHighscore.text = PlayerPrefs.GetInt("highScore").ToString("000000");
        if (p1Highscore != null)
            p1Highscore.text = PlayerPrefs.GetInt("p1Score").ToString("000000");
        if (p2Highscore != null)
            p2Highscore.text = PlayerPrefs.GetInt("p2Score").ToString("000000");
        Invoke("LoadLevel", 3f);
    }

    private void LoadLevel()
    {
        SceneManager.LoadScene(LoadingData.sceneToLoad);
    }
}
