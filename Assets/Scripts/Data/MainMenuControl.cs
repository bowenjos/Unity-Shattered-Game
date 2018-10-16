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

    public Text playerName;
    public Text playTime;
    public Text playerLocation;

    public GameObject will;

	// Use this for initialization
	void Start () {

        //Set all ghosts to inactive on menu
        will.SetActive(false);

        if(File.Exists(Application.persistentDataPath + "/playerSave.dat"))
        {
            GameControl.control.LoadTemp();
            playerName.text = GameControl.control.playerName;
            System.TimeSpan ts = System.TimeSpan.FromSeconds((int)GameControl.control.playedTimeTemp);
            playTime.text = ts.ToString();
            playerLocation.text = GameControl.control.saveRoomNameTemp;

            //Set Ghosts Active as needed
            if (GameControl.control.masks[0])
            {
                will.SetActive(true);
            }

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
        GameControl.control.Load();
        SceneManager.LoadScene(GameControl.control.room, LoadSceneMode.Single);
    }

    public void OnNewGameButtonPress()
    {

        GameControl.control.playerName = "Klaus";
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
