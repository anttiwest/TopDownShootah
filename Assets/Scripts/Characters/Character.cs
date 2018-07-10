using UnityEngine;

public class Character : MonoBehaviour {

    internal float speed;
    internal float health;
    internal float damage;
    internal long score;
    ScoreCounter scoreCounter;
    internal GameController gameController;
    internal EnemySpawner spawner;

    private void Start()
    {
        spawner = GameObject.FindWithTag("EnemySpawner").GetComponent<EnemySpawner>();
        scoreCounter = new ScoreCounter();
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
    }

    private void Update()
    {
        CheckLifeStatus();
    }


    void CheckLifeStatus()
    {
        if (health <= 0)
        {
            if (gameObject.GetComponent<Enemy>())
            {
                spawner.SpawnEnemy();
                spawner.EnemyDied();
            }

            if (gameObject.GetComponent<Player>())
            {
                gameController.SaveScore();
            }
            Destroy(gameObject);
        }
    }
    public void TakeDamage(float amount)
    {
        health -= amount;
    }
}
