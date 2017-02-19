using UnityEngine;

public class GameState : MonoBehaviour
{

    private static GameState instance;

    private int roundsLeft;

    private Faction winner;
    private int pinkScore;
    private int purpleScore;

    public GameState getInstance()
    {
        return instance;
    }

    public void setRoundsLeft(int rounds)
    {
        roundsLeft = rounds;
    }

    public int getRoundsLeft()
    {
        return roundsLeft;
    }

    public int getPinkScore()
    {
        return pinkScore;
    }

    public int getPurpleScore()
    {
        return purpleScore;
    }

    public Faction getWinner()
    {
        return winner;
    }

    public void setWinnner(Faction faction, int pinkScore, int purpleScore)
    {
        winner = faction;
        this.pinkScore = pinkScore;
        this.purpleScore = purpleScore;
    }

    private void reset()
    {
        pinkScore = 0;
        purpleScore = 0;
        roundsLeft = 0;
        winner = Faction.NEUTRAL;
    }

    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            reset();
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        Debug.Log(roundsLeft);
    }
}
