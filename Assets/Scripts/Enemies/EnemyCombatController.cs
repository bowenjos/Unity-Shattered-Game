using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyCombatController : MonoBehaviour {

    //A Class for storing data relevant to Monster's

    public bool fleeable;

    public double enemyHealth;
    public double enemyHealthMax;
    public string enemyEmotion;
    public int[] enemyResistances;
    public int enemyLevel;

    public string introDialogue;
    public string[] talkDialogue;
    public string[] sitDialogue;
    public string[] hugDialogue;
    public string[] actDialogue;
    public string[] affirmDialogue;
    public string[] giftDialogue;

    public string[] reactionDialogue;
    public string[][] enemyChatter;
    public string[] playerTurnIdle;

    public GameObject AttackPrefab;

    private Transform[] SetPoints;
    private Transform EnemyAttackSprite;

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Encounter")
        {
            GameControl.control.encounter = true;
            SetPoints = GameObject.Find("SetPoints").GetComponentsInChildren<Transform>();
            EnemyAttackSprite = GameObject.Find("EnemyAttackPhaseSprite").GetComponent<Transform>();
            Debug.Log(SetPoints.Length);
        }
        else
        {
            GameControl.control.encounter = false;
        }
    }

	// Use this for initialization
	void OnSceneLoaded (Scene aScene, LoadSceneMode aMode) {
        Debug.Log("Scene Laoded");
        if(aScene.name == "Encounter")
        {
            GameControl.control.encounter = true;
            SetPoints = GameObject.Find("SetPoints").GetComponentsInChildren<Transform>();
            Debug.Log(SetPoints);
        }
        else
        {
            GameControl.control.encounter = false;
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (GameControl.control.encounter == true)
        {
            if (BattleController.BC.currentState == BattleController.BattleState.EnemyTurn && BattleController.BC.enemyTurnStart == true)
            {
                Debug.Log("Hewwo");
                StartCoroutine(EnemyTurn());
            }
        }
	}

    public virtual IEnumerator EnemyTurn()
    {
        BattleController.BC.enemyTurnStart = false;
        yield return StartCoroutine(MoveToPoint(SetPoints[12], .2f));
        DefaultAttack();
        for(int i = 1; i < 12; i++)
        {
            yield return StartCoroutine(MoveToPoint(SetPoints[i], .2f));
            DefaultAttack();
        }
        yield return StartCoroutine(MoveToPoint(SetPoints[3], .2f));
        DefaultAttack();
        yield return StartCoroutine(MoveToPoint(SetPoints[8], .2f));
        DefaultAttack();
        yield return StartCoroutine(MoveToPoint(SetPoints[4], .2f));
        DefaultAttack();
        yield return StartCoroutine(MoveToPoint(SetPoints[7], .2f));
        DefaultAttack();
        yield return StartCoroutine(MoveToPoint(SetPoints[12], .2f));
        DefaultAttack();
        yield return new WaitForSeconds(3f);
        StartCoroutine(BattleController.BC.EndTurnEnemy());
    }

    public virtual IEnumerator MoveToPoint(Transform destination, float time)
    {
        float x = EnemyAttackSprite.position.x;
        float y = EnemyAttackSprite.position.y;
        float dx = EnemyAttackSprite.position.x - destination.position.x;
        float dy = EnemyAttackSprite.position.y - destination.position.y;
        float dxt = dx / time;
        float dyt = dy / time;

        for (float i = 0; i < time; i += .01f)
        {
            EnemyAttackSprite.position = new Vector3(-dxt*i + x, -dyt*i + y, 0);
            yield return new WaitForSeconds(.01f);
            
        }
        EnemyAttackSprite.position = destination.position;
    }

    void DefaultAttack()
    {
        GameObject attack = Instantiate(AttackPrefab);
        attack.GetComponent<Transform>().position = EnemyAttackSprite.transform.position;
    }
}
