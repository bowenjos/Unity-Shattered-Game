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

    public Sprite enemyMainSprite;
    public Sprite enemyTurnSprite;

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

    //START

    void Start()
    {
        //Check to see if the current scene is encounter, if so, set up the variables, if not, ignore.
        if (SceneManager.GetActiveScene().name == "Encounter")
        {
            GameControl.control.encounter = true;
            SetPoints = GameObject.Find("SetPoints").GetComponentsInChildren<Transform>();
            EnemyAttackSprite = GameObject.Find("EnemyAttackPhaseSprite").GetComponent<Transform>();
            GameObject.Find("EnemyAttackPhaseSprite").GetComponent<SpriteRenderer>().sprite = enemyTurnSprite;
            GameObject.Find("EnemySprite").GetComponent<SpriteRenderer>().sprite = enemyMainSprite;
            Debug.Log(SetPoints.Length);
        }
        else
        {
            GameControl.control.encounter = false;
        }
    }

	// Same as above, however the check is performed ideal when the scene is changed to the combat scene
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
        if (GameControl.control.encounter)
        {
            //Checks to see if it's the enemies turn, and if they've started said turn yet, if met, the enemy starts their turn.
            if (BattleController.BC.currentState == BattleController.BattleState.EnemyTurn && BattleController.BC.enemyTurnStart)
            {
                BattleController.BC.enemyTurnStart = false;
                StartCoroutine(EnemyTurn());
            }
        }
	}

    //All actions performed on an Enemies turn
    public virtual IEnumerator EnemyTurn()
    {
        yield return SelectAttack();
        yield return BattleController.BC.EndTurnEnemy();
    }

    //Selection of the enemies attack
    public virtual IEnumerator SelectAttack()
    {
        yield return new WaitForSeconds(1f);
    }

    //Move directly from the enemies current setpoint, so a new setpoint.
    public virtual IEnumerator MoveToSetpoint(Transform destination, float time)
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

    //Move from the current setpoints to a new setpoint going counter clockwise
    public virtual IEnumerator MoveAroundLeft(int destination, int position, float time)
    {
        while(destination != position)
        {
            if(position != 1)
            {
                position -= 1;
            }
            else
            {
                position = 12;
            }
            yield return MoveToSetpoint(SetPoints[position], time);
        }
    }

    //Move from the current setpoint to a new setpoint going clockwise
    public virtual IEnumerator MoveAroundRight(int destination, int position, float time)
    {
        while (destination != position)
        {
            if (position != 12)
            {
                position += 1;
            }
            else
            {
                position = 1;
            }
            yield return MoveToSetpoint(SetPoints[position], time);
        }
    }

    public virtual IEnumerator MoveToPoint(int x, int y, float time)
    {
        float xn = EnemyAttackSprite.position.x;
        float yn = EnemyAttackSprite.position.y;
        float dx = EnemyAttackSprite.position.x - x;
        float dy = EnemyAttackSprite.position.y - y;
        float dxt = dx / time;
        float dyt = dy / time;

        for (float i = 0; i < time; i += .01f)
        {
            EnemyAttackSprite.position = new Vector3(-dxt * i + xn, -dyt * i + yn, 0);
            yield return new WaitForSeconds(.01f);

        }
        EnemyAttackSprite.position = new Vector3(x, y, 0);
    }


    //Spawn the default projectile
    void SpawnDefaultProjectile()
    {
        GameObject attack = Instantiate(AttackPrefab);
        attack.GetComponent<Transform>().position = EnemyAttackSprite.transform.position;
    }
}
