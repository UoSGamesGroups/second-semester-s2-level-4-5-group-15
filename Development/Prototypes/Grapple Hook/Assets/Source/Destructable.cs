using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour
{
    public bool destroyObjects;
    public float destroyTime;

    public float minForce;
    public float maxForce;

    private bool destroyed;

    /// <summary>
    /// Returns a list containing the GameObject's children
    /// </summary>
    private IEnumerable<GameObject> getChildren()
    {
        var children = new List<GameObject>();
        for (var i = 0; i < transform.childCount; i++)
        {
            children.Add(transform.GetChild(i).gameObject);
        }
        return children;
    }

    /// <summary>
    /// Add a random force to a Rigidbody2D within an inclusive random range
    /// </summary>
    /// <param name="rb">The Rigidbody2D to add force to</param>
    /// <param name="min">The minimum force range</param>
    /// <param name="max">The maximum force range</param>
    /// <param name="forceMode">How the force should be applied</param>
    private static void addRandomForce(ref Rigidbody2D rb, float min, float max, ForceMode2D forceMode)
    {
        rb.AddForce(new Vector2(Random.Range(min, max), Random.Range(min, max)), forceMode);
    }

    /// <summary>
    /// Destroys a GameObject and all of its children after the given time has passed
    /// </summary>
    /// <param name="pause">The time to wait before destroying (in seconds)</param>
    /// <returns></returns>
    private IEnumerator destroyAfter(float pause)
    {
        yield return new WaitForSeconds(pause);
        foreach (var child in getChildren())
        {
            Destroy(child);
        }
        Destroy(gameObject);
    }

    private void Start()
    {
        foreach (var child in getChildren())
        {
            if (child.GetComponent<Rigidbody2D>() != null)
            {
                // Set all child Rigidbody2D's to be kinematic 
                child.GetComponent<Rigidbody2D>().isKinematic = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (destroyed || !collision.gameObject.CompareTag("Player"))
            return;

        foreach (var child in getChildren())
        {
            var rb = child.GetComponent<Rigidbody2D>();
            if (rb == null) continue;

            // Set all child Rigidbody2D's to be non kinematic and apply a random force to them
            rb.isKinematic = false;
            addRandomForce(ref rb, minForce, maxForce, ForceMode2D.Impulse);         
        }

        if (destroyObjects)
        {
            StartCoroutine(destroyAfter(destroyTime));
        }

        destroyed = true;
    }
}

