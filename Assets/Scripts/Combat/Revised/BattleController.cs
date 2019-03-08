using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleController : MonoBehaviour {

    public static BattleController BC;
    public enum BattleState {PlayerTurn, EnemyTurn, Neither};
    public BattleState currentState;

    public TalkControl textBox;
    public PlayerTurnController PTC;

    public Image FieldSeperator;
    public SpriteRenderer Mirror;
    public Text mirrorHealthText;

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

        FieldSeperator.color = new Color(1f, 1f, 1f, 0f);
        Mirror.color = new Color(1f, 1f, 1f, 0f);
        mirrorHealthText.color = new Color(139 / 255f, 139 / 255f, 139 / 255f, 0f);
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

        yield return StartCoroutine(textBox.Dialogue(Enemy.introDialogue));
        currentState = BattleState.PlayerTurn;
        PTC.currentState = PlayerTurnController.MenuStates.MainSelect;
        PTC.HelpButton.Select();
    }

	// Update is called once per frame
	void Update () {
		
	}

    public IEnumerator EndTurnPlayer()
    {
        Debug.Log("End Player Turn");

        for (float i = 0f; i < 200f; i += 20f)
        {
            FieldSeperator.color = new Color(0f, 0f, 0f, i / 255f);
            Mirror.color = new Color(1f, 1f, 1f, (i * 255 / 200) / 255f);
            mirrorHealthText.color = new Color(139/255f, 139/255f, 139/255f, (i * 255 / 200) / 255f);
            yield return new WaitForSeconds(0.005f);
        }
        FieldSeperator.color = new Color(0f, 0f, 0f, 200/255f);
        Mirror.color = new Color(1f, 1f, 1f, 1f);
        mirrorHealthText.color = new Color(139 / 255f, 139 / 255f, 139 / 255f, 1f);
        yield return new WaitForSeconds(1f);
        currentState = BattleState.EnemyTurn;
        yield return null;
    }

    public IEnumerator EndTurnEnemy()
    {
        Debug.Log("End Enemy Turn");
        int rand = Random.Range(0, Enemy.playerTurnIdle.Length);
        currentState = BattleState.PlayerTurn;
        for (float i = 200f; i > 0f; i -= 20f)
        {
            FieldSeperator.color = new Color(0f, 0f, 0f, i / 255f);
            Mirror.color = new Color(1f, 1f, 1f, (i * 255 / 200) / 255f);
            mirrorHealthText.color = new Color(139 / 255f, 139 / 255f, 139 / 255f, (i * 255 / 200) / 255f);
            yield return new WaitForSeconds(0.005f);
        }
        FieldSeperator.color = new Color(0f, 0f, 0f, 0f);
        Mirror.color = new Color(1f, 1f, 1f, 0f);
        mirrorHealthText.color = new Color(139 / 255f, 139 / 255f, 139 / 255f, 0f);
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
