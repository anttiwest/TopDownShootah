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
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //RaycastHit hit;

        //if (Physics.Raycast(ray, out hit))
        //{
        //    crosshairPos = new Vector2(hit.point.x, hit.point.y);
        //    position = new Rect(crosshairPos, size);
        //}

        crosshairPos = new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y) - 0.5f * size;
        position = new Rect(crosshairPos, size);

        Debug.Log("hitpos: " + crosshairPos);
    }
}
