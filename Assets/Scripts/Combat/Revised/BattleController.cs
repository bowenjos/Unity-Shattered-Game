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
    public EnemyCombatController Enemy;

    void Awake()
    {
        if (BC == null)
        {
            DontDestroyOnLoad(gameObject);
            BC = this;
        }
        else if (BC != this)
        {
            Destroy(gameObject);
        }

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

        yield return StartCoroutine(textBox.StartDialogue(Enemy.introDialogue));
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
        Debug.Log("End Player Turn");
        yield return null;
    }

    public IEnumerator EndTurnEnemy()
    {
        Debug.Log("End Enemy Turn");
        int rand = Random.Range(0, Enemy.playerTurnIdle.Length);
        currentState = BattleState.PlayerTurn;
        StartCoroutine(textBox.Dialogue(Enemy.playerTurnIdle[rand]));
        PTC.currentState = PlayerTurnController.MenuStates.MainSelect;
        PTC.HelpButton.Select();
        yield return null;
    }
    
    public IEnumerator ResolveCombat()
    {
        yield return null;
    }
}
