using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    float speed = 2F;
    public int direction = 2;
    public bool walking;
    public bool walking2;
    public bool pushing;
    public bool stopped;
    int count;
    //bool frozen;
    public bool canPush;

    public Animator animator;
    protected ContactFilter2D contactFilter;
    protected RaycastHit2D[] hitBuffer = new RaycastHit2D[16];
    protected Rigidbody2D rb2d;
    protected List<RaycastHit2D> hitBufferList = new List<RaycastHit2D> (16);
    protected Collider2D playerCollider;
    public GameObject collideObject;
    
    //Initialize player character status variables

    void Start()
    {
        contactFilter.useTriggers = false;
        contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
        animator.SetInteger("walkDirection", 2);
        GameControl.control.frozen = false;
        canPush = false;
        walking = false;
        walking2 = false;
        stopped = false;
    }

    //Acquire access to specific parts of the player character

    void Awake()
    {
        animator = GetComponent<Animator> ();
        rb2d = GetComponent<Rigidbody2D>();
    }

    
    //Set it so te player character can push objects
    public void CanPush()
    {
        canPush = true;
    }

    //Remove the ability for the player character to push objects
    public void CannotPush()
    {
        canPush = false;
    }

    //What follows was an alternate take on the walking function, it ended up being more complicated and less effective than the form I use currently, but is preserved in case i desire to revist it

    /*
    IEnumerator WalkAngled(int dir, int count, KeyCode keyPressed)
    {
        walking2 = true;
        //Debug.Log(stopped);
        Debug.Log("I'm walking angled here!");
        while (Input.GetKey(keyPressed) && walking)
        {
            if (stopped == true)
            {
                switch (dir)
                {
                    case 0:
                        if (0 < rb2d.Cast(Vector2.up, contactFilter, hitBuffer, 0.01F))
                        {
                            animator.SetBool("walking", false);
                        }
                        else
                        {
                            animator.SetBool("walking", true);
                            rb2d.velocity = new Vector3(rb2d.velocity.x, 1.75f, 0);
                            animator.SetInteger("walkDirection", 0);
                        }
                        break;
                    case 1:
                        if (0 < rb2d.Cast(Vector2.right, contactFilter, hitBuffer, 0.01F))
                        {
                            animator.SetBool("walking", false);
                        }
                        else
                        {
                            animator.SetBool("walking", true);
                            rb2d.velocity = new Vector3(1.75f, rb2d.velocity.y, 0);
                            animator.SetInteger("walkDirection", 1);
                        }
                        break;
                    case 2:
                        if (0 < rb2d.Cast(Vector2.down, contactFilter, hitBuffer, 0.01F))
                        {
                            animator.SetBool("walking", false);
                        }
                        else
                        {
                            animator.SetBool("walking", true);
                            rb2d.velocity = new Vector3(rb2d.velocity.x, -1.75f, 0);
                            animator.SetInteger("walkDirection", 2);
                        }
                        break;
                    case 3:
                        if (0 < rb2d.Cast(Vector2.left, contactFilter, hitBuffer, 0.01F))
                        {
                            animator.SetBool("walking", false);
                        }
                        else
                        {
                            animator.SetBool("walking", true);
                            rb2d.velocity = new Vector3(-1.75f, rb2d.velocity.y, 0);
                            animator.SetInteger("walkDirection", 3);
                        }
                        break;
                }
            }
            else
            {
                switch (dir)
                {
                    case 0:
                        rb2d.velocity = new Vector3(rb2d.velocity.x, 1.75f, 0);
                        break;
                    case 1:
                        rb2d.velocity = new Vector3(1.75f, rb2d.velocity.y, 0);
                        break;
                    case 2:
                        rb2d.velocity = new Vector3(rb2d.velocity.x, -1.75f, 0);
                        break;
                    case 3:
                        rb2d.velocity = new Vector3(-1.75f, rb2d.velocity.y, 0);
                        break;
                }
            }

            yield return null;
        }
        rb2d.velocity = new Vector3(0, 0, 0);
        stopped = false;
        walking = false;
        walking2 = false;
        Debug.Log("I'm not walking angled here!");
    }

    
    IEnumerator Walk2(int dir, int count, KeyCode keyPressed)
    {
        Debug.Log("I'm walking here!");
        while (Input.GetKey(keyPressed))
        {
            walking = true;
            direction = dir;
            if (!stopped)
            {
                animator.SetInteger("walkDirection", dir);
            }
            //Check for collision against wall to stop walking animation
            switch (dir)
            {
                case 0:
                    count = rb2d.Cast(Vector2.up, contactFilter, hitBuffer, 0.01F);
                    break;
                case 1:
                    count = rb2d.Cast(Vector2.right, contactFilter, hitBuffer, 0.01F);
                    break;
                case 2:
                    count = rb2d.Cast(Vector2.down, contactFilter, hitBuffer, 0.01F);
                    break;
                case 3:
                    count = rb2d.Cast(Vector2.left, contactFilter, hitBuffer, 0.01F);
                    break;
            }
            //If no collision keep moving
            if (count == 0)
            {
                stopped = false;
                animator.SetBool("walking", true);
                switch (dir)
                {
                    case 0:
                        rb2d.velocity = new Vector3(rb2d.velocity.x, 1.75f, 0);
                        break;
                    case 1:
                        rb2d.velocity = new Vector3(1.75f, rb2d.velocity.y, 0);
                        break;
                    case 2:
                        rb2d.velocity = new Vector3(rb2d.velocity.x, -1.75f, 0);
                        break;
                    case 3:
                        rb2d.velocity = new Vector3(-1.75f, rb2d.velocity.y, 0);
                        break;
                }
            }
            //If collision, stop
            else
            {
                stopped = true;
                //animator.SetBool("walking", false);
            }

            //Start a function if the player starts walking with two directions
            if (!walking2)
            {
                if (keyPressed == KeyCode.UpArrow || keyPressed == KeyCode.DownArrow)
                {
                    if (Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
                    { // Walk Left
                        count = rb2d.Cast(Vector2.left, contactFilter, hitBuffer, 0.01F);
                        StartCoroutine(WalkAngled(3, count, KeyCode.LeftArrow));
                    }
                    else if (Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow))
                    { // Walk Right
                        count = rb2d.Cast(Vector2.right, contactFilter, hitBuffer, 0.01F);
                        StartCoroutine(WalkAngled(1, count, KeyCode.RightArrow));
                    }
                    else if(stopped == true)
                    {
                        animator.SetBool("walking", false);
                    }
                }
                if (keyPressed == KeyCode.LeftArrow || keyPressed == KeyCode.RightArrow)
                {
                    if (Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow))
                    { // Walk Up
                        count = rb2d.Cast(Vector2.up, contactFilter, hitBuffer, 0.01F);
                        StartCoroutine(WalkAngled(0, count, KeyCode.UpArrow));
                    }
                    else if (Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.UpArrow))
                    { // Walk Down
                        count = rb2d.Cast(Vector2.down, contactFilter, hitBuffer, 0.01F);
                        StartCoroutine(WalkAngled(2, count, KeyCode.DownArrow));
                    }
                    else if (stopped == true)
                    {
                        animator.SetBool("walking", false);
                    }
                }
            }

            yield return null;
        }
        Debug.Log("I'm not walking here!");
        walking = false;
        stopped = false;
        animator.SetBool("walking", false);
        rb2d.velocity = new Vector3(0, 0, 0);
    }
    
    */

    /***********************
    * Function Name: Push
    * Type: IEnumerator
    * Arguments: direction the player is facing, the number of objects in front of the player, and the keypress of the direction they are facing
    * Purpose: To allow the player character to push objects in front of them, prevent them from pushing more than 1 object at a time. 
    ***********************/

    IEnumerator Push(int dir, int count, KeyCode keyPressed)
    {
        pushing = true;
        //Check to see if the object in front of the player is pushable
        if(hitBuffer[0].collider.transform.gameObject.GetComponent<Pushable>() != null)
        {
            //If the object is pushable set that object's variable "beingPushed" to true
            hitBuffer[0].collider.transform.gameObject.GetComponent<Pushable>().beingPushed = true;
        }
        //While the player continues to hold the directional key
        while (Input.GetKey(keyPressed))
        {
            //Begin the pushing animation for the direction they are moving
            animator.SetInteger("walkDirection", dir);
            animator.SetBool("pushing", true);
            //Set the players velocity for that direction
            if (dir == 0)
            {
                rb2d.velocity = new Vector3(0, speed, 0);
            }
            else if (dir == 1)
            {
                rb2d.velocity = new Vector3(speed, 0, 0);
            }
            else if (dir == 2)
            {
                rb2d.velocity = new Vector3(0, -speed, 0);
            }
            else if (dir == 3)
            {
                rb2d.velocity = new Vector3(-speed, 0, 0);
            }

            yield return null;
        }
        //Once no longer pushing, set the object being pushed back to false and remove the animation
        hitBuffer[0].collider.transform.gameObject.GetComponent<Pushable>().beingPushed = false;
        pushing = false;
        animator.SetBool("pushing", false);
    }
    
    /**********************
    * Function Name: Walk
    * Type: void
    * Arguments: direction the player is facing, number of objects in front of the player
    * Purpose: To allow the player to move (cause the game would be real boring without it)
    **********************/
    void Walk(int dir, int count)
    {
        //Checks to see if the number of objects in front of the player is more than 0
        if (count > 0)
        {
            //If true the players walk direction is set facing that direction, but walking is set to false, a new check is performed
            animator.SetInteger("walkDirection", dir);
            direction = dir;
            animator.SetBool("walking", false);
            //Checks to see if the player is pushing either the left or the right arrow, but not both at the same time
            if (Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
            { // Walk Left
                //If Left is pressed, a check is perfroemd to see if there are objects in front of the player
                int count2 = rb2d.Cast(Vector2.left, contactFilter, hitBuffer, 0.02F);
                if(count2 == 0)
                {
                    //If there are no objects in front of the player, they can walk left freely
                    animator.SetInteger("walkDirection", 3);
                    animator.SetBool("walking", true);
                    walking = true;
                    direction = 3;
                }
            }
            else if (Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow))
            { // Walk Right
                //If Right is pressed, a check is performed to see if there are objects in front of the player
                int count2 = rb2d.Cast(Vector2.right, contactFilter, hitBuffer, 0.02F);
                if (count2 == 0)
                {
                    //If there are no objects in front of the player, they can walk right freely
                    animator.SetInteger("walkDirection", 1);
                    animator.SetBool("walking", true);
                    walking = true;
                    direction = 1;
                }
            }
            //NOTE: Because of the nature of the FixedUpdate directionary check function, it is NOT necessary to check for up or down if the player is going left or right, 
            // this is because when fixed update is checked every frame the up and down directions occur AFTER the left or right directions meaning if they are pressed at the same time
            // the left or right will be the final one that occurs that frame. This is due to the fact that all functions that aren't IEnumerators are completed in a single frame.
        }
        else {
            //If there is nothing in front of the player, their direction is set that direction
            animator.SetInteger("walkDirection", dir);
            //Sets the players velocity based on the direction they are going
            if (dir == 0) {
                rb2d.velocity = new Vector3(rb2d.velocity.x, speed, 0);
            } else if(dir == 1) {
                rb2d.velocity = new Vector3(speed, rb2d.velocity.y, 0);
            } else if(dir == 2) {
                rb2d.velocity = new Vector3(rb2d.velocity.x, -speed, 0);
            } else if(dir == 3) {
                rb2d.velocity = new Vector3(-speed, rb2d.velocity.y, 0);
            }
            animator.SetBool("walking", true);
            walking = true;
            direction = dir;
        }
    }
    
    /*****************
    * Function Name: Realign()
    * Type: void
    * Purpose: Keeps the player's y and z position algned so as to allow the player to appear visually behind obejcts that should be in front of them
    *           Runs every frame
    ******************/

    void Realign()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y);
    }

    IEnumerator Held(KeyCode keypressed, int dir)
    {
        do
        {
            yield return null;
        } while (Input.GetKey(keypressed));

        if (dir == 0)
        {
            rb2d.velocity = new Vector3(rb2d.velocity.x, 0, 0);
        }
        else if (dir == 1)
        {
            rb2d.velocity = new Vector3(0, rb2d.velocity.y, 0);
        }
        else if (dir == 2)
        {
            rb2d.velocity = new Vector3(rb2d.velocity.x, 0, 0);
        }
        else if (dir == 3)
        {
            rb2d.velocity = new Vector3(0, rb2d.velocity.y, 0);
        }
    }
	
	void FixedUpdate ()
    {
        Array.Clear(hitBuffer, 0, hitBuffer.Length);
        if (!GameControl.control.frozen)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                speed = 2.5f;
            }
            else
            {
                speed = 1.2f;
            }

            if (!pushing)
            {
                if (Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
                { // Walk Left
                    count = rb2d.Cast(Vector2.left, contactFilter, hitBuffer, 0.02F);
                    if (count > 0 && hitBuffer[0].collider.transform.gameObject.GetComponent<Pushable>())
                    {
                        StartCoroutine(Push(3, count, KeyCode.LeftArrow));
                    }
                    else
                    {
                        Walk(3, count);
                    }

                    StartCoroutine(Held(KeyCode.LeftArrow, 3));
                }
                else if (Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow))
                { // Walk Right
                    count = rb2d.Cast(Vector2.right, contactFilter, hitBuffer, 0.02F);
                    if (count > 0 && hitBuffer[0].collider.transform.gameObject.GetComponent<Pushable>())
                    {
                        StartCoroutine(Push(1, count, KeyCode.RightArrow));
                    }
                    else
                    {
                        Walk(1, count);
                    }
                    StartCoroutine(Held(KeyCode.RightArrow, 1));
                }
                else
                {
                    animator.SetBool("walking", false);
                    walking = false;
                }

                if (Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow))
                { // Walk Up
                    count = rb2d.Cast(Vector2.up, contactFilter, hitBuffer, 0.02F);
                    if (count > 0 && hitBuffer[0].collider.transform.gameObject.GetComponent<Pushable>())
                    {
                        StartCoroutine(Push(0, count, KeyCode.UpArrow));
                    }
                    else
                    {
                        Walk(0, count);
                    }
                    StartCoroutine(Held(KeyCode.UpArrow, 0));
                }
                else if (Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.UpArrow))
                { // Walk Down
                    count = rb2d.Cast(Vector2.down, contactFilter, hitBuffer, 0.02F);
                    if (count > 0 && hitBuffer[0].collider.transform.gameObject.GetComponent<Pushable>())
                    {
                        StartCoroutine(Push(2, count, KeyCode.DownArrow));
                    }
                    else
                    {
                        Walk(2, count);
                    }
                    StartCoroutine(Held(KeyCode.DownArrow, 2));
                }
            }

            // Stop Walking
            if (!Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow))
            {
                animator.SetBool("walking", false);
                rb2d.velocity = new Vector3(0, 0, 0);
                walking = false;
            }

            //Check to see if their is an object in front of the player and begin interaction
            if (Input.GetKeyDown(KeyCode.Z) && !GameControl.control.frozen)
            {
                if (direction == 0)
                {
                    count = rb2d.Cast(Vector2.up, contactFilter, hitBuffer, 0.02F);
                }
                if (direction == 1)
                {
                    count = rb2d.Cast(Vector2.right, contactFilter, hitBuffer, 0.02F);
                }
                if (direction == 2)
                {
                    count = rb2d.Cast(Vector2.down, contactFilter, hitBuffer, 0.02F);
                }
                if (direction == 3)
                {
                    count = rb2d.Cast(Vector2.left, contactFilter, hitBuffer, 0.02F);
                }
                if (hitBuffer[0])
                {
                    collideObject = hitBuffer[0].collider.transform.gameObject;
                    //Check to see if the object in front is interactable
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
            //If the player is frozen set them to not walking
            animator.SetBool("walking", false);
            rb2d.velocity = new Vector3(0, 0, 0);
            walking = false;
        }
        //Make sure the players Z lines up with their Y, for the purpose of appearing in front of or behind objects.
        Realign();

    }
}
