using UnityEngine;

public class Enemy : MonoBehaviour {

    public GameObject player;
    float speed = 2f;
    Vector3 direction;

    void Awake() {
        Debug.Log("Enemy spawned");
	}
	
	void FixedUpdate () {
        transform.LookAt(player.transform);
        MoveToPlayer();
	}

    void MoveToPlayer()
    {
        direction = player.transform.position;
        transform.position += Time.deltaTime * speed * direction.normalized;
        Debug.Log(direction);
    }
}
