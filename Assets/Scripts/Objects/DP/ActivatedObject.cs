using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatedObject : MonoBehaviour {

    public ButtonEffect button;

    public Sprite enabledSprite;
    public Sprite middleSprite;
    public Sprite disabledSprite;

    SpriteRenderer sr;
    BoxCollider2D bx;

    bool activatedHere;

	// Use this for initialization
	void Start () {
        sr = GetComponent<SpriteRenderer>();
        bx = GetComponent<BoxCollider2D>();
        activatedHere = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(button.activated == true && activatedHere != button.activated)
        {
            StartCoroutine(Activate());
        }
        else if(button.activated == false && activatedHere != button.activated)
        {
            StartCoroutine(Deactivate());
        }
	}

    IEnumerator Activate()
    {
        activatedHere = true;
        sr.sprite = middleSprite;
        yield return new WaitForSeconds(0.05f);
        sr.sprite = enabledSprite;
        bx.enabled = false;
        activatedHere = true;
    }

    IEnumerator Deactivate()
    {
        activatedHere = false;
        sr.sprite = middleSprite;
        yield return new WaitForSeconds(0.05f);
        sr.sprite = disabledSprite;
        bx.enabled = true;
    }
}
