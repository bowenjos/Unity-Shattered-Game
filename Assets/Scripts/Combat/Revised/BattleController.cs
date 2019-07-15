using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattleController : MonoBehaviour {

    public static BattleController BC;
    public GameObject BattleCamera;
    public enum BattleState {PlayerTurn, EnemyTurn, Neither};
    public BattleState currentState;

    public TalkControl textBox;
    public PlayerTurnController PTC;

    public Image FieldSeperator;
    public SpriteRenderer Mirror;
    public SpriteRenderer EnemyAttacker;
    public Text mirrorHealthText;
    public Text mirrorBorderText;
    public SpriteRenderer[] Blockers;

    public bool enemyTurnStart;

    public AudioSource BattleJukeBox;
    public FanfareController fanfare;

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
    public GameObject Player;

    void Awake()
    {

        if (BC == null)
        {
            //DontDestroyOnLoad(gameObject);
            BC = this;
        }
        else if (BC != this)
        {
            Destroy(gameObject);
        }
        
        Blockers = GameObject.Find("Blockers").GetComponentsInChildren<SpriteRenderer>();
        FieldSeperator.color = new Color(1f, 1f, 1f, 0f);
        Mirror.color = new Color(1f, 1f, 1f, 0f);
        EnemyAttacker.color = new Color(1f, 1f, 1f, 0f);
        mirrorHealthText.color = new Color(139 / 255f, 139 / 255f, 139 / 255f, 0f);
        mirrorBorderText.color = new Color(139 / 255f, 139 / 255f, 139 / 255f, 0f);
        Enemy = GameObject.Find("Enemy").GetComponent<EnemyCombatController>();
        BattleJukeBox.clip = Enemy.battleMusic;
        BattleJukeBox.Play();
        Player = GameObject.Find("player(Clone)");
        Player.SetActive(false);
        currentState = BattleState.Neither;
        enemyTurnStart = false;
    }

    // Use this for initialization
    void Start () {
        //Are you ready to Begin?
        PTC.currentState = PlayerTurnController.MenuStates.EnemyTurn;
        //Play battle music
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
            mirrorBorderText.color = new Color(1f, 1f, 1f, (i * 255 / 200) / 255f);
            for (int j = 0; j < 12; j++)
            {
                Blockers[j].color = new Color(1f, 1f, 1f, (i * 255f / 200f * 0.2f) / 255f);
            }
            EnemyAttacker.color = new Color(1f, 1f, 1f, (i * 255 / 200) / 255f);
            yield return new WaitForSeconds(0.005f);
        }
        FieldSeperator.color = new Color(0f, 0f, 0f, 200/255f);
        Mirror.color = new Color(1f, 1f, 1f, 1f);
        mirrorHealthText.color = new Color(139 / 255f, 139 / 255f, 139 / 255f, 1f);
        mirrorBorderText.color = new Color(1f, 1f, 1f, 1f);
        for (int i = 0; i < 12; i++)
        {
            Blockers[i].color = new Color(1f, 1f, 1f, 0.2f);
        }
        EnemyAttacker.color = new Color(1f, 1f, 1f, 1f);
        yield return new WaitForSeconds(1f);
        currentState = BattleState.EnemyTurn;
        enemyTurnStart = true;
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
            mirrorBorderText.color = new Color(1f, 1f, 1f, (i * 255 / 200) / 255f);
            for (int j = 0; j < 12; j++)
            {
                Blockers[j].color = new Color(1f, 1f, 1f, (i * 255f/200f * 0.2f)/255f);
            }
            EnemyAttacker.color = new Color(1f, 1f, 1f, (i * 255 / 200) / 255f);
            yield return new WaitForSeconds(0.005f);
        }
        FieldSeperator.color = new Color(0f, 0f, 0f, 0f);
        Mirror.color = new Color(1f, 1f, 1f, 0f);
        mirrorHealthText.color = new Color(139 / 255f, 139 / 255f, 139 / 255f, 0f);
        mirrorBorderText.color = new Color(1f, 1f, 1f, 0f);
        for (int i = 0; i < 12; i++)
        {
            Blockers[i].color = new Color(1f, 1f, 1f, 0f);
        }
        EnemyAttacker.color = new Color(1f, 1f, 1f, 0f);
        StartCoroutine(textBox.Dialogue(Enemy.playerTurnIdle[rand]));
        PTC.currentState = PlayerTurnController.MenuStates.MainSelect;
        PTC.HelpButton.Select();
        yield return null;
    }

    public void EnemyTakeDamage(int value)
    {
        Enemy.enemyHealth -= value;
    }

    public void DestroyPlayer()
    {
        Player.SetActive(true);
        Destroy(Player);
    }

    public IEnumerator ResolveCombat()
    {
        Enemy.ResolveEnemy();
        BattleJukeBox.Stop();
        fanfare.PlayFanfare();
        yield return StartCoroutine(textBox.Dialogue(Enemy.outroDialogue));
        yield return new WaitForSeconds(4f);
        int actualMoney = Enemy.rewardMoney * (GameControl.control.numMasks + 1) + ((int)Random.Range(-GameControl.control.numMasks, Mathf.Pow(GameControl.control.numMasks, 2f))) + (int)Random.Range(-GameControl.control.numMasks, GameControl.control.numMasks);
        yield return StartCoroutine(textBox.Dialogue("You received " + actualMoney + " GD."));
        GameControl.control.money += actualMoney;
        yield return new WaitForSeconds(4f);
        //Item drop?
        TransitionController TC = GameObject.Find("TransitionControl(Clone)").GetComponent<TransitionController>();
        GameObject.Find("JukeBox(Clone)").GetComponent<JukeBoxController>().ResumeSongPartway();
        StartCoroutine(GameObject.Find("JukeBox(Clone)").GetComponent<JukeBoxController>().FadeIn(0.4f));
        yield return StartCoroutine(TC.transitionOut());
        Destroy(BattleCamera);
        Player.SetActive(true);
        GameControl.control.Unfreeze();
        SceneManager.LoadScene(GameControl.control.room.ToString());

        yield return null;
    }
}
