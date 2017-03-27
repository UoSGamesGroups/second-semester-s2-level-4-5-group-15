using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private SceneTransitioner sceneTransitioner;

    private void Start()
    {
        sceneTransitioner = GameObject.Find("Scene Transitioner").GetComponent<SceneTransitioner>();
    }

    public void startGame()
    {
        sceneTransitioner.transition_to("Round Select");
    }

    public void exitGame()
    {
        Application.Quit();   
    }

}
