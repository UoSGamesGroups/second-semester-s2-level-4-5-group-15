using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    private const float COMBO_RESET_TIME = 5.0f;
    private const int COMBO_MAX = 3;
    private const int COMBO_NONE = 0;
    private int combo;

    private const int MULTIPLIER_MAX = 5;
    private const int MULTIPLIER_MIN = 1;
    private int multiplier = MULTIPLIER_MIN;

    private Timer timer;

    [SerializeField]
    private Faction faction;

    private int score;

    private void Start()
    {
        timer = new Timer(COMBO_RESET_TIME);
    }

	private void Update ()
	{
        // If the combo time out has expired, reset the combo counter and the Player's multiplier
	    if (timer.hasElapsed())
	    {
            multiplier = MULTIPLIER_MIN;
            combo = COMBO_NONE;
            timer.reset();
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If the trigger is not a GameObject tagged "Destructable" then discontinue
        if (!collision.gameObject.CompareTag("Destructable"))
        {
            return;
        }

        // Obtain a reference to the collided GameObject's Destructable script (All objects tagged as "Destructable" have this component)
        var destructableObject = collision.gameObject.GetComponent<Destructable>();

        // If the "Destructable" has already been collided with and processed, discontinue.
        if (!destructableObject.isActive())
        {
            return;
        }

        // If the "Destructable" is neither a member of our Faction or NEUTRAL then discontinue
        var objectFaction = destructableObject.getFaction();
        if (objectFaction != Faction.NEUTRAL && objectFaction != faction)
        {
            return;
        }

        // Set the "Destructable" GameObject's Active flag to false so it will not be processed again
        destructableObject.setActive(false);

        // Increase the players score
        score += destructableObject.getValue() * multiplier;

        // Add one to the combo counter
        combo++;

        // If the player has accumulated a high enough combo, increase the multiplier level
        if (combo >= multiplier * COMBO_MAX && multiplier < MULTIPLIER_MAX)
        {
            multiplier++;
        }

        // Reset the combo time out clock
        timer.reset();
    }

    public int getScore()
    {
        return score;
    }

    public Faction getFaction()
    {
        return faction;
    }

}
