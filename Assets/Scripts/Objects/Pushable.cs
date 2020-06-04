using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pushable : MonoBehaviour {

    Transform thisObject;
    PlayerController pCon;
    Rigidbody2D rb2d;

    public bool beingPushed;

    void Start()
    {
        thisObject = this.gameObject.transform;
        pCon = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        rb2d = GetComponent<Rigidbody2D>();
        beingPushed = false;
    }

	// Update is called once per frame
	void Update () {
        thisObject.position = new Vector3(thisObject.position.x, thisObject.position.y, thisObject.position.y);

        if (pCon.canPush && pCon.pushing && beingPushed)
        {
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        }
        else
        {
            beingPushed = false;
            rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }
}
