using UnityEngine;

public class Enemy : MonoBehaviour {

    public GameObject player;
    float speed = 2f;
    public float health;
    bool nearPlayer;
    public EnemySpawner spawner;

    void Awake() {
        health = 100;
        nearPlayer = false;
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
            Destroy(this.gameObject);
            spawner.SpawnEnemy();
        }
    }

    void MoveToPlayer()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step);
    }



    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Trigger enter");
        if (other.gameObject.GetComponent<Player>())
        {
            nearPlayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Debug.Log("Trigger exit");
        if (other.gameObject.GetComponent<Player>())
        {
            nearPlayer = false;
        }
    }
}
