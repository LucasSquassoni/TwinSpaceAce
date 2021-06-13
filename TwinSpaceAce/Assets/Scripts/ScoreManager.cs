using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance = null;

    public int levelScore = 0;
    public Text scoreText;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Update()
    {
        scoreText.text = levelScore.ToString("000000");
    }

    public void SaveHighScore()
    {
        if(PlayerPrefs.GetInt("highScore") < levelScore)
        {
            PlayerPrefs.SetInt("highScore", levelScore);
        }
    }
}
