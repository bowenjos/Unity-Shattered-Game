using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultRoom : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    protected virtual void LoadRoom()
    {
        //Get this rooms data from the copy of this class in GameControl

    }

    protected virtual void SaveRoom()
    {
        //Store this room data in the GameControl
    }
}
