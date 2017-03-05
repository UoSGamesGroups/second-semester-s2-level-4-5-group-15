using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinScreen : MonoBehaviour
{

    [SerializeField] private Text txt_winner_;
    [SerializeField] private Text txt_winner_score_;
    [SerializeField] private Text txt_loser_score_;
    [SerializeField] private Text txt_winner_rounds_;
    [SerializeField] private Text txt_loser_rounds_;

    [SerializeField] private Image img_loser_;
    [SerializeField] private Image img_winner_;
    [SerializeField] private Image img_podium_;

    [SerializeField] private Sprite spr_pink_;
    [SerializeField] private Sprite spr_purple_;

    [SerializeField] private float time_before_next_game_;

    private GameState game_state_;

    private void Start()
    {
        if (!GameObject.Find("GameState"))
        {
            Debug.Log("Error: Failed to find global state.");
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #else
            Application.Quit();
            #endif
        }

        game_state_ = GameObject.Find("GameState").GetComponent<GameState>();

        int pink_score = game_state_.pink_score();
        int purple_score = game_state_.purple_score();

        int pink_rounds_won = game_state_.pink_rounds_won();
        int purple_rounds_won = game_state_.purple_rounds_won();

        var winner = game_state_.winner();

        switch (winner)
        {
            case Faction.PINK:
                txt_winner_score_.text = pink_score.ToString();
                //txt_winner_score_.color = new Color32(255, 0, 255, 255);
                txt_loser_score_.text = purple_score.ToString();
                //txt_loser_score_.color = new Color32(138, 43, 226, 255);
                txt_winner_.text = "Pink Wins!";
                txt_winner_rounds_.text = pink_rounds_won.ToString();
                txt_loser_rounds_.text = purple_rounds_won.ToString();
                img_winner_.sprite = spr_pink_;
                img_loser_.sprite = spr_purple_;
                break;
            case Faction.PURPLE:
                txt_winner_score_.text = purple_score.ToString();
                //txt_winner_score_.color = new Color32(138, 43, 226, 255);
                txt_loser_score_.text = pink_score.ToString();
                //txt_loser_score_.color = new Color32(255, 0, 255, 255);
                txt_winner_.text = "Purple Wins!";
                txt_winner_rounds_.text = purple_rounds_won.ToString();
                txt_loser_rounds_.text = pink_rounds_won.ToString();
                img_winner_.sprite = spr_purple_;
                img_loser_.sprite = spr_pink_;
                break;
            case Faction.NEUTRAL:
                txt_winner_score_.enabled = false;
                txt_loser_score_.enabled = false;
                img_winner_.enabled = false;
                txt_loser_rounds_.enabled = false;
                txt_winner_rounds_.enabled = false;
                img_podium_.enabled = false;
                img_loser_.enabled = false;
                txt_winner_.text = "Draw!";
                break;
        }

        StartCoroutine(TransitionToMenu(time_before_next_game_));
    }

    private IEnumerator TransitionToMenu(float wait_time)
    {
        yield return new WaitForSeconds(wait_time);
        GameObject.Find("Scene Transitioner").GetComponent<SceneTransitioner>().transition_to("Main Menu");
    }

}
