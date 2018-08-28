using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElevatorController : MonoBehaviour {

    public ElevatorInteraction EI;
    public GameObject elevatorPanel;

    public ElevatorExitInteraction ElevatorDoor;

    public Button[] buttons;
    public Button exitButton;
    public Text zoneText;
    public string[] zones;

    protected bool buttonPressed;
    protected int currentZone;

    protected bool[] buttonActive = new bool[4];
    protected string[] buttonDestination = new string[4];

    protected string[] buttonText = new string[4];



	// Use this for initialization
	void Start () {
        currentZone = EI.currentZoneValue;
        //GameControl.control.ElevatorData.unlockedZones[currentZone] = true;
        UpdateButtons();
        elevatorPanel.SetActive(false);
	}
	
	// Update is called once per frame

    public IEnumerator StartElevator(string curLoc)
    {
        buttonPressed = false;
        GameControl.control.Freeze();

        UpdateButtons();
        
        exitButton.Select();

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

        for(int i = 0; i < 4; i++)
        {
            if (GameControl.control.ElevatorData.unlockedElevators[currentZone][i])
            {
                buttonText[i] = GameControl.control.ElevatorData.roomElevators[currentZone][i];
                buttons[i].GetComponentInChildren<Text>().text = GameControl.control.ElevatorData.nameElevators[currentZone][i];
                buttons[i].gameObject.SetActive(true);
            }
            else
            {
                buttons[i].GetComponentInChildren<Text>().text = "????";
                buttons[i].gameObject.SetActive(false);
            }
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
        for (int i = currentZone+1; i <= 8; i++)
        {
            if (GameControl.control.ElevatorData.unlockedZones[i])
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
        for(int i = currentZone-1; i >= 0; i--) {
            if (GameControl.control.ElevatorData.unlockedZones[i])
            {
                currentZone = i;
                UpdateButtons();
                return;
            }
        }
        UpdateButtons();
    }

    void SetZone()
    {
        switch (currentZone)
        {
            case 0:
                ElevatorDoor.zone = "SE";
                break;
            case 1:
                ElevatorDoor.zone = "DP";
                break;
            case 2:
                ElevatorDoor.zone = "FM";
                break;
            case 3:
                ElevatorDoor.zone = "FL";
                break;
            case 4:
                ElevatorDoor.zone = "NB";
                break;
            case 5:
                ElevatorDoor.zone = "FK";
                break;
            case 6:
                ElevatorDoor.zone = "DS";
                break;
            case 7:
                ElevatorDoor.zone = "CD";
                break;
            case 8:
                ElevatorDoor.zone = "DH";
                break;

        }
    }

    public void OnButtonZeroPress()
    {
        ElevatorDoor.targetSceneName = buttonText[0];
        SetZone();
        EI.selectionMade = true;
        buttonPressed = true;
    }

    public void OnButtonOnePress()
    {
        ElevatorDoor.targetSceneName = buttonText[1];
        SetZone();
        EI.selectionMade = true;
        buttonPressed = true;
    }

    public void OnButtonTwoPress()
    {
        ElevatorDoor.targetSceneName = buttonText[2];
        SetZone();
        EI.selectionMade = true;
        buttonPressed = true;
    }

    public void OnButtonThreePress()
    {
        ElevatorDoor.targetSceneName = buttonText[3];
        SetZone();
        EI.selectionMade = true;
        buttonPressed = true;
    }

}
