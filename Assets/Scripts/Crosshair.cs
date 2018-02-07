using UnityEngine;

public class Crosshair : MonoBehaviour {
    
    public Texture3D crosshairImage;
    Rect position;
    Vector2 hitpos;
    Vector2 size;

    void Start()
    {
        size = new Vector2((crosshairImage.width / 10), (crosshairImage.height / 10));
    }

    private void OnGUI()
    {
        GUI.DrawTexture(position, crosshairImage);
    }

    void Update () {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            hitpos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            position = new Rect(hitpos, size);
            Debug.Log("hitpos: " + hitpos);
        }
    }
}
