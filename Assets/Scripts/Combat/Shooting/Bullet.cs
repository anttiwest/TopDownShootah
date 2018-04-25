using UnityEngine;

public class Bullet : MonoBehaviour {

    public int damage;

    private void Awake()
    {
        transform.rotation.Set(0,0,0,0);
        damage = 20;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Enemy>())
        {
            Enemy enemy = other.GetComponent<Enemy>();
            enemy.TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
