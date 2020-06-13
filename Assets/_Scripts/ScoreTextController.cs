using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreTextController : MonoBehaviour
{
    public static ScoreTextController instance;

    [HideInInspector] public Text scoreText;

    public int gameScore { get; set; }

    private int highscore;
    private string mapKey;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        scoreText = GetComponent<Text>();

        mapKey = SceneManager.GetActiveScene().name + "Highscore";

        highscore = PlayerPrefs.GetInt(mapKey);

        Debug.Log("Highscore: " + highscore);
    }

    public void ScoreUp(int scoreIn)
    {
        gameScore += scoreIn;
        scoreText.text = "Score: " + gameScore;

        if (gameScore > highscore)
        {
            PlayerPrefs.SetInt(mapKey, gameScore);
            highscore = gameScore;
            //TODO: Add highscore sound

        }
    }
}
