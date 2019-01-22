using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleController : MonoBehaviour {

    public static BattleController BC;
    public enum BattleState {Player, Enemy};
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
    TalkControlDialogue tcd;

    void Awake()
    {
        Enemy = GameObject.Find("Enemy").GetComponent<EnemyCombatControl>();
    }

    // Use this for initialization
    void Start () {
		//Are you ready to Begin?
        //StartCoroutine(tcd.Dialogue(Enemy.dialogueApproach);

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
