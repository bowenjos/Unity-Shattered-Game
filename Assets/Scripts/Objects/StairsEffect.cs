using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsEffect : MonoBehaviour {

    public float intensity;
    public int direction;

    bool isColliding;

    protected PlayerController pCon;

	// Use this for initialization
	void Start () {
        isColliding = false;
    }
	
	// Update is called once per frame
	void Update () {
	}

    //BUG: Player will retrain staircase effect if they hold the button and collided with a wall even after leaving the zone
    //REPRODUCE: Collide with wall and walk into staircase, the effect will be doubled and stay even after the player leaves the zone
    //          If you collide upwards and walk left into a zone that should push you up you go down, for some reason...
    //FIXED: When the played leaves the collision zone I set their velocity to 0, 0, 0 effectivelly reseting their movement. It doesn't solve the problem at it's source but it does solve it.
    //       Might revist later to see if I can fix the root of the issue.

    void OnTriggerEnter2D(Collider2D col)
    {
        //Checks to see if the object in the stairwell is the played (stairwells will only ever have the player on them, but the floors and other objects can cause complications)
        if (col.gameObject.tag == "Player")
        {
            if (isColliding) return;
            isColliding = true;
            pCon = col.gameObject.GetComponent<PlayerController>();
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (pCon != null)
            {
                if (pCon.walking && pCon.direction == 1)
                {
                    if (direction == 1)
                    {
                        col.gameObject.transform.position += Vector3.down * intensity * Time.deltaTime;
                    }
                    else if (direction == 3)
                    {
                        col.gameObject.transform.position += Vector3.up * intensity * Time.deltaTime;
                    }
                }
                else if (pCon.walking && pCon.direction == 3)
                {
                    if (direction == 1)
                    {
                        col.gameObject.transform.position += Vector3.up * intensity * Time.deltaTime;
                    }
                    else if (direction == 3)
                    {
                        col.gameObject.transform.position += Vector3.down * intensity * Time.deltaTime;
                    }
                }
            }
        }
    }

    void OnTriggerExit2D()
    {
        isColliding = false;
        pCon.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
        pCon = null;
    }
}
