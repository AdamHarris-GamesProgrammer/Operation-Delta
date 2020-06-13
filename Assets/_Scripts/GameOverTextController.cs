using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverTextController : MonoBehaviour
{
    [Header("Game Over UI")]
    [SerializeField] private Text scoreText;
    [SerializeField] private Text highscoreText;
    [SerializeField] private Text timeSurvivedText;

    void Start()
    {
        scoreText.text = ScoreTextController.instance.scoreText.text;
        highscoreText.text = "Highscore: " + ScoreTextController.instance.highscore;

        TimeSpan t = TimeSpan.FromSeconds(GameManager.instance.levelTimer);

        timeSurvivedText.text = t.Minutes + " Minutes " + t.Seconds + " Seconds!";

    }
}
