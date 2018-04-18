using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**************
Class Name: LightFlicker
Purpose: adds a simple flickering affect to a light attached to an object by changing the size of it's cookie
**************/
public class LightFlicker : MonoBehaviour {

    bool flickering;
    Light thisLight;
    float flickerSize;

    public float modifier = 1;

	// Use this for initialization
	void Start () {
        thisLight = this.GetComponent<Light>();
        flickerSize = thisLight.cookieSize;
	}
	
	// Update is called once per frame
	void Update () {
        //Checks every frame if the flickering function is already running, if it isn't, it starts it.
		if(flickering == false)
        {
            StartCoroutine("flicker");
        }
	}

    /*****
    Function Name: Flicker
    Function Type: IEnumerator
    Purpose: Makes a light cookie flicker by changing it's size back and forth
    Pre: Gameobject has this script and a child light object
    Post: None
    *****/
    IEnumerator flicker()
    {
        //sets the flickering value to true to stop the function from competeing with itself
        flickering = true;
        //shrinks the cookie size for a moment, then shrinks again, then increases back to normal.
        if (!GameControl.control.paused)
        {
            thisLight.cookieSize = flickerSize;
        }
        yield return new WaitForSeconds(9f / 60f);
        if (!GameControl.control.paused)
        { 
            thisLight.cookieSize = flickerSize - (0.1f * modifier);
        }
        yield return new WaitForSeconds(9f/60f);
        if (!GameControl.control.paused)
        {
            thisLight.cookieSize = flickerSize - (0.2f * modifier);
        }
        yield return new WaitForSeconds(9f/60f);
        if (!GameControl.control.paused)
        {
            thisLight.cookieSize = flickerSize - (0.1f * modifier);
        }
        yield return new WaitForSeconds(9f/60f);
        //Sets the flickering value to false so it can run again.
        flickering = false;
    }
}
