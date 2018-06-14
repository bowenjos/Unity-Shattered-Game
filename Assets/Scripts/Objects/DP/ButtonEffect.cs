using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonEffect : MonoBehaviour {

    public bool activated = false;

    public Sprite ButtonUp;
    public Sprite ButtonDown;

    SpriteRenderer sr;
    AudioSource audios;

	// Use this for initialization
	void Start () {
        sr = GetComponent<SpriteRenderer>();
        audios = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        sr.sprite = ButtonDown;
        audios.Play();
        activated = true;
    }

    void OnTriggerStay2D(Collider2D col)
    {
        sr.sprite = ButtonDown;
        activated = true;
    }

    void OnTriggerExit2D(Collider2D col)
    {
        sr.sprite = ButtonUp;
        activated = false;
    }
}
