using UnityEngine;

public class Enemy : MonoBehaviour {

    GameObject player;
    float speed = 4.5f;
    public float health;
    bool nearPlayer;
    EnemySpawner spawner;
    ParticleSystem damageEffect;

    void Awake() {
        health = 100;
        nearPlayer = false;
        player = GameObject.FindWithTag("Player");
        spawner = GameObject.FindWithTag("EnemySpawner").GetComponent<EnemySpawner>();
        damageEffect = GetComponentInChildren<ParticleSystem>();
    }
	
	void FixedUpdate () {

        transform.LookAt(player.transform);
        if (!nearPlayer)
        {
            MoveToPlayer();
        }
        
        CheckLifeStatus();
    }

    void CheckLifeStatus()
    {
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
        spawner.SpawnEnemy();
        spawner.EnemyDied();
    }

    void MoveToPlayer()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Player>())
        {
            nearPlayer = true;
        }

        if (other.gameObject.GetComponent<OutOfBounds>())
        {
            Die();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Player>())
        {
            nearPlayer = false;
        }
        
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        damageEffect.Play();
    }
}
