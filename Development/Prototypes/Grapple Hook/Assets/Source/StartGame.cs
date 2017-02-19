using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour {

    public void startGame(int rounds)
    {
        GameObject.Find("GameState").GetComponent<GameState>().getInstance().setRoundsLeft(rounds);
        SceneManager.LoadScene("Main Scene", LoadSceneMode.Single);
    }

}
