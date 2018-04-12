using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : Enemy {
    
    bool nearPlayer;
    NavMeshAgent meshNavigator;
    float distanceToPlayer;

    private void Awake()
    {
        nearPlayer = false;
        speed = 4f;
        meshNavigator = GetComponent<NavMeshAgent>();
    }

    private void FixedUpdate()
    {
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        LookForPlayer();
    }

    void LookForPlayer()
    {
        if (playerNoticed)
        {
            transform.LookAt(player.transform);
            if (!nearPlayer)
            {
                MoveToPlayer();
            }
        }

        if (distanceToPlayer <= 10)
        {
            playerNoticed = true;
            MoveToPlayer();
        }

        if(distanceToPlayer >= 20 && playerNoticed)
        {
            playerNoticed = false;
        }

        if (!playerNoticed && transform.position != spawnLocation)
        {
            MoveToSpawnLocation();
        }
    }

    void MoveToPlayer()
    {
        meshNavigator.destination = player.transform.position;
    }

    void MoveToSpawnLocation()
    {
        meshNavigator.destination = spawnLocation;
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
