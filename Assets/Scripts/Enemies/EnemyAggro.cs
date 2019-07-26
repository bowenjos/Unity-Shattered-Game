using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyAggro : MonoBehaviour
{
    public SpriteRenderer sprite;
    public EnemyWalk enemyWalk;
    public Transform enemyTransform;
    public Animator anim;
    public Animator animAggro;

    protected bool moved;

    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Encounter")
        {
            Destroy(this);
        }
        moved = false;
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.name == "player(Clone)")
        {
            enemyWalk.aggro = true;
            animAggro.SetBool("Aggro", true);
        }
    }

    public void OnTriggerStay2D(Collider2D col)
    {
        
        if (col.gameObject.name == "player(Clone)" && !moved && !GameControl.control.paused)
        {
            anim.SetBool("walking", true);
            StartCoroutine(Move(col));
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
        enemyTransform.position = Vector3.MoveTowards(enemyTransform.position, col.transform.position, 3 * enemyWalk.speed);
        yield return new WaitForSeconds(0.01f);
        moved = false;
        
    }

    public void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.name == "player(Clone)")
        {
            
            enemyWalk.moving = false;
            anim.SetBool("walking", false);
            animAggro.SetBool("Aggro", false);
        }
        
    }
}
