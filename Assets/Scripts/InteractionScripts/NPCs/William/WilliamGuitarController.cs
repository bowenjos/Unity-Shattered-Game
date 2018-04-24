﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WilliamGuitarController : MonoBehaviour {

    bool changed;
    Animator anim;
    Light thisLight;

    public Texture GuitarCookie0;
    public Texture GuitarCookie1;

	// Use this for initialization
	void Start () {
        anim = this.gameObject.GetComponent<Animator>();
        thisLight = this.gameObject.GetComponentInChildren<Light>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!changed)
        {
            StartCoroutine(Change());
        }
        
	}

    IEnumerator Change()
    {
        changed = true;
        int rand = Random.Range(0, 3);
        anim.SetInteger("Style", rand);
        yield return new WaitForSeconds(0.5f);
        rand = Random.Range(0, 2);
        if(rand == 0)
        {
            anim.SetBool("Turned", false);
            thisLight.cookie = GuitarCookie0;
        }
        else
        {
            anim.SetBool("Turned", true);
            thisLight.cookie = GuitarCookie1;
        }
        float rand2 = Random.Range(1f, 3f);
        yield return new WaitForSeconds(rand2);
        changed = false;
    }
}
