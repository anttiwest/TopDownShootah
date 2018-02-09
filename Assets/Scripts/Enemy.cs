using UnityEngine;
using UnityEngine.AI;

public class Enemy : Character {

    GameObject player;
    bool nearPlayer;
    EnemySpawner spawner;
    ParticleSystem damageEffect;
    float coolDown = 0;
    float damageRate = 1f;
    NavMeshAgent meshNavigator;

    void Awake() {
        health = 100;
        nearPlayer = false;
        player = GameObject.FindWithTag("Player");
        spawner = GameObject.FindWithTag("EnemySpawner").GetComponent<EnemySpawner>();
        damageEffect = GetComponentInChildren<ParticleSystem>();
        damage = 20f;
        speed = 3f;
        meshNavigator = GetComponent<NavMeshAgent>();
    }
	
	void FixedUpdate () {
        
        coolDown -= Time.deltaTime;

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
            Die(gameObject);
            spawner.SpawnEnemy();
            spawner.EnemyDied();
        }
    }

    void MoveToPlayer()
    {
        meshNavigator.destination = player.transform.position;
        //old movement
        //float step = speed * Time.deltaTime;
        //transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Player>())
        {
            nearPlayer = true;
        }

        if (other.gameObject.GetComponent<OutOfBounds>())
        {
            Die(gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<Player>())
        {
            nearPlayer = true;
            Player target = player.GetComponent<Player>();
            if (coolDown < 0)
            {
                target.Hurt(damage);
                coolDown = damageRate;
            }

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
