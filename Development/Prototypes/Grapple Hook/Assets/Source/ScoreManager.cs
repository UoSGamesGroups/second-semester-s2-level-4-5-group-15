using System;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    private Text txtPinkScore;
    [SerializeField]
    private Text txtPinkMultiplier;
    [SerializeField]
    private Text txtPurpleScore;
    [SerializeField]
    private Text txtPurpleMultiplier;
    [SerializeField]
    private Text txtTimeLeft;
    [SerializeField]
    private PlayerScore psPink;
    [SerializeField]
    private PlayerScore psPurple;

    Timer scoreTimer;

    private void Start()
    {
        scoreTimer = new Timer(30);
    }

    private void Update()
    {
        txtPinkScore.text = psPink.getScore().ToString();
        txtPinkMultiplier.text = "x" + psPink.getMultiplier().ToString();

        txtPurpleScore.text = psPurple.getScore().ToString();
        txtPurpleMultiplier.text = "x" + psPurple.getMultiplier().ToString();

        txtTimeLeft.text = "0:" + scoreTimer.getTimeLeft().ToString();
    }

}
