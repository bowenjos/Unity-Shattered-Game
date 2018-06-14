using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatedObject : MonoBehaviour {

    public ButtonEffect button;

    public Sprite enabledSprite;
    public Sprite disabledSprite;

    SpriteRenderer sr;
    BoxCollider2D bx;

	// Use this for initialization
	void Start () {
        sr = GetComponent<SpriteRenderer>();
        bx = GetComponent<BoxCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {
		if(button.activated == true)
        {
            sr.sprite = enabledSprite;
            bx.enabled = false;
        }
        else
        {
            sr.sprite = disabledSprite;
            bx.enabled = true;
        }
	}
}
