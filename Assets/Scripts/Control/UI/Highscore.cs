using UnityEngine;
using UnityEngine.UI;

public class Highscore : MonoBehaviour {

    Text highScore;

    private void Awake()
    {
        highScore = GetComponent<Text>();
        highScore.text = "Highscore: " + PlayerPrefs.GetInt("highscore");
    }
}
