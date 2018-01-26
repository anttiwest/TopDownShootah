using UnityEngine;

public class Player : MonoBehaviour {

    public GameObject player;
    public float speed;
    Vector3 movementDirection;
    float camRayLength = 100f;

    void Awake()
    {
        
    }

    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Move(h, v);
        Turn();
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
        Debug.DrawRay(ray.origin, 100 * ray.direction);
    }
}
