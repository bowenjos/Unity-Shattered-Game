using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuController : MonoBehaviour {

    enum PauseMenuStates { NoMenu, SelectMenu, ItemMenu, SettingsPanel, QuitPanel}
    PauseMenuStates currentState;

    public GameObject PlayerMenu;

    public GameObject statusPanel;
    public GameObject menuOptions;
    public GameObject playerPanel;
    public GameObject pausedPanel;
    public GameObject moneyPanel;
    public GameObject itemPanel;
    public GameObject quitPanel;

    public Text playerNameText;
    public Text playtimeText;
    public Text curHealthText;
    public Text maxHealthText;
    public Text moneyText;

    public Button resumeButton;
    public Button desktopButton;
    public Button item0;

    public AudioClip pause;
    public AudioClip unpause;

	// Use this for initialization
	void Awake () {
        currentState = PauseMenuStates.NoMenu;
	}
	
	// Update is called once per frame
	void Update () {
        switch (currentState)
        {
            case PauseMenuStates.NoMenu:
                statusPanel.SetActive(false);
                menuOptions.SetActive(false);
                playerPanel.SetActive(false);
                pausedPanel.SetActive(false);
                moneyPanel.SetActive(false);
                itemPanel.SetActive(false);
                quitPanel.SetActive(false);
                break;
            case PauseMenuStates.SelectMenu:
                statusPanel.SetActive(true);
                SetVisibleLens();
                menuOptions.SetActive(true);
                playerPanel.SetActive(true);
                SetPlayerName();
                SetHP();
                SetPlaytime();
                pausedPanel.SetActive(true);
                moneyPanel.SetActive(true);
                SetMoney();
                itemPanel.SetActive(false);
                quitPanel.SetActive(false);
                break;
            case PauseMenuStates.ItemMenu:
                statusPanel.SetActive(false);
                menuOptions.SetActive(true);
                playerPanel.SetActive(true);
                SetPlayerName();
                SetHP();
                SetPlaytime();
                pausedPanel.SetActive(true);
                moneyPanel.SetActive(true);
                SetMoney();
                itemPanel.SetActive(true);
                quitPanel.SetActive(false);
                break;
            case PauseMenuStates.QuitPanel:
                statusPanel.SetActive(false);
                menuOptions.SetActive(false);
                playerPanel.SetActive(false);
                pausedPanel.SetActive(false);
                moneyPanel.SetActive(false);
                itemPanel.SetActive(false);
                quitPanel.SetActive(true);
                break;
        }

        if (Input.GetButtonDown("Pause"))
        {
            switch (currentState)
            {
                case PauseMenuStates.NoMenu:
                    if (!GameControl.control.frozen)
                    {
                        this.GetComponent<AudioSource>().clip = pause;
                        this.GetComponent<AudioSource>().Play();
                        GameControl.control.Pause();
                        menuOptions.SetActive(true);
                        resumeButton.Select();
                        resumeButton.OnSelect(null);
                        currentState = PauseMenuStates.SelectMenu;
                    }
                    break;
                default:
                    this.GetComponent<AudioSource>().clip = unpause;
                    this.GetComponent<AudioSource>().Play();
                    GameControl.control.Unpause();
                    currentState = PauseMenuStates.NoMenu;
                    break;
            }
        }

        if (Input.GetButtonDown("Cancel"))
        {
            switch (currentState)
            {
                case PauseMenuStates.SelectMenu:
                    GameControl.control.Unpause();
                    this.GetComponent<AudioSource>().clip = unpause;
                    this.GetComponent<AudioSource>().Play();
                    currentState = PauseMenuStates.NoMenu;
                    break;
                case PauseMenuStates.ItemMenu:
                    currentState = PauseMenuStates.SelectMenu;
                    resumeButton.Select();
                    resumeButton.OnSelect(null);
                    break;
                case PauseMenuStates.QuitPanel:
                    menuOptions.SetActive(true);
                    resumeButton.Select();
                    resumeButton.OnSelect(null);
                    currentState = PauseMenuStates.SelectMenu;
                    break;
            }
        }
    }

    void SetMoney()
    {
        moneyText.text = GameControl.control.money.ToString();
    }

    void SetVisibleLens()
    {
        for(int i = 0; i < 7; i++)
        {
            statusPanel.transform.GetChild(i).gameObject.SetActive(GameControl.control.lens[i]);
        }
    }

    void SetHP()
    {
        curHealthText.text = GameControl.control.health.ToString();
        maxHealthText.text = GameControl.control.maxHealth.ToString();
    }

    void SetPlaytime()
    {
        System.TimeSpan ts = System.TimeSpan.FromSeconds((int)GameControl.control.playedTime); 
        playtimeText.text = ts.ToString();
    }

    void SetPlayerName()
    {
        playerNameText.text = GameControl.control.playerName;
    }

    public void OnResumeClick()
    {
        currentState = PauseMenuStates.NoMenu;
        this.GetComponent<AudioSource>().clip = unpause;
        this.GetComponent<AudioSource>().Play();
        GameControl.control.Unpause();
    }

    public void OnItemsClick()
    {
        currentState = PauseMenuStates.ItemMenu;
        itemPanel.SetActive(true);
        item0.Select();
        item0.OnSelect(null);
    }

    public void OnConfigClick()
    {

    }

    public void OnQuitClick()
    {
        currentState = PauseMenuStates.QuitPanel;
        quitPanel.SetActive(true);
        desktopButton.Select();
        desktopButton.OnSelect(null);
    }

    public void OnDesktopClick()
    {
        Application.Quit();
    }

    public void OnMainMenuClick()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    public void OnNevermindClick()
    {
        currentState = PauseMenuStates.SelectMenu;
        menuOptions.SetActive(true);
        resumeButton.Select();
        resumeButton.OnSelect(null);
    }
}
