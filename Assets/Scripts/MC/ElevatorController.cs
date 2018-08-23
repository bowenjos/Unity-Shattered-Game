using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElevatorController : MonoBehaviour {

    public GameObject elevatorPanel;

	// Use this for initialization
	void Start () {
        elevatorPanel.SetActive(false);
	}
	
	// Update is called once per frame

    public IEnumerator StartElevator(string curLoc)
    {
        GameControl.control.Freeze();
        elevatorPanel.SetActive(true);

        yield return new WaitForSeconds(3f);

        elevatorPanel.SetActive(false);
        GameControl.control.Unfreeze();
        yield return null;
    }
}
