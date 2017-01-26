using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public KeyCode keyGrapple;
    public GrapplingHook grapplingHook;
    public Crosshair crosshair;

    /// <summary>
    /// Returns a Vector3 containing the current coordinate position of the mouse
    /// </summary>
    private static Vector3 getMousePosition() {
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0.0f;
        return mousePosition;
    }

    private void Update()
    {
        if (Input.GetKey(keyGrapple))
        {
            var origin = transform.position;
            if (Input.GetKeyDown(keyGrapple))
            {
                grapplingHook.attatch(origin, crosshair.getDirection());
            }
            grapplingHook.updateOriginPoint(origin);
        }
        else
        {
            grapplingHook.detatch();
        }
    }
}
