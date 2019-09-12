using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyWalk : MonoBehaviour
{
    public float speed;
    public float idleTime;

    public SpriteRenderer sprite;
    public Transform enemyTransform;
    public BoxCollider2D range;
    protected Bounds enemyRange;
    public Animator anim;
    public Rigidbody2D rb2d;

    public bool moving;
    public bool aggro;

    protected Coroutine co;

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Encounter")
        {
            Destroy(this);
        }
 
        enemyRange = range.bounds;
    }

    // Update is called once per frame
    void Update()
    {

        if (!moving && !aggro)
        {
            Vector3 destination = RandomPointInBounds(enemyRange);
            //Debug.Log("Move");
            co = StartCoroutine(Move(destination));
        }
        else if (aggro)
        {
            moving = false;
            //Debug.Log("Kill");
            StopCoroutine(co);
        }
    }



    protected IEnumerator Move(Vector3 destination)
    {
        moving = true;
        yield return new WaitForSeconds(idleTime);
        anim.SetBool("walking", true);
        if (destination.x > enemyTransform.position.x)
        {
            sprite.flipX = true;
        }
        else
        {
            sprite.flipX = false;
        }
        while (!(Vector3.Distance(enemyTransform.position, destination) < 0.01f))
        {
            if (!GameControl.control.frozen)
            {
                while (GameControl.control.paused)
                {
                    yield return null;
                }
                enemyTransform.position = Vector3.MoveTowards(enemyTransform.position, destination, speed);
            }
            yield return new WaitForSeconds(.01f);
        }
        anim.SetBool("walking", false);
        moving = false;

    }

    protected Vector3 RandomPointInBounds(Bounds bounds)
    {
        float randy = Random.Range(bounds.min.y, bounds.max.y);
        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            randy,
            randy
            );
    }

    /*
    public IEnumerator Hop()
    {
        hopping = true;
        float x = enemyTransform.position.x;
        float y = enemyTransform.position.y;
        enemyTransform.position = new Vector3(x, y + .01f, y);
        yield return new WaitForSeconds(0.01f);
        enemyTransform.position = new Vector3(x, y + .02f, y);
        yield return new WaitForSeconds(0.01f);
        enemyTransform.position = new Vector3(x, y + .03f, y);
        yield return new WaitForSeconds(0.04f);
        enemyTransform.position = new Vector3(x, y + .02f, y);
        yield return new WaitForSeconds(0.01f);
        enemyTransform.position = new Vector3(x, y + .01f, y);
        yield return new WaitForSeconds(0.01f);
        enemyTransform.position = new Vector3(x, y, y);
        hopping = false;
    }
    */
    
}
