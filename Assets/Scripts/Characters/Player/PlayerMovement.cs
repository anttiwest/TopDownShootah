﻿using UnityEngine;
using UnityEngine.EventSystems;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMovement : Player {


    float jumpForce;
    Vector3 jumpMovement;
    Vector3 movementDirection;
    bool isGrounded;
    Rigidbody playerRigidbody;
    float sprintSpeedMultip;
    Quaternion currentRotation;


    //stamina
    internal float stamina;
    internal float maxStamina = 100f;
    float staminaRegenTimer = 0.0f;
    const float staminaDecreasePerFrame = 30.0f;
    const float staminaIncreasePerFrame = 20.0f;
    const float staminaTimeToRegen = 3.0f;

    Joystick leftJoystick;

    private void Awake()
    {
        speed = 10f;
        sprintSpeedMultip = 1.5f;
        jumpForce = 25f;
        playerRigidbody = GetComponent<Rigidbody>();
        stamina = 100f;
        leftJoystick = GameObject.Find("LeftJoyStick").GetComponent<Joystick>();
    }

    private void FixedUpdate()
    {
        bool jumpPressed = false;
#if UNITY_ANDROID
        float h = CrossPlatformInputManager.GetAxis("HorizontalLeft");
        float v = CrossPlatformInputManager.GetAxis("VerticalLeft");
        
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

        if (jumpPressed && isGrounded && stamina >= 50)
        {
            Jump(h, v);
        }
        TurnMobile();
#elif UNITY_IPHONE
        float h = CrossPlatformInputManager.GetAxis("Horizontal");
        float v = CrossPlatformInputManager.GetAxis("Vertical");
        bool jumpPressed = false;
        TurnMobile();
#else
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        bool jumpPressed = Input.GetKeyDown("space");
        Turn();
        if (jumpPressed && isGrounded && stamina >= 50)
        {
            Jump(h, v);
        }
#endif
        Move(h, v);
        transform.rotation = new Quaternion(0, transform.rotation.y, 0, transform.rotation.w);

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
        stamina -= 50f;
    }

    void Move(float h, float v)
    {
        if(Input.GetKey(KeyCode.LeftShift) && stamina >= 10)
        {
            speed = speed * sprintSpeedMultip;
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
