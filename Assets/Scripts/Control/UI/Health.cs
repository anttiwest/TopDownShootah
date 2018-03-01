using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour {

	Player player;
    Text healthDisplay;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        healthDisplay = GameObject.Find("Health").GetComponent<Text>();
    }

    private void FixedUpdate()
    {
        healthDisplay.text = "+" + player.health;
    }
}
