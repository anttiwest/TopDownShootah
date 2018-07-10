using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public Player player;
    GoogleMobileAdverts googleAds;
    public int score;
    Text scoreBoard;
    public int highScore;

    void Awake()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        Cursor.visible = true;

    #if UNITY_ANDROID
            //googleAds = GameObject.Find("AdSystem").GetComponent<GoogleMobileAdverts>();
    #elif UNITY_IPHONE
            googleAds = GameObject.Find("AdSystem").GetComponent<GoogleMobileAdverts>();
    #endif

    }

    void FixedUpdate()
    {
        if (!GameObject.FindWithTag("Player") || Input.GetKeyDown(KeyCode.Escape))
        {
            LoadMenu();
        }
        CheckLifeStatus();
    }

    //void CheckLifeStatus()
    //{
    //    if (player.health <= 0)
    //    {
    //        Destroy(player);
    //    }
    //}

    public void LoadMenu()
    {
        if(googleAds != null)
        {
            googleAds.PlayRewardedVideo();
        }        
        SceneManager.LoadScene("Menu");
    }

    public void UpdateScore(int amount)
    {
        Debug.Log("SCORE: " + score);
        score += amount;
        scoreBoard.text = score.ToString();
        player.score = score;
    }

    public void SaveScore()
    {
        int i = 0;
        int highest = 0;

        while (PlayerPrefs.HasKey(i.ToString()))
        {
            int n = PlayerPrefs.GetInt(i.ToString());
            if (n > highest)
            {
                highest = n;
            }
            i++;
        }

        UpdateHighscore(score > highest ? true : false);
        PlayerPrefs.SetInt(i + "", score);
    }

    private void UpdateHighscore(bool isHighscore)
    {
        PlayerPrefs.SetInt("highscore", score);
    }
}
