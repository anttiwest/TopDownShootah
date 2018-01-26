using UnityEngine;

public class Enemy : MonoBehaviour {

    public GameObject player;
    float speed = 2f;
    Vector3 direction;
    public float Hp;
    bool nearPlayer;

    void Awake() {
        Hp = 100;
        nearPlayer = false;
	}
	
	void FixedUpdate () {
        transform.LookAt(player.transform);

        if (!nearPlayer)
        {
            MoveToPlayer();
        }
        Debug.Log("is nearPlayer: " + nearPlayer);
    }

    void MoveToPlayer()
    {
        direction = player.transform.position;
        transform.position += Time.deltaTime * speed * direction.normalized;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger enter");
        if (other.gameObject.GetComponent<Player>())
        {
            nearPlayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Trigger exit");
        if (other.gameObject.GetComponent<Player>())
        {
            nearPlayer = false;
        }
    }
}
