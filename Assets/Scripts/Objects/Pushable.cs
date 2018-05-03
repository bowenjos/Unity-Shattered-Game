﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pushable : MonoBehaviour {

    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    Transform thisObject;

    void Start()
    {
        thisObject = this.gameObject.transform;
    }

	// Update is called once per frame
	void Update () {
        thisObject.position = new Vector3(thisObject.position.x, thisObject.position.y, thisObject.position.y);
        if(thisObject.position.x > maxX)
        {
            thisObject.position = new Vector3(maxX, thisObject.position.y, thisObject.position.y);
        }
        else if(thisObject.position.x < minX)
        {
            thisObject.position = new Vector3(minX, thisObject.position.y, thisObject.position.y);
        }
        if(thisObject.position.y > maxY)
        {
            thisObject.position = new Vector3(thisObject.position.x, maxY, maxY);
        }
        if (thisObject.position.y < minY)
        {
            thisObject.position = new Vector3(thisObject.position.x, minY, minY);
        }
    }
}
