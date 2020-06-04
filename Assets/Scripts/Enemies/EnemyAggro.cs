using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyAggro : MonoBehaviour
{
    public SpriteRenderer sprite;
    public EnemyIdle enemyAggroStyle;
    public Transform enemyTransform;
    public Animator anim;
    public Animator animAggro;

    protected bool moved;
    public bool stop;

    // Start is called before the first frame update
    void Start()
    {
        /*
        if (SceneManager.GetActiveScene().name == "Encounter")
        {
            Debug.Log("Twice?");
            //Destroy(this);
        }
        */
        moved = false;
        stop = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameControl.control.stunned)
        {
            stop = true;
        }
        else
        {
            stop = false;
        }
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.name == "player(Clone)" && !stop)
        {
            enemyAggroStyle.aggro = true;
            animAggro.SetBool("Aggro", true);
        }
    }

    public void OnTriggerStay2D(Collider2D col)
    {
        
        if (col.gameObject.name == "player(Clone)" && !moved && !GameControl.control.paused && !stop)
        {
            anim.SetBool("walking", true);
            StartCoroutine(Move(col));
        }
        if (stop)
        {
            animAggro.SetBool("Aggro", false);
        }
    }

    private IEnumerator Move(Collider2D col)
    {
        moved = true;
        if (col.transform.position.x > enemyTransform.position.x)
        {
            sprite.flipX = true;
        }
        else
        {
            sprite.flipX = false;
        }
        if (!GameControl.control.frozen)
        {
            enemyTransform.position = Vector3.MoveTowards(enemyTransform.position, col.transform.position, 3 * enemyAggroStyle.speed);
        }
        yield return new WaitForSeconds(0.01f);
        moved = false;
        
    }

    public void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.name == "player(Clone)")
        {
            
            enemyAggroStyle.moving = false;
            enemyAggroStyle.aggro = false;
            anim.SetBool("walking", false);
            animAggro.SetBool("Aggro", false);
        }
        
    }
}
