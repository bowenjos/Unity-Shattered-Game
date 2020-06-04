using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*************
Class Name: DialogueInteraction
Purpose: Makes objects interactable by the player. Displays dialogue to the screen
*************/
public class DialogueInteraction : InteractionController {

    public GameObject talkCanvas;

    public string[] dialogue;

    // Use this for initialization
    void Start () {
        
        //Find the prefab initialized versions of the player, talk ui, and text from the talk ui.
        talkCanvas = GameObject.Find("Talk UI(Clone)");
    }


    //Dialogue Functions


    /*******************
    Function Name: Dialogue Start
    Function Type: IEnumerator
    Purpose: Iterates over the dialogue set for the object and prints it to to the screen via the talk ui
    Pre: Dialogue has been set for the object inside of the Unity interfact for that game object
    Post: Dialogue finishes, the object can be interacted with again for the same dialogue
    *******************/
    public override IEnumerator StartInteraction()
    {
        yield return StartCoroutine(talkCanvas.GetComponent<TalkController>().StartDialogueSolo(dialogue));
    }
    
}
