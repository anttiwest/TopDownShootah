using UnityEngine;

public class Character : MonoBehaviour {

    internal float speed;
    internal float health;
    internal float damage;

    public void Die(GameObject target)
    {
        if (target.GetComponent<Enemy>())
        {
            Destroy(target);
        }

        if (target.GetComponent<Player>())
        {
            Destroy(target);
        }
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
    }
}
