using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceInteraction : DialogueInteraction
{
    public TalkController talkController;

    public string choiceDialogue;
    public string[] leftDialogue;
    public string[] rightDialogue;

    public string leftButtonText;
    public string rightButtonText;

    // Start is called before the first frame update
    void Start()
    {
        talkCanvas = GameObject.Find("Talk UI(Clone)");
        talkController = talkCanvas.GetComponent<TalkController>();
    }

    // Update is called once per frame
    public override IEnumerator StartInteraction()
    {
        yield return StartCoroutine(talkController.StartDialogueSolo(dialogue));
        yield return StartCoroutine(talkController.StartDialogueChoice(choiceDialogue, leftButtonText, rightButtonText));
        if (talkController.result)
        {
            yield return StartCoroutine(talkController.StartDialogueSolo(leftDialogue));
        }
        else
        {
            yield return StartCoroutine(talkController.StartDialogueSolo(rightDialogue));
        }
    }
}
