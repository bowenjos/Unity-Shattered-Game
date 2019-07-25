using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAggro : MonoBehaviour
{
    public SpriteRenderer sprite;
    public EnemyWalk enemyWalk;
    public Transform enemyTransform;
    public Animator anim;

    protected bool moved;

    // Start is called before the first frame update
    void Start()
    {
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
            StartCoroutine(enemyWalk.Hop());
        }
    }

    public void OnTriggerStay2D(Collider2D col)
    {
        
        if (col.gameObject.name == "player(Clone)" && !enemyWalk.hopping && !moved)
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
            enemyWalk.aggro = false;
            enemyWalk.moving = false;
            anim.SetBool("walking", false);
        }
        
    }
}
