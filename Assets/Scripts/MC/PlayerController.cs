using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    float speed = 1.5F;
    public int direction = 2;
    int count;
    //bool frozen;
    bool canPush;

    public Animator animator;
    protected ContactFilter2D contactFilter;
    protected RaycastHit2D[] hitBuffer = new RaycastHit2D[16];
    protected Rigidbody2D rb2d;
    protected List<RaycastHit2D> hitBufferList = new List<RaycastHit2D> (16);
    protected Collider2D playerCollider;
    public GameObject collideObject;
    

    void Start()
    {
        contactFilter.useTriggers = false;
        contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
        animator.SetInteger("walkDirection", 2);
        GameControl.control.frozen = false;
        canPush = false;
    }

    void Awake()
    {
        animator = GetComponent<Animator> ();
        rb2d = GetComponent<Rigidbody2D>();
    }

    /*
    public void Freeze()
    {
        animator.SetBool("walking", false);
        GameControl.control.frozen = true;
    }

    public void Unfreeze()
    {
        GameControl.control.frozen = false;
    }

    public bool IsFrozen()
    {
        return GameControl.control.frozen;
    }
    */

    public void Push()
    {
        canPush = true;
    }

    public void Unpush()
    {
        canPush = false;
    }

    void Walk(int dir, int count)
    {
        
        if (count > 0)
        {
            animator.SetInteger("walkDirection", dir);
            direction = dir;
            animator.SetBool("walking", false);
            if (Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
            { // Walk Left
                int count2 = rb2d.Cast(Vector2.left, contactFilter, hitBuffer, 0.01F);
                if(count2 == 0)
                {
                    animator.SetInteger("walkDirection", 3);
                    animator.SetBool("walking", true);
                    direction = 3;
                }
            }
            else if (Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow))
            { // Walk Right
                int count2 = rb2d.Cast(Vector2.right, contactFilter, hitBuffer, 0.01F);
                if (count2 == 0)
                {
                    animator.SetInteger("walkDirection", 1);
                    animator.SetBool("walking", true);
                    direction = 1;
                }
            }
        }
        else {
            animator.SetInteger("walkDirection", dir);
            if (dir == 0) {
                transform.position += Vector3.up * speed * Time.deltaTime;
            } else if(dir == 1) {
                transform.position += Vector3.right * speed * Time.deltaTime;
            } else if(dir == 2) {
                transform.position += Vector3.down * speed * Time.deltaTime;
            } else if(dir == 3) {
                transform.position += Vector3.left * speed * Time.deltaTime;
            }
            animator.SetBool("walking", true);
            direction = dir;
        }
        
    }

    void Realign()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y);
    }
	
	void Update ()
    {
        if (!GameControl.control.frozen)
        {
            if (Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
            { // Walk Left
                count = rb2d.Cast(Vector2.left, contactFilter, hitBuffer, 0.01F);
                Walk(3, count);
            }
            else if (Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow))
            { // Walk Right
                count = rb2d.Cast(Vector2.right, contactFilter, hitBuffer, 0.01F);
                Walk(1, count);
            }
            else {
                animator.SetBool("walking", false);
            }
            if (Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow))
            { // Walk Up
                count = rb2d.Cast(Vector2.up, contactFilter, hitBuffer, 0.01F);
                Walk(0, count);
            }
            else if (Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.UpArrow))
            { // Walk Down
                count = rb2d.Cast(Vector2.down, contactFilter, hitBuffer, 0.01F);
                Walk(2, count);
            }

            // Stop Walking
            if (!Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow))
            {
                animator.SetBool("walking", false);
            }

            if (Input.GetKeyDown(KeyCode.Z) && !GameControl.control.frozen)
            {
                if (direction == 0)
                {
                    count = rb2d.Cast(Vector2.up, contactFilter, hitBuffer, 0.1F);
                }
                if (direction == 1)
                {
                    count = rb2d.Cast(Vector2.right, contactFilter, hitBuffer, 0.1F);
                }
                if (direction == 2)
                {
                    count = rb2d.Cast(Vector2.down, contactFilter, hitBuffer, 0.1F);
                }
                if (direction == 3)
                {
                    count = rb2d.Cast(Vector2.left, contactFilter, hitBuffer, 0.1F);
                }
                if (hitBuffer[0])
                {
                    collideObject = hitBuffer[0].collider.transform.gameObject;
                    if (collideObject.GetComponent<InteractionController>() != null)
                    {
                        StartCoroutine(collideObject.GetComponent<InteractionController>().StartInteraction());

                        collideObject = GameObject.Find("nothing");
                        hitBuffer = new RaycastHit2D[16];
                    }
                }
            }
        }
        else
        {
            animator.SetBool("walking", false);
        }
        Realign();

    }
}
