using UnityEngine;

public class Crosshair : MonoBehaviour
{
    public Transform parent;
    public float rotationSpeed;

    public KeyCode keyLeft;
    public KeyCode keyRight;

    /// <summary>
    /// Return the direction in which the crosshair is facing as a normalized Vector2
    /// </summary>
    /// <returns></returns>
    public Vector2 getDirection()
    {
        return transform.up;
    }

    // Update is called once per frame
    private void Update ()
    {
        if (Input.GetKey(keyLeft))
        {
            transform.RotateAround(parent.position, Vector3.forward, rotationSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(keyRight))
        {
            transform.RotateAround(parent.position, Vector3.back, rotationSpeed * Time.deltaTime);
        }
	}
}
