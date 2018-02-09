using UnityEngine;

public class Bullet : MonoBehaviour {

    public int damage;

    private void Awake()
    {
        damage = 20;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Enemy>())
        {
            Enemy enemy = other.GetComponent<Enemy>();
            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }
    }

}
