using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MirrorController : MonoBehaviour {

    public float damageValue;

    public GameObject bf;

    protected ContactFilter2D contactFilter;
    protected List<RaycastHit2D> hitBufferList = new List<RaycastHit2D>(16);
    protected Collider2D mirrorCollider;
    

    // Use this for initialization
    void Start () {
        contactFilter.useTriggers = false;
        contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
    }
	
	// Update is called once per frame
	void Update () {
		if(GameControl.control.health <= 0)
        {
            //Play Shattered Effect
            StopAllCoroutines();
            SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
            Debug.Log("You have Shattered");
        }
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "TrebleClefPF(Clone)" || col.gameObject.name == "EnemyNotePF(Clone)")
        {
            //GameControl.control.health -= (damageValue * bf.GetComponent<BattleFlow>().enemyDamageModifier);
            this.GetComponent<Shake>().StartShake(0.1f);
            Destroy(col.gameObject);
        }
    }

    
}
