using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnemyCombatControl : MonoBehaviour {

    public Sprite battleSprite;
    public string sceneName;
    public string currentSceneSaveFile;
    public bool fleeable;

    public double enemyHealth;
    public double enemyHealthReset;
    public string enemyEmotion;
    public int enemyLevelState;
    public int enemyDamageValue;
    public int numberOfAttacks;
    public int tempo;
    public static bool hasAttacked;
    protected bool combat;

    public GameObject Player;
    protected GameObject Enemy;
    protected GameObject Talk;
    protected BattleFlow bf;
    protected GameObject playerTalk;
    protected EnemyHornController eHC;
    public Transform combatFlash;
    public int[,] enemyResistences;
    public string[][] dialogueRespondTalk;
    public string[][] dialogueIdle;
    public string[][] dialoguePlayerTalk;
    public string[] dialogueStateChange;
    public string[][] dialogueBoss;
    public string dialogueApproach;

    // Use this for initialization
    void Start () {
        hasAttacked = false;
        combat = false;
    }

    // Update is called once per frame
    public virtual void Update () {
		if(hasAttacked == false && BattleFlow.playerOrEnemyPhase == 1)
        {
            //attacked = true;
            StartCoroutine(Attack());
            
        }
        if(combat == true){
            if(bf.expelled == true)
            {
                DestroyThis();
            }  
        }
	}
    
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Combat")
        {
            combat = true;

            Enemy = GameObject.Find("EnemySprite");
            eHC = GameObject.Find("EnemyHorn").GetComponent<EnemyHornController>();
            Talk = GameObject.Find("FoeChatterText");
            bf = GameObject.Find("BattleControl").GetComponent<BattleFlow>();
            playerTalk = GameObject.Find("PlayerChatterText");

        }
        else
        {
            combat = false;
        }
    }
    

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "player(Clone)")
        {
            
            GameControl.control.Freeze();
            GameObject.Find("player(Clone)").GetComponent<PlayerController>().animator.SetBool("walking", false);
            Instantiate(combatFlash, new Vector2(this.transform.position.x, this.transform.position.y), combatFlash.rotation, this.transform);
            this.name = "Enemy";
            Invoke("ChangeScene", 1);
        }
    }

    void ChangeScene()
    {
        Destroy(GameObject.Find("EnterCombatFlash(Clone)"));
        this.gameObject.AddComponent<DontDestroy>();
        this.GetComponent<Renderer>().enabled = false;
        
        SceneManager.LoadScene("Combat", LoadSceneMode.Single);
        //yield return null;
    }

    public virtual IEnumerator Attack()
    {
        hasAttacked = true;
        yield return null;
    }

    public virtual void DestroyThis()
    {
        Destroy(this);
    }

    public virtual void CheckThis()
    {
        
    }
}
