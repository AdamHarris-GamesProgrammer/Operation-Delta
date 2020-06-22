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
    public int highscore { get; set; }

    private string mapKey;

    bool soundPlayed;

    private AudioSource audioSource;

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

        if (Debug.isDebugBuild)
        {
            WipeHighscore();
        }

        highscore = PlayerPrefs.GetInt(mapKey);

        Debug.Log("Highscore: " + highscore);

        audioSource = GetComponent<AudioSource>();
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

            if (!soundPlayed)
            {
                soundPlayed = true;
                audioSource.Play();
            }

        }
    }

    public void WipeHighscore()
    {
        highscore = 0;
        PlayerPrefs.SetInt(mapKey, highscore);
    }

}
