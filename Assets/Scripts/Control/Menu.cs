using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour {

    Button playButton;
    Button highScoreButton;
    Button exitButton;
    GameObject menuButtons;
    Camera mainCamera;
    Quaternion cameraDefaultRotation;
    Text highscore;
    GameObject camTarget;
    Text highScoreTable;

    private void Start()
    {
        playButton = GameObject.Find("PlayButton").GetComponent<Button>();
        highScoreButton = GameObject.Find("HighscoresButton").GetComponent<Button>();
        exitButton = GameObject.Find("ExitButton").GetComponent<Button>();
        menuButtons = GameObject.Find("MenuButtons").GetComponent<GameObject>();

        mainCamera = GetComponentInParent<MenuRoot>().GetComponentInChildren<Camera>();
        cameraDefaultRotation = mainCamera.transform.rotation;
        camTarget = GameObject.Find("CamTarget").GetComponent<GameObject>();
        highScoreTable = GameObject.Find("HighscoreTable").GetComponent<Text>();

        playButton.onClick.AddListener(StartScene);
        highScoreButton.onClick.AddListener(ShowHighScores);
        exitButton.onClick.AddListener(ExitGame);
    }

    void StartScene()
    {

        Debug.Log("Starting game..");
        UnityEngine.SceneManagement.SceneManager.LoadScene("scene001");
    }

    void toggleButtonVisibility(bool show)
    {
        playButton.gameObject.SetActive(show);
        highScoreButton.gameObject.SetActive(show);
        exitButton.gameObject.SetActive(show);
    }

    void ShowHighScores()
    {
        toggleButtonVisibility(false);
        //highScoreTable.text = "score: " + PlayerPrefs.GetInt("highscore");
        StringBuilder sb = new StringBuilder();
        int i = 0;
        while (PlayerPrefs.HasKey(i.ToString()))
        {
            sb.Append(PlayerPrefs.GetInt(i.ToString()) + "\n");
        }
        highScoreTable.text = sb.ToString();
    }

    void ExitGame()
    {
        Application.Quit();
    }

}
