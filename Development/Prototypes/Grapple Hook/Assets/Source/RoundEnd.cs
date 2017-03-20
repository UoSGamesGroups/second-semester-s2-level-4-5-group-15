using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RoundEnd : MonoBehaviour
{

    [SerializeField] private Text txt_pink_rounds_won_;
    [SerializeField] private Text txt_purple_rounds_won_;

    [SerializeField] private float flip_speed_;
    [SerializeField] private float flip_smoothness_;

    [SerializeField] private float time_before_next_game_;

    [SerializeField] private AudioSource sound_effect_;

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

        game_state_ = GameObject.Find("GameState").GetComponent<GameState>().get_instance();

        int pink_rounds_won = game_state_.pink_rounds_won();
        int pink_rounds_won_prev = game_state_.pink_rounds_won_prev();

        int purple_rounds_won = game_state_.purple_rounds_won();
        int purple_rounds_won_prev = game_state_.purple_rounds_won_prev();

        txt_pink_rounds_won_.text = pink_rounds_won_prev.ToString();
        txt_purple_rounds_won_.text = purple_rounds_won_prev.ToString();

        if (pink_rounds_won != pink_rounds_won_prev)
        {
            StartCoroutine(FlipAndChange(txt_pink_rounds_won_, pink_rounds_won));
        }
        if (purple_rounds_won != purple_rounds_won_prev)
        {
            StartCoroutine(FlipAndChange(txt_purple_rounds_won_, purple_rounds_won));
        }

        StartCoroutine(TransitionToGame(time_before_next_game_));
    }

    private IEnumerator TransitionToGame(float wait_time)
    {
        yield return new WaitForSeconds(wait_time);

        int rounds_left = game_state_.rounds_left() - 1;

        int pink_rounds_won = game_state_.pink_rounds_won();
        int purple_rounds_won = game_state_.purple_rounds_won();

        // 3 : P 2 0


        int difference = Math.Abs(pink_rounds_won - purple_rounds_won);

        if (rounds_left > 0 && difference <= rounds_left)
        {
            game_state_.set_rounds_left(rounds_left);
            GameObject.Find("Scene Transitioner").GetComponent<SceneTransitioner>().transition_to("Main Scene");
        }
        else
        {
            var winner = Faction.NEUTRAL;

            if (pink_rounds_won > purple_rounds_won)
            {
                winner = Faction.PINK;
            }
            else if (purple_rounds_won > pink_rounds_won)
            {
                winner = Faction.PURPLE;
            }
            else
            {
                int pink_score = game_state_.pink_score();
                int purple_score = game_state_.purple_score();

                if (pink_score > purple_score)
                {
                    winner = Faction.PINK;
                }
                else if (purple_score > pink_score)
                {
                    winner = Faction.PURPLE;
                }
            }

            game_state_.set_winnner(winner);
            GameObject.Find("Scene Transitioner").GetComponent<SceneTransitioner>().transition_to("Win Screen");
        }
    }

    private IEnumerator FlipAndChange(Text text, int value)
    {
        sound_effect_.Play();
        for (var i = 0.0f; Math.Abs(i - 90.0f) > 0.001f; i += flip_smoothness_)
        {
            text.GetComponent<Transform>().localEulerAngles = new Vector3(0.0f, i, 0.0f);
            yield return new WaitForSeconds(flip_speed_);
        }
        text.text = value.ToString();
        for (var i = 90.0f; Math.Abs(i) > 0.001f; i -= flip_smoothness_)
        { 
            text.GetComponent<Transform>().localEulerAngles = new Vector3(0.0f, i, 0.0f);
            yield return new WaitForSeconds(flip_speed_);
        }
    }

}
