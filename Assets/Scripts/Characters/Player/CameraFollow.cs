using UnityEngine;

public class CameraFollow : MonoBehaviour {

    GameObject player;
    Vector3 distance;
    float smoothing = 10f;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        distance = transform.position - player.transform.position;
        
    }

    void FixedUpdate ()
    {
        if (player)
        {
            LookAtPlayer();
        }
    }

    void LookAtPlayer()
    {
        Vector3 targetPosition = player.transform.position + distance;
        transform.position = Vector3.Lerp(targetPosition, player.transform.position, smoothing * Time.deltaTime);
    }
}
