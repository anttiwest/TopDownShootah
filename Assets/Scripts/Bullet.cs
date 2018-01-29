using UnityEngine;

public class Bullet : MonoBehaviour {

    float speed;
    float distanceTravelled;

    private void Awake()
    {
        speed = 50;
        distanceTravelled = 0;
        Shoot();
    }

    void FixedUpdate()
    {
        if (distanceTravelled >= 5)
        {
            Destroy(this.gameObject);
        }
    }

    public void Shoot()
    {
        BulletSpawnPoint spawnPoint = GetComponentInChildren<BulletSpawnPoint>();
        transform.position = spawnPoint.transform.position;
        float step = Time.deltaTime * speed;
        Vector3 target = new Vector3(Input.mousePosition.x, 1, Input.mousePosition.z);
        transform.position = Vector3.MoveTowards(transform.position, target, step);

        distanceTravelled = Vector3.Distance(GetComponentInParent<Player>().transform.position, transform.position);
        Debug.Log(distanceTravelled);
    }
}
