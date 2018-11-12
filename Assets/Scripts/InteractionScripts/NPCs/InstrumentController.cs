using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstrumentController : MonoBehaviour {

    bool changed;
    Animator anim;
    Light thisLight;

    public Texture Cookie0;
    public Texture Cookie1;

	// Use this for initialization
	void Start () {
        if(GameControl.control.DPMainData.progression != 0)
        {
            Destroy(this.gameObject);
        }

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
        rand = Random.Range(0, 8);
        if(rand == 0)
        {
            anim.SetBool("Turned", false);
            thisLight.cookie = Cookie0;
        }
        else
        {
            anim.SetBool("Turned", true);
            thisLight.cookie = Cookie1;
        }
        float rand2 = Random.Range(3f, 7f);
        yield return new WaitForSeconds(rand2);
        changed = false;
    }
}
