using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    public Text txtPinkScore;

    [SerializeField]
    public Text txtPinkMultiplier;

    [SerializeField]
    private Text txtPurpleScore;

    [SerializeField]
    private Text txtPurpleMultiplier;

    [SerializeField]
    private Text txtRoundTime;

    [SerializeField]
    private List<GameObject> destructables;

    private readonly List<GameObject> activeObjects = new List<GameObject>();

    [SerializeField]
    private List<Transform> objSpawnPoints;

    [SerializeField]
    private GameObject playerPink;

    [SerializeField]
    private GameObject playerPurple;

    private const int ROUND_LENGTH = 5;

    private Vector3 pinkSpawn;
    private Vector3 purpleSpawn;

    private GameState gameState;

    private Timer tmrGameTime;

    private bool transitioning_;

    private void Start()
    {
        pinkSpawn = playerPink.transform.position;
        purpleSpawn = playerPurple.transform.position;
        gameState = GameObject.Find("GameState").GetComponent<GameState>().get_instance();
        tmrGameTime = new Timer(ROUND_LENGTH);
        resetMatch();
    }

    private void FixedUpdate()
    {
        var pinkScoreManager = playerPink.GetComponent<PlayerScore>();
        txtPinkScore.text = pinkScoreManager.getScore().ToString();
        txtPinkMultiplier.text = "x" + pinkScoreManager.getMultiplier();

        var purpleScoreManager = playerPurple.GetComponent<PlayerScore>();
        txtPurpleScore.text = purpleScoreManager.getScore().ToString();
        txtPurpleMultiplier.text = "x" + purpleScoreManager.getMultiplier();

        txtRoundTime.text = tmrGameTime.getTimeLeft().ToString("00");
    }

    private bool ObjectsAreActive()
    {
        return activeObjects.Any(o => !o.GetComponent<Destructable>().isDestroyed());
    }

    private void Update()
    {
        if (!tmrGameTime.hasElapsed() && ObjectsAreActive() || transitioning_) return;

        int pink_score = playerPink.GetComponent<PlayerScore>().getScore();
        int purple_score = playerPurple.GetComponent<PlayerScore>().getScore();

        gameState.set_pink_score(gameState.pink_score() + pink_score);
        gameState.set_purple_score(gameState.purple_score() + purple_score);

        if (pink_score > purple_score)
        {
            gameState.set_round_winner(Faction.PINK);
        }
        else if (purple_score > pink_score)
        {
            gameState.set_round_winner(Faction.PURPLE);
        }
        else
        {
            gameState.set_round_winner(Faction.NEUTRAL);
        }

        GameObject.Find("Scene Transitioner").GetComponent<SceneTransitioner>().transition_to("Round End");
        transitioning_ = true;
    }

    private void destroyObjects()
    {
        foreach (var activeObject in activeObjects)
        {
            Destroy(activeObject);
        }
        activeObjects.Clear();
    }

    private void resetPlayers()
    {
        playerPink.GetComponent<Transform>().position = pinkSpawn;
        playerPurple.GetComponent<Transform>().position = purpleSpawn;
    }

    private void spawnObjects()
    {
        var availableSpawnPoints = new List<Transform>(objSpawnPoints);
        while (availableSpawnPoints.Any())
        {
            var spawnPointIndex = Random.Range(0, availableSpawnPoints.Count);
            var objType = Random.Range(0, destructables.Count);
            var objNew = Instantiate(destructables[objType], availableSpawnPoints[spawnPointIndex].position, Quaternion.identity);
            objNew.GetComponent<Destructable>().setFaction(Faction.NEUTRAL);
            activeObjects.Add(objNew);
            availableSpawnPoints.RemoveAt(spawnPointIndex);
        }
    }

    private void resetMatch()
    {
        resetPlayers();
        destroyObjects();
        spawnObjects();
        tmrGameTime.reset();
    }

}
