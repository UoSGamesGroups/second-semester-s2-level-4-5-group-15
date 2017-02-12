using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public KeyCode keyGrapple;
    public GrapplingHook grapplingHook;
    public Crosshair crosshair;

    private bool sliding;

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

        GetComponent<Animator>().SetBool("sliding", sliding);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        sliding = collision.gameObject.CompareTag("Slide");
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Slide"))
        {
            return;
        }

        sliding = collision.gameObject.CompareTag("Slide");
        var velocity = GetComponent<Rigidbody2D>().velocity;
        GetComponent<SpriteRenderer>().flipX = velocity.x < 0.0f;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        sliding = false;
        GetComponent<SpriteRenderer>().flipX = false;
    }

}
