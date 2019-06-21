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
    public int numAttacks;

    public int rewardMoney;

    public Sprite enemyMainSprite;
    public Sprite enemyTurnSprite;

    public string introDialogue;
    public string outroDialogue;
    public string[] talkDialogue;
    public string[] sitDialogue;
    public string[] hugDialogue;
    public string[] actDialogue;
    public string[] affirmDialogue;
    public string[] giftDialogue;

    //public string[] reactionDialogue;
    public string[][] enemyChatter;
    public string[] playerTurnIdle;

    public GameObject AttackPrefab;

    private Transform[] SetPoints;
    private Transform EnemyAttackSprite;

    //START

    void Start()
    {
        if (!GameControl.control.MainRoom.monikaAlive)
        {
            Destroy(this.gameObject);
        }
        SceneManager.sceneLoaded += OnSceneLoaded;
        //Check to see if the current scene is encounter, if so, set up the variables, if not, ignore.
        if (SceneManager.GetActiveScene().name == "Encounter")
        {
            GameControl.control.encounter = true;
            SetPoints = GameObject.Find("SetPoints").GetComponentsInChildren<Transform>();
            EnemyAttackSprite = GameObject.Find("EnemyAttackPhaseSprite").GetComponent<Transform>();
            GameObject.Find("EnemyAttackPhaseSprite").GetComponent<SpriteRenderer>().sprite = enemyTurnSprite;
            GameObject.Find("EnemySprite").GetComponent<SpriteRenderer>().sprite = enemyMainSprite;
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
            EnemyAttackSprite = GameObject.Find("EnemyAttackPhaseSprite").GetComponent<Transform>();
            GameObject.Find("EnemyAttackPhaseSprite").GetComponent<SpriteRenderer>().sprite = enemyTurnSprite;
            GameObject.Find("EnemySprite").GetComponent<SpriteRenderer>().sprite = enemyMainSprite;
        }
        else
        {
            GameControl.control.encounter = false;
        }
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "player(Clone)")
        {
            GameControl.control.Freeze();
            StartCoroutine(GameObject.Find("JukeBox(Clone)").GetComponent<JukeBoxController>().FadeOut(0.4f));
            //Animation
            this.gameObject.name = "Enemy";
            this.gameObject.AddComponent<DontDestroy>();
            Destroy(this.gameObject.GetComponent<BoxCollider2D>());
            this.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
            SceneManager.LoadScene("Encounter");
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


    //Make this specific enemy dead
    public virtual void ResolveEnemy()
    {
        GameControl.control.MainRoom.monikaAlive = false;
    }

    //All actions performed on an Enemies turn
    public virtual IEnumerator EnemyTurn()
    {
        yield return SelectAttack();
        yield return AllProjectilesGone();
        yield return BattleController.BC.EndTurnEnemy();
    }

    //Selection of the enemies attack
    public virtual IEnumerator SelectAttack()
    {
        numAttacks = 3;
        int rand = Random.Range(0, numAttacks);
        switch (rand)
        {
            case 0:
                yield return new WaitForSeconds(1f);
                yield return MoveToSetpoint(SetPoints[1], .2f);
                SpawnDefaultProjectile();
                yield return MoveToSetpoint(SetPoints[12], .2f);
                SpawnDefaultProjectile();
                yield return MoveToSetpoint(SetPoints[11], .2f);
                SpawnDefaultProjectile();
                yield return MoveToSetpoint(SetPoints[1], .2f);
                SpawnDefaultProjectile();
                yield return MoveToSetpoint(SetPoints[3], .2f);
                SpawnDefaultProjectile();
                break;
            case 1:
                yield return MoveToSetpoint(SetPoints[12], .1f);
                yield return MoveAroundRight(3, 12, .2f);
                SpawnDefaultProjectile();
                yield return MoveAroundRight(6, 3, .2f);
                SpawnDefaultProjectile();
                yield return MoveAroundRight(9, 6, .2f);
                SpawnDefaultProjectile();
                yield return MoveAroundRight(12, 9, .2f);
                SpawnDefaultProjectile();
                break;
            case 2:
                yield return MoveToSetpoint(SetPoints[12], .1f);
                SpawnDefaultProjectile();
                yield return MoveAroundRight(9, 12, .2f);
                SpawnDefaultProjectile();
                yield return MoveAroundLeft(3, 9, .2f);
                SpawnDefaultProjectile();
                yield return MoveToSetpoint(SetPoints[12], .2f);
                SpawnDefaultProjectile();
                break;
        }

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

    IEnumerator AllProjectilesGone()
    {
        do
        {
            yield return null;
        } while (GameObject.Find("Attack(Clone)") != null);
    }

}
