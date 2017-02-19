using UnityEngine;
using UnityEngine.UI;

public class WinScreen : MonoBehaviour
{

    [SerializeField]
    private Text txtWinner;

    [SerializeField]
    private Image imgLoser;

    [SerializeField]
    private Image imgWinner;

    [SerializeField]
    private Sprite sprPink;

    [SerializeField]
    private Sprite sprPurple;

    private void Start()
    {
        var gameState = GameObject.Find("GameState").GetComponent<GameState>();

        int pinkScore = gameState.getPinkScore();
        int purpleScore = gameState.getPurpleScore();

        Faction winner = gameState.getWinner();

        imgLoser.sprite = winner == Faction.PINK ? sprPurple : sprPink;
        imgWinner.sprite = winner == Faction.PINK ? sprPink : sprPurple;


        txtWinner.text = (winner == Faction.PINK ? "Pink" : "Purple") + " wins!";
    }


}
