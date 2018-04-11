using System;
using UnityEngine;
using UnityEngine.EventSystems;
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

    Joystick leftJoystick;

    bool jumpPressed;
    
    float tapCooldown = 0;
    float tapRate = 0.3f;

    private void Awake()
    {
        speed = 10f;
        sprintSpeedMultip = 1.5f;
        sprintSpeed = speed * sprintSpeedMultip;
        normalSpeed = speed;

        jumpForce = 25f;
        playerRigidbody = GetComponent<Rigidbody>();

        leftJoystick = GameObject.Find("LeftJoyStick").GetComponent<Joystick>();
        sprintButton = GameObject.Find("SprintButton").GetComponent<Button>();
        sprintButton.onClick.AddListener(ToggleSprinting);

        stamina = maxStamina;
        staminaHandler = FindObjectOfType<StaminaHandler>();

        isAbleToSprint = true;
        isSprinting = false;
    }

    private void FixedUpdate()
    {

        jumpPressed = false;
        float h = CrossPlatformInputManager.GetAxis("HorizontalLeft");
        float v = CrossPlatformInputManager.GetAxis("VerticalLeft");

        tapCooldown -= Time.deltaTime;

        if (Input.touches.Length > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                if (tapCooldown > 0) jumpPressed = true;
                else tapCooldown = tapRate;
            }
        }

        if (jumpPressed && isGrounded && stamina >= 50)
        {
            Jump(h, v);
        }

        TurnMobile();

        if (stamina <= 0)
        {
            isAbleToSprint = false;
            ToggleSprinting();
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
