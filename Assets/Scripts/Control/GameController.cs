using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public GameObject player;

    void Awake()
    {
        player = GameObject.FindWithTag("Player");
        Cursor.visible = true;
    }

    void FixedUpdate()
    {
        if (!GameObject.FindWithTag("Player") || Input.GetKeyDown(KeyCode.Escape))
        {
            RestartGame();
        }
    }

    public void RestartGame()
    {
        //string scene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("Menu");
    }
}
