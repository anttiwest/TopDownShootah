using UnityEngine;

public class PlayerMovement : Player {


    float jumpForce;
    Vector3 jumpMovement;
    Vector3 movementDirection;
    bool isGrounded;
    Rigidbody playerRigidbody;
    float sprintSpeed;

    //stamina
    internal float stamina;
    internal float maxStamina = 100f;
    float staminaRegenTimer = 0.0f;
    const float staminaDecreasePerFrame = 30.0f;
    const float staminaIncreasePerFrame = 20.0f;
    const float staminaTimeToRegen = 3.0f;


    private void Awake()
    {
        speed = 10f;
        sprintSpeed = 15f;
        jumpForce = 25f;
        playerRigidbody = GetComponent<Rigidbody>();
        stamina = 100f;
    }

    private void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Move(h, v);
        Turn();
        
        transform.rotation = new Quaternion(0, transform.rotation.y, 0, transform.rotation.w);

        if (Input.GetKeyDown("space") && isGrounded && stamina >= 50)
        {
            Jump(h, v);
            stamina -= 50f;
        }

        if (!isGrounded)
        {
            playerRigidbody.AddForce(new Vector3(0, -50, 0));
            transform.rotation = new Quaternion(0, transform.rotation.y, 0, transform.rotation.w);
        }
    }

    void Jump(float h, float v)
    {
        jumpMovement.Set(h, jumpForce, v);
        playerRigidbody.velocity += jumpMovement;
    }

    void Move(float h, float v)
    {
        if(Input.GetKey(KeyCode.LeftShift) && stamina >= 10)
        {
            speed = sprintSpeed;
            DecreaseStamina();
        }
        else if(stamina < maxStamina)
        {
            speed = 10f;
            if (staminaRegenTimer <= staminaTimeToRegen)
            {
                IncreaseStamina();
            }
        }
        else
        {
            staminaRegenTimer += Time.deltaTime;
            speed = 10f;
        }
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

    public void DecreaseStamina()
    {
        stamina = Mathf.Clamp(stamina - (staminaDecreasePerFrame * Time.deltaTime), 0.0f, maxStamina);
        staminaRegenTimer = 0.0f;
    }

    public void IncreaseStamina()
    {
        stamina = Mathf.Clamp(stamina + (staminaIncreasePerFrame * Time.deltaTime), 0.0f, maxStamina);
    }
}
