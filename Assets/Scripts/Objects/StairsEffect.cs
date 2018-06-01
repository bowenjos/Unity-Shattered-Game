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
		
	}
	
	// Update is called once per frame
	void Update () {
        isColliding = false;
	}

    //BUG: Something about this whole system isn't working right now
    //REPRODUCE: Collide with wall and walk into staircase, the effect will be doubled and stay even after the player leaves the zone
    //          If you collide upwards and walk left into a zone that should push you up you go down, for some reason...

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log(col.transform.name);
        if (isColliding) return;
        isColliding = true;
        if(col.gameObject.tag == "Player")
        {
            pCon = col.gameObject.GetComponent<PlayerController>();
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Debug.Log(col.transform.name);
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

    void OnTriggerExit()
    {
        pCon = null;
    }
}
