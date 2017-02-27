using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour {

    public void startGame(int rounds)
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

        var game_state = GameObject.Find("GameState").GetComponent<GameState>().get_instance();

        game_state.reset();
        game_state.set_rounds_left(rounds);

        SceneManager.LoadScene("Main Scene", LoadSceneMode.Single);
    }

}
