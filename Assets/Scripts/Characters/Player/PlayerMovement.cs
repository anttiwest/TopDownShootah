using UnityEngine;

public class PlayerMovement : Player {


    float jumpForce;
    Vector3 jumpMovement;
    Vector3 movementDirection;
    bool isGrounded;
    Rigidbody playerRigidbody;

    private void Awake()
    {
        speed = 10f;
        jumpForce = 25f;
        playerRigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Move(h, v);
        Turn();
        
        transform.rotation = new Quaternion(0, transform.rotation.y, 0, transform.rotation.w);

        //if (Input.GetKeyDown("space") && isGrounded)
        //{
        //    Jump(h, v);
        //}

        //if (!isGrounded)
        //{
        //    playerRigidbody.AddForce(new Vector3(0, -50, 0));
        //    transform.rotation = new Quaternion(0, transform.rotation.y, 0, transform.rotation.w);
        //}
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
