using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatInPlace : MonoBehaviour {

    public float modifier;
    public float speed;

    Transform tf;

	// Use this for initialization
	void Start () {
        tf = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
        tf.position = new Vector3(tf.position.x, tf.position.y + (modifier * 0.01f * (float)Math.Sin(speed*Time.time)), tf.position.z);
	}
}
