using UnityEngine;

public class Player : MonoBehaviour {

    public GameObject player;
    public float speed;
    Vector3 movementDirection;
    BulletSpawnPoint bulletSpawnPoint;
    Bullet bullet;

    private void Awake()
    {
        bulletSpawnPoint = GetComponentInChildren<BulletSpawnPoint>();
    }

    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Move(h, v);
        Turn();

        if (Input.GetMouseButton(0))
        {
            Fire();
        }
    }

    void Move(float h, float v)
    {
        movementDirection.Set(h, 0, v);
        movementDirection = speed * Time.deltaTime * movementDirection.normalized;
        transform.position += movementDirection;
    }

    void Turn()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit info;

        if (Physics.Raycast(ray, out info))
        {
            Vector3 positionToLookAt = info.point;
            positionToLookAt.y = 0f;
            transform.LookAt(positionToLookAt);
        }
    }

    void Fire()
    {
        Bullet.Instantiate(bullet, bulletSpawnPoint.transform.position, Quaternion.identity);
    }

    void Die()
    {

    }
}
