using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyCombatController : MonoBehaviour {

    //A Class for storing data relevant to Monster's
    public AudioClip battleMusic;
    public Sprite backgroundSprite;
    public Sprite midgroundSprite;

    public bool fleeable;
    public int enemyNumber;

    public double enemyHealth;
    public double enemyHealthMax;
    public string enemyEmotion;
    //0 - weak, 1 - normal, 2 - resistant
    //0 - Talking, 1 - Physical Touch, 2 - Words of Affirmation, 3 - Quality Time, 4 - Acts of Charity, 5 - Gifts
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
    public GameObject LaserPrefab;

    protected Transform[] SetPoints;
    protected Transform EnemyAttackSprite;

    //START

    void Start()
    {
        if (CheckEnemy())
        {
            Destroy(GetComponentInChildren<Transform>().gameObject);
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
            GameObject.Find("BattleJukeBox").GetComponent<AudioSource>().clip = battleMusic;
            GameObject.Find("Background").GetComponent<SpriteRenderer>().sprite = backgroundSprite;
            GameObject.Find("Midground").GetComponent<SpriteRenderer>().sprite = midgroundSprite;
        }
        else
        {
            GameControl.control.encounter = false;
        }
    }

	// Same as above, however the check is performed ideal when the scene is changed to the combat scene
	void OnSceneLoaded (Scene aScene, LoadSceneMode aMode) {
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
            StartCoroutine(BattleStart());
        }
    }

	public IEnumerator BattleStart()
    {
        GameControl.control.Freeze();
        this.GetComponent<AudioSource>().Play();
        StartCoroutine(GameObject.Find("JukeBox(Clone)").GetComponent<JukeBoxController>().PauseOut(0.4f));
        yield return StartCoroutine(GameObject.Find("TransitionControl(Clone)").GetComponent<TransitionController>().EnterCombat());
        //Animation
        this.gameObject.name = "Enemy";
        this.gameObject.AddComponent<DontDestroy>();
        Destroy(this.gameObject.GetComponent<BoxCollider2D>());
        this.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
        SceneManager.LoadScene("Encounter");
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
        Debug.Log("Enemy defeated");
    }

    public virtual bool CheckEnemy()
    {
        return GameControl.control.EnemyData.jitterbugDefeated[enemyNumber];
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
        int rand = UnityEngine.Random.Range(0, numAttacks);
        switch (rand)
        {
            case 0:
                yield return new WaitForSeconds(1f);
                yield return MoveToSetpoint(SetPoints[1], .2f);
                SpawnDefaultProjectile(3f);
                yield return MoveToSetpoint(SetPoints[12], .2f);
                SpawnDefaultProjectile(3f);
                yield return MoveToSetpoint(SetPoints[11], .2f);
                SpawnDefaultProjectile(3f);
                yield return MoveToSetpoint(SetPoints[1], .2f);
                SpawnDefaultProjectile(3f);
                yield return MoveToSetpoint(SetPoints[3], .2f);
                SpawnDefaultProjectile(3f);
                break;
            case 1:
                yield return MoveToSetpoint(SetPoints[12], .1f);
                yield return MoveAroundRight(3, 12, .2f);
                SpawnDefaultProjectile(3f);
                yield return MoveAroundRight(6, 3, .2f);
                SpawnDefaultProjectile(3f);
                yield return MoveAroundRight(9, 6, .2f);
                SpawnDefaultProjectile(3f);
                yield return MoveAroundRight(12, 9, .2f);
                SpawnDefaultProjectile(3f);
                break;
            case 2:
                yield return MoveToSetpoint(SetPoints[12], .1f);
                SpawnDefaultProjectile(3f);
                yield return MoveAroundRight(9, 12, .2f);
                SpawnDefaultProjectile(3f);
                yield return MoveAroundLeft(3, 9, .2f);
                SpawnDefaultProjectile(3f);
                yield return MoveToSetpoint(SetPoints[12], .2f);
                SpawnDefaultProjectile(3f);
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
    protected void SpawnDefaultProjectile(float speed)
    {
        GameObject attack = Instantiate(AttackPrefab);
        attack.name = "Attack";
        attack.GetComponent<DefaultAttack>().speed = speed;
        attack.GetComponent<Transform>().position = EnemyAttackSprite.transform.position;
    }

    protected IEnumerator SpawnLaserProjectile(float speed, int total)
    {
        GameObject[] attack = new GameObject[total];
        yield return new WaitForSeconds(speed);
        for (int i = 0; i < total; i++)
        {
            attack[i] = Instantiate(LaserPrefab);
            attack[i].name = "Attack";
            Transform thisTransform = attack[i].GetComponent<Transform>();
            thisTransform.position = EnemyAttackSprite.transform.position;


            double value = Math.Atan(thisTransform.position.y / thisTransform.position.x) * (180 / Math.PI);
            Vector3 newAngle = new Vector3(0f, 0f, (float)value);
            Quaternion from = thisTransform.rotation;
            Quaternion to = Quaternion.Euler(newAngle);
            thisTransform.localRotation = Quaternion.Lerp(from, to, 1);

            yield return new WaitForSeconds(0.002f);
        }
        
        //Set speed
        //rotate object
        //stretch from 
    }

    IEnumerator AllProjectilesGone()
    {
        do
        {
            yield return null;
        } while (GameObject.Find("Attack") != null);
        yield return new WaitForSeconds(.3f);
    }

}
