using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorInteraction : CharacterInteraction
{

    public ElevatorExitInteraction ElevatorDoor;
    public ElevatorController elevControl;
    public Shake elevator;
    public string[][] travelDialogue;
    public string[] stayDialogue;
    public string[] errorDialogue;
    public string currentZone;
    public int currentZoneValue;
    public string currentLocation;
    public string targetLocation;

    public bool selectionMade = false;

    protected GameObject ElevDataTrans;
    

    // Use this for initialization
    void Awake()
    {
        ElevDataTrans = GameObject.Find("ElevatorDataTransfer");
        currentZone = ElevDataTrans.GetComponent<ElevatorTransferData>().currentZone;
        currentLocation = ElevDataTrans.GetComponent<ElevatorTransferData>().currentRoom;
        currentZoneValue = ElevDataTrans.GetComponent<ElevatorTransferData>().zoneValue;
        ElevatorDoor.targetSceneName = currentLocation;
        ElevatorDoor.zone = currentZone;
        Destroy(ElevDataTrans);
        talkControl = GameObject.Find("Talk UI(Clone)").GetComponent<TalkController>();
        travelDialogue = new string[3][];
        travelDialogue[0] = new string[] { "So, where to kid?" };
        travelDialogue[1] = new string[] { "You got it Boss." };
        travelDialogue[2] = new string[] { "Here we are." };
    }

    public override IEnumerator StartInteraction()
    {
        selectionMade = false;
        yield return StartCoroutine(talkControl.StartDialogueSprite(travelDialogue[0], "default", 0, 0));
        yield return StartCoroutine(elevControl.StartElevator(currentLocation));
        if (selectionMade == false)
        {
            yield return StartCoroutine(talkControl.StartDialogueSprite(stayDialogue, "default", 0, 0));
        }
        else {
            yield return StartCoroutine(talkControl.StartDialogueSprite(travelDialogue[1], "default", 0, 0));
            elevator.StartShake(1f);
            yield return new WaitForSeconds(1f);
            yield return StartCoroutine(talkControl.StartDialogueSprite(travelDialogue[2], "default", 0, 0));
        }
        selectionMade = false;
    }

}
