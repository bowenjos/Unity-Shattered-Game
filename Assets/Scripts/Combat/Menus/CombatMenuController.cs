using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CombatMenuController : MonoBehaviour {

	// Use this for initialization
	public enum MenuStates { UserCombatPanel, ItemPanel, HelpPanel, TalkPanel, EnemyPhase, PlayerEndPhase };
    public static MenuStates currentState;

    protected BattleFlow bf;

    public GameObject combatMenu;
    public GameObject itemMenu;
    public GameObject helpMenu;
    public GameObject enemyTalkPanel;
    public GameObject playerTalkPanel;

    public GameObject helpButton;
    public GameObject itemButton;
    public GameObject expelButton;
    public GameObject fleeButton;
    public GameObject itemStartButton;
    public GameObject helpStartButton;

    void Awake()
    {
        currentState = MenuStates.UserCombatPanel;
        
    }

    void Start()
    {
        helpButton.GetComponent<Button>().Select();
        bf = this.GetComponent<BattleFlow>();
    }

    void Update()
    {
        switch(currentState)
        {
            case MenuStates.UserCombatPanel:
                combatMenu.SetActive(true);
                itemMenu.SetActive(false);
                helpMenu.SetActive(false);
                playerTalkPanel.SetActive(true);
                enemyTalkPanel.SetActive(false);

                helpButton.GetComponent<Button>().interactable = true;
                itemButton.GetComponent<Button>().interactable = true;
                expelButton.GetComponent<Button>().interactable = true;
                fleeButton.GetComponent<Button>().interactable = true;
                break;
            case MenuStates.ItemPanel:
                combatMenu.SetActive(true);
                itemMenu.SetActive(true);
                helpMenu.SetActive(false);
                playerTalkPanel.SetActive(false);
                enemyTalkPanel.SetActive(false);

                helpButton.GetComponent<Button>().interactable = false;
                itemButton.GetComponent<Button>().interactable = false;
                expelButton.GetComponent<Button>().interactable = false;
                fleeButton.GetComponent<Button>().interactable = false;
                break;
            case MenuStates.HelpPanel:
                combatMenu.SetActive(true);
                itemMenu.SetActive(false);
                helpMenu.SetActive(true);
                playerTalkPanel.SetActive(false);
                enemyTalkPanel.SetActive(false);

                helpButton.GetComponent<Button>().interactable = false;
                itemButton.GetComponent<Button>().interactable = false;
                expelButton.GetComponent<Button>().interactable = false;
                fleeButton.GetComponent<Button>().interactable = false;
                break;
            case MenuStates.EnemyPhase:
                combatMenu.SetActive(true);
                itemMenu.SetActive(false);
                helpMenu.SetActive(false);
                playerTalkPanel.SetActive(false);
                enemyTalkPanel.SetActive(true);

                helpButton.GetComponent<Button>().interactable = false;
                itemButton.GetComponent<Button>().interactable = false;
                expelButton.GetComponent<Button>().interactable = false;
                fleeButton.GetComponent<Button>().interactable = false;
                break;
            case MenuStates.PlayerEndPhase:
                combatMenu.SetActive(true);
                itemMenu.SetActive(false);
                helpMenu.SetActive(true);
                playerTalkPanel.SetActive(true);
                enemyTalkPanel.SetActive(false);

                helpButton.GetComponent<Button>().interactable = false;
                itemButton.GetComponent<Button>().interactable = false;
                expelButton.GetComponent<Button>().interactable = false;
                fleeButton.GetComponent<Button>().interactable = false;
                break;

            default:
                break;
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            
            if(currentState == MenuStates.HelpPanel)
            {
                helpButton.GetComponent<Button>().interactable = true;
                helpButton.GetComponent<Button>().Select();
            }
            else if(currentState == MenuStates.ItemPanel)
            {
                itemButton.GetComponent<Button>().interactable = true;
                itemButton.GetComponent<Button>().Select();
            }
            currentState = MenuStates.UserCombatPanel;
        }
    }

    public void OnHelpButtonPress()
    {
        if(currentState == MenuStates.UserCombatPanel)
        {
            currentState = MenuStates.HelpPanel;
            helpMenu.SetActive(true);
            helpStartButton.GetComponent<Button>().Select();
        }
        
    }

    public void OnExpelButtonPress()
    {
        if (currentState == MenuStates.UserCombatPanel)
        {
            if (bf.enemyHealth == 0 && bf.enemyLevelState != 0)
            {
                bf.EnemySprite.GetComponent<Shake>().StartShake(0.1f);
                //Set animation value
                bf.playerAttackType = 0;
                bf.enemyHealth = bf.enemyHealthReset;
                StartCoroutine(bf.EndTurnPlayer());
            }
            else if (bf.enemyHealth == 0 && bf.enemyLevelState == 0)
            {
                //Animation
                StartCoroutine(bf.ResolveCombat());
            }
            else
            {
                //Nothing, End Turn
            }
        }
    }

    public void OnItemButtonPress()
    {
        if (currentState == MenuStates.UserCombatPanel)
        {
            currentState = MenuStates.ItemPanel;
            itemMenu.SetActive(true);
            itemStartButton.GetComponent<Button>().Select();
        }
    }

    public void OnFleeButtonPress()
    {
        if (currentState == MenuStates.UserCombatPanel)
        {
            //leave encounter
            if (bf.fleeable)
            {
                int random = Random.Range(0, 4);
                if (random == 0)
                {
                    //Animation
                    SceneManager.LoadScene(bf.oldScene, LoadSceneMode.Single);
                }
                else
                {
                    //StartCoroutine(GameObject.Find("FoeChatterText").GetComponent<TalkControl>().Dialogue("Nice try, but not this time.", 1));
                    StartCoroutine(bf.EndTurnPlayer());
                }
            }
            else
            {
                //StartCoroutine(GameObject.Find("FoeChatterText").GetComponent<TalkControl>().Dialogue{"You won't escape my grasp no matter how hard you try.", 1));
                StartCoroutine(bf.EndTurnPlayer());
            }

        }
    }

    public void RestoreControl()
    {
        currentState = MenuStates.UserCombatPanel;
        helpButton.GetComponent<Button>().interactable = true;
        helpButton.GetComponent<Button>().Select();
        int random = Random.Range(0, bf.idleDialogue[bf.enemyLevelState].Length);
        StartCoroutine(playerTalkPanel.GetComponentInChildren<TalkControl>().Dialogue(bf.idleDialogue[bf.enemyLevelState][random]));

    }
}
