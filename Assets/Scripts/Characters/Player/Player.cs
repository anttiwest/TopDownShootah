using UnityEngine;

public class Player : Character {
    
    ParticleSystem damageEffect;

    private void Awake()
    {
        damageEffect = GetComponentInChildren<ParticleSystem>();
        health = 100f;
    }

    void FixedUpdate()
    {
        CheckLifeStatus();
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

    void CheckLifeStatus()
    {
        if(health <= 0)
        {
            Die(gameObject);
        }
    }
}
