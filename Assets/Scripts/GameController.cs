using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    GameObject player;

    void Awake()
    {
        player = GameObject.FindWithTag("Player");
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
        SceneManager.LoadScene("scene001");
    }
}
