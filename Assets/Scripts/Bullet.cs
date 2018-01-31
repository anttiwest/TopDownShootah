using UnityEngine;

public class Bullet : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Enemy>())
        {
            Enemy enemy = other.GetComponent<Enemy>();
            enemy.health -= 10;
            Destroy(gameObject);
        }
    }

}
