using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    GameObject player;

    void Awake()
    {
        player = GameObject.FindWithTag("Player");
        Cursor.visible = false;
    }

    void FixedUpdate()
    {
        if (!GameObject.FindWithTag("Player"))
        {
            RestartGame();
        }
    }

    public void RestartGame()
    {
        string scene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(scene);
    }
}
