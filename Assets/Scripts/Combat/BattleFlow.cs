using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System.IO;


/***********************
Class Name: BattleFlow
Purpose: Holds all of the values and data regarding combat
***********************/
public class BattleFlow : MonoBehaviour {

    public static BattleFlow BF;
    public static int playerOrEnemyPhase = 0;

    public bool expelled = false;

    public double enemyHealth;
    public double enemyHealthReset;
    public string enemyEmotion;
    public int enemyLevelState;
    public string oldScene;
    public bool fleeable;

    public int[,] enemyResistences;

    public string approachDialogue;
    public string[][] talkDialogue;
    public string[][] idleDialogue;
    public string[] stateChangeDialogue;

    public GameObject Player;

    public GameObject helpMenu;
    public GameObject TalkControlPlayer;
    public GameObject TalkControlEnemy;
    public GameObject EnemySprite;
    Text ButtonText;

    //public float enemyDamageValue;
    public float playerDamageModifier = 1;
    public float enemyDamageModifier = 1;
    public int playerDamageModTurnCounter;
    public int enemyDamageModTurnCounter;

    public int playerAttackType;
    int battleStart = 0;

	// Update is called once per frame
	void Update () {
        if(battleStart == 0)
        {
            StartCoroutine(TalkControlPlayer.GetComponent<TalkControl>().Dialogue(approachDialogue));
            battleStart = 1;
        }
	}

    public IEnumerator EndTurnPlayer()
    {

        CombatMenuController.currentState = CombatMenuController.MenuStates.PlayerEndPhase;
        //yield return StartCoroutine()
        if (playerAttackType == 0)
        {
            CombatMenuController.currentState = CombatMenuController.MenuStates.EnemyPhase;
            yield return StartCoroutine(TalkControlEnemy.GetComponent<TalkControl>().Dialogue(stateChangeDialogue[enemyLevelState]));
            --enemyLevelState;
            yield return new WaitForSeconds(0.5f);
        }
        if (playerAttackType == 1)
        {
            int random = Random.Range(0, talkDialogue[enemyLevelState].Length);
            yield return StartCoroutine(TalkControlPlayer.GetComponent<TalkControl>().Dialogue(talkDialogue[enemyLevelState][random]));
            yield return new WaitForSeconds(0.5f);
        }
        CombatMenuController.currentState = CombatMenuController.MenuStates.EnemyPhase;
        playerOrEnemyPhase = 1;
        yield return null;
    }

    public void EndTurnEnemy()
    {
        if (playerDamageModTurnCounter > 0)
        {
            playerDamageModTurnCounter -= 1;
            if (playerDamageModTurnCounter == 0)
            {
                playerDamageModifier = 1;
            }
        }
        if (enemyDamageModTurnCounter > 0)
        {
            enemyDamageModTurnCounter -= 1;
            if (enemyDamageModTurnCounter == 0)
            {
                enemyDamageModifier = 1;
            }
        }
        playerOrEnemyPhase = 0;
        EnemyCombatControl.hasAttacked = false;
        this.GetComponent<CombatMenuController>().RestoreControl();
    }

    public IEnumerator ResolveCombat()
    {
        CombatMenuController.currentState = CombatMenuController.MenuStates.EnemyPhase;
        yield return StartCoroutine(TalkControlEnemy.GetComponent<TalkControl>().Dialogue(stateChangeDialogue[0]));
        //Give rewards

        expelled = true;
        yield return new WaitForSeconds(0.1f);
        GameControl.control.Unfreeze();
        Player.SetActive(true);
        SceneManager.LoadScene(oldScene, LoadSceneMode.Single);
    }
}
