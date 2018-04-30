using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public GameObject player;
    GoogleMobileAdverts googleAds;

    void Awake()
    {
        player = GameObject.FindWithTag("Player");
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
    }

    public void LoadMenu()
    {
        if(googleAds != null)
        {
            googleAds.PlayRewardedVideo();
        }        
        SceneManager.LoadScene("Menu");
    }
}
