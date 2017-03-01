using System.Collections.Generic;
using UnityEngine;

public enum Faction { NEUTRAL, PINK, PURPLE };

public class Destructable : MonoBehaviour
{
	private readonly Color32 clrPink = new Color32 (255, 0, 255, 255);//new Color32(255, 0, 255, 255);
	private readonly Color32 clrPurple = new Color32 (117, 0, 173, 255);//new Color32(155, 0, 255, 255);
    private readonly Color32 clrNeutral = new Color32(255, 255, 255, 255);

    private const int DEFAULT_VALUE = 1000;

    [SerializeField]
    private int objectValue = DEFAULT_VALUE;

    [SerializeField]
    private Faction faction;

    [SerializeField]
    private float minForce;

    [SerializeField]
    private float maxForce;

    private bool active;
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

    public void setFaction(Faction faction)
    {
        var colour = clrNeutral;
        switch (faction) {
            case Faction.PINK:
                this.faction = Faction.PINK;
                colour = clrPink;
                break;
            case Faction.PURPLE:
                this.faction = Faction.PURPLE;
                colour = clrPurple;
                break;
        }
        foreach (var child in getChildren())
        {
            // Set all child SpriteRenderer's colour to our Faction's colour
            if (child.GetComponent<SpriteRenderer>() != null)
            {
                child.GetComponent<SpriteRenderer>().color = colour;
            }
        }
    }

    private void Start()
    {
        foreach (var child in getChildren())
        {
            // Set all child Rigidbody2D's to be kinematic 
            if (child.GetComponent<Rigidbody2D>() != null)
            {
                child.GetComponent<Rigidbody2D>().isKinematic = true;
            }
        }

        // Flag ourselves as active
        active = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If we are flagged inactive or the collision was not tagged "Player" then discontinue
        if (destroyed || !collision.gameObject.CompareTag("Player"))
        {
            return;
        }

        // If the Player is not a member of our Faction and we are not Neutral then discontinue
        var playerFaction = collision.GetComponent<PlayerScore>().getFaction();
        if (playerFaction != faction && faction != Faction.NEUTRAL) {
            return;
        }

        // Set all child Rigidbody2D's to be non kinematic and apply a random force to them
        foreach (var child in getChildren())
        {
            var rb = child.GetComponent<Rigidbody2D>();
            if (rb == null) continue;

            rb.isKinematic = false;
            addRandomForce(ref rb, minForce, maxForce, ForceMode2D.Impulse);         
        }

        destroyed = true;
    }

    public int getValue()
    {
        return objectValue;
    }

    public Faction getFaction()
    {
        return faction;
    }

    public bool isActive()
    {
        return active;
    }

    public bool isDestroyed()
    {
        return destroyed;
    }

    public void setActive(bool state)
    {
        active = state;
    }
}

