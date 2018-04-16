using UnityEngine;

public class InvisbleCollider : MonoBehaviour {
    
	void Start () {
        Destroy(gameObject.GetComponent<MeshRenderer>());
	}
}
