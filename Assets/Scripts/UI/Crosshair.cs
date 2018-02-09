using UnityEngine;

public class Crosshair : MonoBehaviour {
    
    public Texture3D crosshairImage;
    Rect position;
    Vector2 crosshairPos;
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
        crosshairPos = new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y) - 0.5f * size;
        position = new Rect(crosshairPos, size);
    }
}
