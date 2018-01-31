using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public GameObject enemyPrefab;
    public GameObject player;
    int enemiesAlive;

    public void SpawnEnemy()
    {
        float maxRange = 5f;
        float minRange = 1f;

        int amount = Random.Range(1, 5);
        if (enemiesAlive < 4)
        {
            
            for (int i = 0; i < amount; i++)
            {
                float randomX;
                float randomZ;

                if (amount < (maxRange / 2))
                {
                    randomX = Random.Range(player.transform.position.x, player.transform.position.x + 10f);
                    randomZ = Random.Range(player.transform.position.z, player.transform.position.z + 10f);
                }
                else
                {
                    randomX = Random.Range((player.transform.position.x * (-1)), player.transform.position.x - 10f);
                    randomZ = Random.Range((player.transform.position.z * (-1)), player.transform.position.z - 10f);
                }
                
                Vector3 spawnLocation = new Vector3(randomX, player.transform.position.y, randomZ);
                GameObject enemy = Instantiate(enemyPrefab, spawnLocation, player.transform.rotation);
                enemiesAlive++;
            }
        }
        //Debug.Log("spawned, enemies alive: " + enemiesAlive);
    }

    public void EnemyDied()
    {
        enemiesAlive--;
        //Debug.Log("died, enemies alive: " + enemiesAlive);
    }
}
