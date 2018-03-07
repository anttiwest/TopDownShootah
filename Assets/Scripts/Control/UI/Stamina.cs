using UnityEngine;
using UnityEngine.UI;

public class Stamina : MonoBehaviour {

    PlayerMovement playerMvmnt;
    Slider staminaBar;

    void Awake()
    {
        playerMvmnt = GameObject.FindWithTag("Player").GetComponentInChildren<PlayerMovement>();
        staminaBar = GameObject.Find("StaminaBar").GetComponent<Slider>();
        staminaBar.maxValue = playerMvmnt.maxStamina;

    }

    void FixedUpdate()
    {
        staminaBar.value = Mathf.Round(playerMvmnt.stamina);
    }
}
