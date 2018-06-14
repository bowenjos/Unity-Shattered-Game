using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOffSwitch : InteractionController {

    public int leverNumber;

    public Sprite SwitchOn;


	// Use this for initialization
	void Start () {
		if(GameControl.control.DPMainData.levers[leverNumber] == true)
        {
            GetComponent<SpriteRenderer>().sprite = SwitchOn;
            Destroy(this);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override IEnumerator StartInteraction()
    {
        GetComponent<SpriteRenderer>().sprite = SwitchOn;
        GameControl.control.DPMainData.levers[leverNumber] = true;
        GameControl.control.DPMainData.progression++;
        GameControl.control.DPMainData.williamTalked = false;
        GetComponent<AudioSource>().Play();
        Destroy(this);
        yield return null;
    }
}
