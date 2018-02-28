﻿using UnityEngine;
using UnityEngine.AI;

public class Enemy : Character {

    GameObject player;
    EnemySpawner spawner;
    ParticleSystem damageEffect;
    float coolDown = 0;
    float damageRate = 1f;
    internal static bool playerNoticed;

    void Awake()
    {
        health = 100;
        spawner = GameObject.FindWithTag("EnemySpawner").GetComponent<EnemySpawner>();
        damageEffect = GetComponentInChildren<ParticleSystem>();
        damage = 20f;
        player = GameObject.FindWithTag("Player");
        playerNoticed = false;
    }
	
	void FixedUpdate ()
    {
        coolDown -= Time.deltaTime;
        CheckLifeStatus();
    }

    void CheckLifeStatus()
    {
        if (health <= 0)
        {
            Die(gameObject);
            spawner.SpawnEnemy();
            spawner.EnemyDied();
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<Player>())
        {
            Player target = other.gameObject.GetComponent<Player>();
            if (coolDown < 0)
            {
                target.Hurt(damage);
                coolDown = damageRate;
            }
        }
    }

    public void TakeDamage(int amount)
    {
        playerNoticed = true;
        health -= amount;
        damageEffect.Play();
    }
}
