using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElevatorController : MonoBehaviour {

    public ElevatorInteraction EI;
    public GameObject elevatorPanel;

    public Button[] buttons;
    public Text zoneText;
    public string[] zones;

    protected bool buttonPressed;
    protected int currentZone;

	// Use this for initialization
	void Start () {
        elevatorPanel.SetActive(false);
        Debug.Log(buttons);
	}
	
	// Update is called once per frame

    public IEnumerator StartElevator(string curLoc)
    {
        buttonPressed = false;
        GameControl.control.Freeze();
        elevatorPanel.SetActive(true);
        buttons[0].Select();

        yield return WaitForButtonPress();
        yield return new WaitForSeconds(0.05f);


        elevatorPanel.SetActive(false);
        GameControl.control.Unfreeze();
        yield return null;
    }

    /*******************
   Function Name: WaitForKeyDown
   Function Type: IEnumerator
   Purpose: Waits for the player to press a given key before continuing
   Pre: Nothing specific, but usually a line of dialogue will be on the screen waiting to be continued
   Post: The line of dialogue will end and a new one might start (or not depending on if there is any left)
   *******************/
    protected IEnumerator WaitForButtonPress()
    {
        do
        {
            yield return null;
        } while (!buttonPressed);
    }

    public void OnExitButtonPress()
    {
        EI.selectionMade = false;
        buttonPressed = true;
    }

    public void OnNextButtonPress()
    {

    }

    public void OnPrevButtonPress()
    {

    }

    public void UpdateButtons()
    {

    }

    public void OnButtonZeroPress()
    {
        EI.selectionMade = true;
        buttonPressed = true;
    }

    public void OnButtonOnePress()
    {
        EI.selectionMade = true;
        buttonPressed = true;
    }

    public void OnButtonTwoPress()
    {
        EI.selectionMade = true;
        buttonPressed = true;
    }

    public void OnButtonThreePress()
    {
        EI.selectionMade = true;
        buttonPressed = true;
    }

}
