using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour {

    public void startGame(int rounds)
    {
        Destroy(GameObject.Find("Music"));

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

        GameObject.Find("Scene Transitioner").GetComponent<SceneTransitioner>().transition_to("Main Scene");
    }

}
