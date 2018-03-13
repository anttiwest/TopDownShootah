using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour {

    Button playButton;
    Button highScoreButton;
    Button exitButton;

    private void Start()
    {
        playButton = GameObject.Find("PlayButton").GetComponent<Button>();
        highScoreButton = GameObject.Find("HighscoresButton").GetComponent<Button>();
        exitButton = GameObject.Find("ExitButton").GetComponent<Button>();

        playButton.onClick.AddListener(StartScene);
        highScoreButton.onClick.AddListener(ShowHighScores);
        exitButton.onClick.AddListener(ExitGame);
    }

    void StartScene()
    {
        Debug.Log("Starting game..");
        UnityEngine.SceneManagement.SceneManager.LoadScene("scene001");
    }

    void ShowHighScores()
    {

    }

    void ExitGame()
    {
        Application.Quit();
    }

}
