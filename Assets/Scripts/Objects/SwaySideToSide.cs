using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwaySideToSide : MonoBehaviour {

    public float modifier;
    public float speed;

    float mod;
    float defaultx;

    Transform tf;

    // Use this for initialization
    void Start () {
        tf = GetComponent<Transform>();
        defaultx = tf.position.x;
    }
	
	// Update is called once per frame
	void Update () {
        mod = (modifier * 0.01f * (float)Math.Sin(speed * Time.time));
        tf.position = new Vector3(defaultx + mod, tf.position.y, tf.position.z);
    }
}
