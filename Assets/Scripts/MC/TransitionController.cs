﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionController : MonoBehaviour {

    private bool dark = false;

    void Update()
    {
        if(dark == true)
        {
            StartCoroutine(transitionIn());
        }
    }

    public IEnumerator transitionOut()
    {
        for(float i = 0f; i < 255f; i += 17f)
        {
            this.GetComponent<SpriteRenderer>().color = new Color( 0f, 0f, 0f, i/255f);
            yield return new WaitForSeconds(0.005f);
        }
        this.GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f, 1);
        dark = true;
    }

    public IEnumerator transitionIn()
    {
        dark = false;
        yield return new WaitForSeconds(0.05f);
        for (float i = 255f; i > 0f; i -= 17f)
        {
            this.GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f, i/255f);
            yield return new WaitForSeconds(0.005f);
        }
        this.GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f, 0f);
    }

    
}
