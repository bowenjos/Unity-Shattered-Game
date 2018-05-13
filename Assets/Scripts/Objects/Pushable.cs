using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pushable : MonoBehaviour {

    Transform thisObject;
    PlayerController pCon;

    void Start()
    {
        thisObject = this.gameObject.transform;
        pCon = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

	// Update is called once per frame
	void Update () {
        thisObject.position = new Vector3(thisObject.position.x, thisObject.position.y, thisObject.position.y);

        if (!pCon.canPush)
        {
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        }
        else
        {
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }
}
