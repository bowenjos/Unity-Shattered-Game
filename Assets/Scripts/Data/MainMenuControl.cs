using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuControl : MonoBehaviour {

    public Button ContinueButton;
    public Button NewGameButton;
    public Button SettingButton;
    public Button ExitButton;

	// Use this for initialization
	void Start () {
        if(File.Exists(Application.persistentDataPath + "/playerSave.dat"))
        {
            GameControl.control.Load();
            ContinueButton.Select();
        }
        else
        {
            ContinueButton.enabled = false;
            ContinueButton.GetComponentInChildren<Text>().color = new Color(25f, 25f, 25f, 0.1f);
            NewGameButton.Select();
        }
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnContinueButtonPress()
    {
        SceneManager.LoadScene(GameControl.control.room, LoadSceneMode.Single);
    }

    public void OnNewGameButtonPress()
    {
        GameControl.control.health = 100;
        GameControl.control.maxHealth = 100;
        GameControl.control.money = 0;

        GameControl.control.items = new string[10];
        GameControl.control.lens = new bool[] { false, false, false, false, false, false, false};
        GameControl.control.masks = new bool[] { false, false, false, false, false, false, false };

        GameControl.control.room = "EntryHallOfSilence";
        GameControl.control.lantern = false;
        SceneManager.LoadScene(GameControl.control.room, LoadSceneMode.Single);

    }

    public void OnSettingButtonPress()
    {

    }

    public void OnExitButtonPress()
    {
        Application.Quit();
    }
}
