using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaHandler : MonoBehaviour {

    //stamina
    //internal float stamina;
    //internal float maxStamina = 100f;
    float staminaRegenTimer = 0.0f;
    const float staminaDecreasePerFrame = 30.0f;
    const float staminaIncreasePerFrame = 20.0f;
    const float staminaTimeToRegen = 3.0f;

    public float Regen(float stamina, float maxStamina)
    {
        if (stamina < maxStamina)
        {
            if (staminaRegenTimer <= staminaTimeToRegen)
            {
                stamina = Mathf.Clamp(stamina + (staminaIncreasePerFrame * Time.deltaTime), 0.0f, maxStamina);
            }
        }
        else
        {
            staminaRegenTimer += Time.deltaTime;
        }
        return stamina;
    }

    public float Drain(float stamina, float maxStamina)
    {
        stamina = Mathf.Clamp(stamina - (staminaDecreasePerFrame * Time.deltaTime), 0.0f, maxStamina);
        staminaRegenTimer = 0.0f;
        return stamina;
    }
}
