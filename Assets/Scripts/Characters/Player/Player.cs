﻿using UnityEngine;

public enum Equipped
{
    Melee,
    Ranged
};

public class Player : Character {
    
    ParticleSystem damageEffect;
    internal Equipped equipped;
    GoogleMobileAdverts googleMobileAds;

    private void Awake()
    {
        damageEffect = GetComponentInChildren<ParticleSystem>();
        health = 100f;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<OutOfBounds>())
        {
            transform.position.Set(0, 2, 0);
        }
    }

    public void Hurt(float amount)
    {
        TakeDamage(amount);
        damageEffect.Play();
    }
}
