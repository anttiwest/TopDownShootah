using UnityEngine;

public class Player : Character {

    public GameObject player;
    Vector3 movementDirection;
    ParticleSystem shootParticles;
    bool isShooting;
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    float coolDown = 0;
    float fireRate = 0.3f;
    float shotSpeed = 30f;
    ParticleSystem damageEffect;
    float jumpForce;
    Vector3 jumpMovement;
    Rigidbody playerRigidbody;
    bool isGrounded;
    Quaternion playerRotation;

    private void Awake()
    {
        shootParticles = GetComponentInChildren<ParticleSystem>();
        isShooting = false;
        health = 100f;
        damageEffect = GetComponentInChildren<ParticleSystem>();
        jumpForce = 25f;
        playerRigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        coolDown -= Time.deltaTime;

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Move(h, v);
        Turn();

        if (Input.GetKeyDown("space") && isGrounded)
        {
            
            Jump(h, v);
        }

        if (!isGrounded)
        {
            playerRigidbody.AddForce(new Vector3(0, -50, 0));
            transform.rotation = new Quaternion(0, transform.rotation.y, 0, transform.rotation.w);
        }

        if (Input.GetMouseButton(0))
        {
            if (coolDown < 0)
            {
                ShootObjects();
                coolDown = fireRate;
            }
        }
        CheckLifeStatus();
    }

    void Jump(float h, float v)
    {
        jumpMovement.Set(h, jumpForce, v);
        playerRigidbody.velocity += jumpMovement;
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
        bullet.GetComponent<Rigidbody>().velocity = transform.forward * shotSpeed;
        Destroy(bullet, 2f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<OutOfBounds>())
        {
            transform.position.Set(0, 2, 0);
        }
    }

    public void Hurt(float amount)
    {
        TakeDamage(amount);
        damageEffect.Play();
    }

    void CheckLifeStatus()
    {
        if(health <= 0)
        {
            Die(gameObject);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.isStatic)
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.isStatic)
        {
            isGrounded = false;
        }
    }
}
