using UnityEngine;

public class Player : MonoBehaviour {

    public GameObject player;
    public float speed;
    Vector3 movementDirection;
    ParticleSystem shootParticles;
    bool isShooting;

    private void Awake()
    {
        shootParticles = GetComponentInChildren<ParticleSystem>();
        isShooting = false;
    }

    void FixedUpdate()
    {
        Move();
        Turn();
        Shoot();
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
            Vector3 positionToLookAt = info.point;
            positionToLookAt.y = 0f;
            transform.LookAt(positionToLookAt);
        }
    }

    void Shoot()
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
}
