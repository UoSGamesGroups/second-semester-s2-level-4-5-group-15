using UnityEngine;

public class GameState : MonoBehaviour
{

    private static GameState instance_;

    private int rounds_left_;

    private Faction winner_;
    private int pink_score_;
    private int purple_score_;

    private int pink_rounds_won_;
    private int pink_rounds_won_prev_;

    private int purple_rounds_won_;
    private int purple_rounds_won_prev_;

    public void set_pink_score(int score)
    {
        pink_score_ = score;
    }

    public void set_purple_score(int score)
    {
        purple_score_ = score;
    }

    public void set_round_winner(Faction faction)
    {
        pink_rounds_won_prev_ = pink_rounds_won_;
        purple_rounds_won_prev_ = purple_rounds_won_;
        switch (faction)
        {
            case Faction.PINK:
                pink_rounds_won_++;
                break;
            case Faction.PURPLE:
                purple_rounds_won_++;
                break;
        }
    }

    public int pink_rounds_won_prev()
    {
        return pink_rounds_won_prev_;
    }

    public int purple_rounds_won_prev()
    {
        return purple_rounds_won_prev_;
    }

    public int pink_rounds_won()
    {
        return pink_rounds_won_;
    }

    public int purple_rounds_won()
    {
        return purple_rounds_won_;
    }

    public GameState get_instance()
    {
        return instance_;
    }

    public void set_rounds_left(int rounds)
    {
        rounds_left_ = rounds;
    }

    public int rounds_left()
    {
        return rounds_left_;
    }

    public int pink_score()
    {
        return pink_score_;
    }

    public int purple_score()
    {
        return purple_score_;
    }

    public Faction winner()
    {
        return winner_;
    }

    public void set_winnner(Faction faction)
    {
        winner_ = faction;
    }

    public void reset()
    {
        pink_score_ = 0;
        purple_score_ = 0;
        rounds_left_ = 0;
        pink_rounds_won_ = 0;
        pink_rounds_won_prev_ = 0;
        purple_rounds_won_ = 0;
        purple_rounds_won_prev_ = 0;
        winner_ = Faction.NEUTRAL;
    }

    private void Awake()
    {
        if (instance_ == null)
        {
            DontDestroyOnLoad(gameObject);
            reset();
            instance_ = this;
        }
        else if (instance_ != this)
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        Debug.Log(rounds_left_);
    }
}
