using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour {

    public GameObject enemyPrefab;
    private GameObject player;
    int enemiesAlive;
    int score;
    Canvas ui;
    Text scoreBoard;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        ui = GameObject.FindWithTag("UI").GetComponentInChildren<Canvas>();
        score = 0;
        scoreBoard = ui.GetComponentInChildren<Text>();
        scoreBoard.text = score.ToString();
    }

    public void SpawnEnemy()
    {
        int maxRange = 5;
        int minRange = 1;

        int amount = Random.Range(minRange, maxRange);
        if (enemiesAlive < 4)
        {
            for (int i = 0; i < amount; i++)
            {
                float randomX;
                float randomZ;

                if (amount < ((maxRange * 1.0f) / 2))
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
        Debug.Log("spawned, enemies alive: " + enemiesAlive);
    }

    public void EnemyDied()
    {
        enemiesAlive--;
        score += 10;
        scoreBoard.text = score.ToString();
    }
}
