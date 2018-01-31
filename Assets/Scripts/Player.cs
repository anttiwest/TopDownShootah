using UnityEngine;

public class Player : MonoBehaviour {

    public GameObject player;
    public float speed;
    Vector3 movementDirection;
    ParticleSystem shootParticles;
    bool isShooting;
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    Vector3 positionToLookAt;

    private void Awake()
    {
        shootParticles = GetComponentInChildren<ParticleSystem>();
        isShooting = false;
    }

    void FixedUpdate()
    {
        Move();
        Turn();
        if (Input.GetMouseButton(0))
        {
            ShootObjects();
        }
    }

    void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
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
            positionToLookAt = info.point;
            positionToLookAt.y = 0f;
            transform.LookAt(positionToLookAt);
        }
    }

    void ShootParticles()
    {
        if (Input.GetMouseButton(0) && !isShooting)
        {
            shootParticles.Play();
            isShooting = true;
        }
        else if (isShooting && !Input.GetMouseButton(0))
        {
            shootParticles.Stop();
            isShooting = false;
        }
        shootParticles.transform.position = new Vector3(transform.position.x, shootParticles.transform.position.y, transform.position.z);
    }

    void ShootObjects()
    {
        bulletSpawn.transform.position = new Vector3(transform.position.x, bulletSpawn.transform.position.y, transform.position.z);
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.transform.position, bulletSpawn.transform.rotation);
        bullet.GetComponent<Rigidbody>().velocity = positionToLookAt * 6;
        Destroy(bullet, 2f);
    }
}
