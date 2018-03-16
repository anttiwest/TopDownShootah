using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public GameObject player;
    GoogleMobileAdverts googleAds;

    void Awake()
    {
        player = GameObject.FindWithTag("Player");
        Cursor.visible = true;
        googleAds = GameObject.Find("AdSystem").GetComponent<GoogleMobileAdverts>();
    }

    void FixedUpdate()
    {
        if (!GameObject.FindWithTag("Player") || Input.GetKeyDown(KeyCode.Escape))
        {
            LoadMenu();
        }
    }

    public void LoadMenu()
    {
        googleAds.PlayRewardedVideo();

        //string scene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("Menu");
    }
}
