using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    void Awake()
    {
        Destroy(GameObject.Find("Talk UI(Clone)"));
        Destroy(GameObject.Find("Overlay(Clone)"));
        Destroy(GameObject.Find("PlayerSpawnController"));
        Destroy(GameObject.Find("player(Clone)"));
        Destroy(GameObject.Find("PauseMenu(Clone)"));
        Destroy(GameObject.Find("JukeBox(Clone)"));
        Destroy(GameObject.Find("Enemy"));
    }


    public void OnContinueClick()
    {
        GameControl.control.Load();
        SceneManager.LoadScene(GameControl.control.room, LoadSceneMode.Single);
    }

    public void OnQuitClick()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}
