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
        this.currentHighscore = PlayerPrefs.GetInt("highscore");
        Debug.Log("currighscore: " + currentHighscore + ", scoreboard: " + scoreBoard.name);
    }

    public void UpdateScore(int amount)
    {
        Debug.Log("SCORE: " + score);
        score += amount;
        scoreBoard.text = score.ToString();

        if(score > currentHighscore)
        {
            PlayerPrefs.SetInt("highscore", score);
        }

        
    }
}
