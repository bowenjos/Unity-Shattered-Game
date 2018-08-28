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

    protected bool[] buttonActive = new bool[4];
    protected string[] buttonDestination = new string[4];

	// Use this for initialization
	void Start () {
        currentZone = EI.currentZoneValue;
        //GameControl.control.ElevatorData.unlockedZones[currentZone] = true;
        for(int i = 0; i < 9; i++)
        {
            Debug.Log(GameControl.control.ElevatorData.unlockedZones[i]);
        }
        Debug.Log(currentZone);
        UpdateButtons();
        elevatorPanel.SetActive(false);
	}
	
	// Update is called once per frame

    public IEnumerator StartElevator(string curLoc)
    {
        buttonPressed = false;
        GameControl.control.Freeze();

        UpdateButtons();
        
        buttons[0].Select();

        yield return WaitForButtonPress();
        yield return new WaitForSeconds(0.05f);


        elevatorPanel.SetActive(false);
        GameControl.control.Unfreeze();
        yield return null;
    }

    public void UpdateButtons()
    {
        elevatorPanel.SetActive(true);
        switch (currentZone)
        {
            case 0:
                zoneText.text = "Silent Entryhall";
                break;
            case 1:
                zoneText.text = "Dead Performance";
                break;
            case 2:
                zoneText.text = "Forgotten Mezzanine";
                break;
            case 3:
                zoneText.text = "Frigid Loft";
                break;
            case 4:
                zoneText.text = "Natural Banquet";
                break;
            case 5:
                zoneText.text = "Festered Kiln";
                break;
            case 6:
                zoneText.text = "Departure Sandbox";
                break;
            case 7:
                zoneText.text = "Cavernous Decline";
                break;
            case 8:
                zoneText.text = "Desolate Heart";
                break;
        }
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
        for(int i = currentZone; i <= 8; i++)
        {
            if(GameControl.control.ElevatorData.unlockedZones[i])
            {
                currentZone = i;
                UpdateButtons();
                return;
            }
        }
        UpdateButtons();
    }

    public void OnPrevButtonPress()
    {
        for(int i = currentZone; i >= 0; i--) { 
            if(GameControl.control.ElevatorData.unlockedZones[i])
            {
                currentZone = i;
                UpdateButtons();
                return;
            }
        } 
        UpdateButtons();
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
