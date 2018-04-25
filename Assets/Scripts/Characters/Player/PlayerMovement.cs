using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMovement : Player {

    float jumpForce;
    Vector3 jumpMovement;
    Vector3 movementDirection;
    bool isGrounded;
    Rigidbody playerRigidbody;
    float sprintSpeedMultip;
    Quaternion currentRotation;

    bool isMoving;
    bool isAbleToSprint;
    bool isSprinting;
    Button sprintButton;
    float sprintSpeed;
    float normalSpeed;
    
    internal float stamina;
    internal float maxStamina = 100f;
    StaminaHandler staminaHandler;

    Animator animator;

    bool jumpPressed;
    
    float tapCooldown = 0;
    float tapRate = 0.3f;

    Text jumpDebug;
    float distToGround;
    Collider collider;
    
    private void Awake()
    {
        speed = 5f;
        sprintSpeedMultip = 1.5f;
        sprintSpeed = speed * sprintSpeedMultip;
        normalSpeed = speed;

        jumpForce = 25f;
        playerRigidbody = GetComponent<Rigidbody>();
        
        sprintButton = GameObject.Find("SprintButton").GetComponent<Button>();
        sprintButton.onClick.AddListener(ToggleSprinting);

        stamina = maxStamina;
        staminaHandler = FindObjectOfType<StaminaHandler>();

        isAbleToSprint = true;
        isSprinting = false;

        animator = GetComponentInChildren<Animator>();

        jumpDebug = GameObject.Find("jumpDebug").GetComponent<Text>();
        jumpDebug.text = "jump not pressed";
        collider = GetComponent<BoxCollider>();
    }

    private void FixedUpdate()
    {
        jumpPressed = false;
        float h = CrossPlatformInputManager.GetAxis("HorizontalLeft");
        float v = CrossPlatformInputManager.GetAxis("VerticalLeft");
        tapCooldown -= Time.deltaTime;

        //if (Input.touches.Length > 0)
        //{
        //    bool touchEnded = false;

        //    for(int i = 0; i < Input.touches.Length; i++)
        //    {
        //        if (Input.touches[i].phase == TouchPhase.Ended)
        //        {
        //            touchEnded = true;
        //        }
        //        Debug.Log("touchEnded: " + touchEnded);
        //    }

        //    if (touchEnded)
        //    {
        //        if (tapCooldown > 0) jumpPressed = true;
        //        else tapCooldown = tapRate;
        //    }
        //}

        for (int i = 0; i < Input.touchCount; i++)
        {
            if (Input.GetTouch(i).phase == TouchPhase.Began)
            {
                if (Input.GetTouch(i).tapCount == 2)
                {
                    jumpPressed = true;
                    
                }
            }
        }

        distToGround = collider.bounds.extents.y;
        if (jumpPressed && IsGrounded() && (stamina >= 50))
        {
            jumpDebug.text = jumpDebug.text + ", jumppressed: "+ jumpPressed;
            jumpMovement.Set(h, jumpForce, v);
            playerRigidbody.velocity += jumpMovement;
            stamina -= 50f;
        }

        TurnMobile();

        if (stamina <= 10)
        {
            isAbleToSprint = false;
            ToggleSprinting();
            isSprinting = false;
        }
        else
        {
            isAbleToSprint = true;
        }

        Move(h, v);
        transform.rotation = new Quaternion(0, transform.rotation.y, 0, transform.rotation.w);

        if (!isGrounded)
        {
            playerRigidbody.AddForce(new Vector3(0, -50, 0));
            transform.rotation = new Quaternion(0, transform.rotation.y, 0, transform.rotation.w);
        }

        if (stamina < maxStamina)
        {
            stamina = staminaHandler.Regen(stamina, maxStamina);
        }

        animator.SetBool("isMoving", isMoving);
    }

    void Jump(float h, float v)
    {
        jumpMovement.Set(h, jumpForce, v);
        playerRigidbody.velocity += jumpMovement;
        stamina -= 50f;
    }

    void Move(float h, float v)
    {
        if (h == 0 && v == 0)
        {
            isSprinting = false;
            isMoving = false;
        }
        else
        {
            isMoving = true;
        }

        if (isSprinting)
        {
            speed = sprintSpeed;
            stamina = staminaHandler.Drain(stamina, maxStamina);
        }
        else
        {   
            speed = normalSpeed;
        }

        movementDirection.Set(h, 0, v);
        movementDirection = speed * Time.deltaTime * movementDirection.normalized;
        transform.position += movementDirection;
    }

    void ToggleSprinting()
    {
        if(isAbleToSprint && !isSprinting && isMoving)
        {
            isSprinting = true;
        }
        else
        {
            isSprinting = false;
        }
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

    void TurnMobile()
    {
        float h = CrossPlatformInputManager.GetAxis("HorizontalRight");
        float v = CrossPlatformInputManager.GetAxis("VerticalRight");

        if (h >= 0.5 || h <= -0.5 || v >= 0.5 || v <= -0.5)
        {
            transform.rotation = Quaternion.LookRotation(new Vector3(h, 0f, v));
            currentRotation = transform.rotation;
        }
        else
        {
            transform.rotation = currentRotation;
        }
    }
    
    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
    }
 }

    //private void OnCollisionStay(Collision collision)
    //{
    //    if (collision.gameObject.isStatic)
    //    {
    //        isGrounded = true;
    //    }
    //}

    //private void OnCollisionExit(Collision collision)
    //{
    //    if (collision.gameObject.isStatic)
    //    {
    //        isGrounded = false;
    //    }
    //}

