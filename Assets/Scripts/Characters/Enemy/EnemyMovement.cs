using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : Enemy {

    GameObject player;
    bool nearPlayer;
    NavMeshAgent meshNavigator;

    private void Awake()
    {
        nearPlayer = false;
        player = GameObject.FindWithTag("Player");
        speed = 6f;
        meshNavigator = GetComponent<NavMeshAgent>();
    }

    private void FixedUpdate()
    {
        transform.LookAt(player.transform);
        if (!nearPlayer)
        {
            MoveToPlayer();
        }
    }

    void MoveToPlayer()
    {
        meshNavigator.destination = player.transform.position;
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



    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Player>())
        {
            nearPlayer = false;
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<Player>())
        {
            nearPlayer = true;
        }
    }
}
