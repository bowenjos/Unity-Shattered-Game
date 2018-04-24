using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerInteraction : InteractionController {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        StartInteraction();
    }
}
