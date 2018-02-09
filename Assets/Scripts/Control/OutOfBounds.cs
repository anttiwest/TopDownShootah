using UnityEngine;

public class OutOfBounds : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        GameObject obj = other.gameObject;
        Destroy(obj);
    }
}
