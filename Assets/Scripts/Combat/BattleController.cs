using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleController : MonoBehaviour {

    public static BattleController BC;
    public enum BattleState {PlayerTurn, EnemyTurn};
    public BattleState currentState;

    //Enemy Variables
    public int enemyHealthMax;
    public int enemyHealthCurrent;
    public string enemyEmotion;
    public int[,] enemyResistances;

    //Battle Rules
    bool fleeable;

    //Previous Scene Data
    string oldSceneName;
    int playerLocationX;
    int playerLocationY;
    int playerLocationZ;

    //Entities
    EnemyCombatControl Enemy;

    void Awake()
    {
        Enemy = GameObject.Find("Enemy").GetComponent<EnemyCombatControl>();
    }

    // Use this for initialization
    void Start () {
		//Are you ready to Begin?

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
