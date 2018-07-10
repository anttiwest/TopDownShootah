using System;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour {

    int score;
    Text scoreBoard;
    int currentHighscore;

    public ScoreCounter()
    {
        this.score = 0;
        this.scoreBoard = GameObject.Find("Score").GetComponent<Text>(); ;
        //this.currentHighscore = PlayerPrefs.GetInt("highscore");
        //Debug.Log("currighscore: " + currentHighscore + ", scoreboard: " + scoreBoard.name);
    }

    //public void UpdateScore(int amount)
    //{
    //    Debug.Log("SCORE: " + score);
    //    score += amount;
    //    scoreBoard.text = score.ToString();
    //}

    //public void SaveScore(int score)
    //{
    //    int i = 0;
    //    while (PlayerPrefs.HasKey(i.ToString()))
    //    {
    //        i++;
    //    }
    //    Debug.Log("logging: " + i + ", " + score);
    //    PlayerPrefs.SetInt(i + "", score);
    //}
}
