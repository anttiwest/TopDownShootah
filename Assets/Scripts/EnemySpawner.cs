using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public GameObject enemyPrefab;
    public GameObject player;

	public void SpawnEnemy()
    {
        float randomX = Random.Range(player.transform.position.x, player.transform.position.x + 10f);
        float randomZ = Random.Range(player.transform.position.z, player.transform.position.z + 10f);
        Vector3 spawnLocation = new Vector3(randomX, player.transform.position.y, randomZ);
        GameObject enemy = Instantiate(enemyPrefab, spawnLocation, player.transform.rotation);
        Debug.Log("spawned");
    }
}
