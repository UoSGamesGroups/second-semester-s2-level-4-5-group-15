using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    public float maxRayDistance;
    public float maxHangDistance; 

    public LineRenderer lineRenderer;
    public LayerMask layerMask;
    public List<string> grappleableTags;

    private DistanceJoint2D distanceJoint;

    private void Start()
    {
        // Create and initialize the distance joint
        distanceJoint = gameObject.AddComponent<DistanceJoint2D>();
        distanceJoint.distance = maxHangDistance;
        distanceJoint.autoConfigureDistance = false;
        distanceJoint.maxDistanceOnly = true;
        distanceJoint.enableCollision = true;

        lineRenderer.startWidth = transform.localScale.x;
        lineRenderer.endWidth = transform.localScale.x;


        // Disable the distance joint and line renderer
        distanceJoint.enabled = false;
        lineRenderer.enabled = false;
    }

    /// <summary>
    /// Sends a Ray in the target direction at maxRayDistance. If a collision occurs within the set Layer Mask, the hook is attatched.
    /// </summary>
    /// <param name="origin">The origin point of the grapple</param>
    /// <param name="target">The target point of the grapple</param>
    public void attatch(Vector3 origin, Vector3 target)
    {
        // Send out a Ray
        var rayHit = Physics2D.Raycast(origin, target, maxRayDistance, layerMask);

        // If the Ray hit nothing, or its target did not have a tag found in grappleableTags then discontinue here
        if (rayHit.collider == null || !grappleableTags.Contains(rayHit.transform.tag))
        {
            return;
        }

        // If the target has no Rigidbody2D then discontinue
        if (!rayHit.collider.gameObject.GetComponent<Rigidbody2D>())
        {
            return;
        }

        // Connect to the target and enable the distance joint
        distanceJoint.connectedBody = rayHit.collider.gameObject.GetComponent<Rigidbody2D>();
        distanceJoint.connectedAnchor = distanceJoint.connectedBody.transform.InverseTransformPoint(rayHit.point);
        distanceJoint.enabled = true;

        // Update the line renders position, material scale and then enable it  
        lineRenderer.SetPosition(0, origin);
        lineRenderer.SetPosition(1, rayHit.point);
        lineRenderer.material.mainTextureScale = new Vector2(Vector3.Distance(origin, rayHit.point), 5.0f);
        lineRenderer.enabled = true;
    }

    /// <summary>
    /// Detatch the hook
    /// </summary>
    public void detatch()
    {
        distanceJoint.enabled = false;
        lineRenderer.enabled = false;
    }

    /// <summary>
    /// Update the origin point of the hook
    /// </summary>
    public void updateOriginPoint(Vector3 origin)
    {
        lineRenderer.SetPosition(0, origin);
    }
}
