using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleController : MonoBehaviour {

    public static BattleController BC;
    public enum BattleState {PlayerTurn, EnemyTurn, Neither};
    public BattleState currentState;

    public TalkControl textBox;
    public PlayerTurnController PTC;

    /*
    //Enemy Variables
    public int enemyHealthMax;
    public int enemyHealthCurrent;
    public string enemyEmotion;
    public int[,] enemyResistances;

    //Battle Rules
    bool fleeable;
    */

    //Previous Scene Data
    string oldSceneName;
    int playerLocationX;
    int playerLocationY;
    int playerLocationZ;

    //Entities
    EnemyCombatController Enemy;

    void Awake()
    {
        Enemy = GameObject.Find("Enemy").GetComponent<EnemyCombatController>();
        currentState = BattleState.Neither;
    }

    // Use this for initialization
    void Start () {
        //Are you ready to Begin?
        PTC.currentState = PlayerTurnController.MenuStates.EnemyTurn;
        StartCoroutine(BattleStart());

	}
	
    public IEnumerator BattleStart()
    {

        yield return StartCoroutine(textBox.Dialogue(Enemy.introDialogue[0]));
        currentState = BattleState.PlayerTurn;
        PTC.currentState = PlayerTurnController.MenuStates.MainSelect;
        PTC.HelpButton.Select();
    }

	// Update is called once per frame
	void Update () {
		
	}

    public IEnumerator EndTurnPlayer()
    {
        currentState = BattleState.EnemyTurn;
        yield return null;
    }

    public IEnumerator EndTurnEnemy()
    {
        currentState = BattleState.PlayerTurn;
        yield return null;
    }
    
    public IEnumerator ResolveCombat()
    {
        yield return null;
    }
}
