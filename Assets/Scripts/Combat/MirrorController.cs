﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MirrorController : MonoBehaviour {

    public float damageValue;

    public GameObject bf;

    protected ContactFilter2D contactFilter;
    protected List<RaycastHit2D> hitBufferList = new List<RaycastHit2D>(16);
    protected Collider2D mirrorCollider;

    public Sprite mirror1;
    public Sprite mirror2;
    public Sprite mirror3;
    public Sprite mirror4;
    public Sprite mirror5;
    

    // Use this for initialization
    void Start () {
        //contactFilter.useTriggers = false;
        //contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
    }
	
	// Update is called once per frame
	void Update () {

        if(GameControl.control.health >= GameControl.control.maxHealth * 0.8f)
        {
            this.GetComponent<SpriteRenderer>().sprite = mirror1;
        }
        else if(GameControl.control.health >= GameControl.control.maxHealth * 0.6f)
        {
            this.GetComponent<SpriteRenderer>().sprite = mirror2;
        }
        else if (GameControl.control.health >= GameControl.control.maxHealth * 0.4f)
        {
            this.GetComponent<SpriteRenderer>().sprite = mirror3;
        }
        else if (GameControl.control.health >= GameControl.control.maxHealth * 0.2f)
        {
            this.GetComponent<SpriteRenderer>().sprite = mirror4;
        }
        else if (GameControl.control.health >= 0f)
        {
            this.GetComponent<SpriteRenderer>().sprite = mirror5;
        }
        if (GameControl.control.health <= 0)
        {
            //Play Shattered Effect
            StopAllCoroutines();
            SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
            Debug.Log("You have Shattered");
        }
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("Something entered");
        GameControl.control.health -= col.gameObject.GetComponent<DefaultAttack>().damageValue;
        this.GetComponent<Shake>().StartShake(.05f);
        Destroy(col.gameObject);
    }

    
}
