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
    public GameObject willExtra;
    public GameObject elise;
    public GameObject thongsai;
    public GameObject des;
    public GameObject drum;
    public GameObject voice;
    public GameObject violin;
    public GameObject yolandaExtra;

    public GameObject[] masks;

	// Use this for initialization
	void Start () {

        //Set all ghosts to inactive on menu
        for(int i = 0; i < 7; i++)
        {
            masks[i].SetActive(false);
        }
        will.SetActive(false);
        elise.SetActive(false);
        thongsai.SetActive(false);
        des.SetActive(false);
        drum.SetActive(false);
        voice.SetActive(false);
        violin.SetActive(false);

        if(File.Exists(Application.persistentDataPath + "/playerSave.dat"))
        {
            GameControl.control.LoadTemp();
            playerName.text = GameControl.control.playerName;
            System.TimeSpan ts = System.TimeSpan.FromSeconds((int)GameControl.control.playedTimeTemp);
            playTime.text = ts.ToString();
            playerLocation.text = GameControl.control.saveRoomNameTemp;

            //Set Ghosts Active as needed
            if (GameControl.control.masksTemp[0])
            {
                will.SetActive(true);
                masks[0].SetActive(true);
                willExtra.SetActive(false);
            }
            if (GameControl.control.masksTemp[1])
            {
                violin.SetActive(true);
                masks[1].SetActive(true);
                yolandaExtra.SetActive(false);
            }
            if (GameControl.control.masksTemp[2])
            {
                elise.SetActive(true);
                masks[2].SetActive(true);
            }
            if (GameControl.control.masksTemp[3])
            {
                thongsai.SetActive(true);
                masks[3].SetActive(true);
            }
            if (GameControl.control.masksTemp[4])
            {
                drum.SetActive(true);
                masks[4].SetActive(true);
            }
            if (GameControl.control.masksTemp[5])
            {
                voice.SetActive(true);
                masks[5].SetActive(true);
            }
            if (GameControl.control.masksTemp[6])
            {
                des.SetActive(true);
                masks[6].SetActive(true);
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
        GameControl.control.frozen = false;
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
        GameControl.control.frozen = false;
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
