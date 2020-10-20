using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstTutorialInteraction : ChoiceInteraction
{

    public EnemyCombatController tutorialBear;

    // Start is called before the first frame update
    void Start()
    {
        talkCanvas = GameObject.Find("Talk UI(Clone)");
        talkController = talkCanvas.GetComponent<TalkController>();

        dialogue = new string[3];
        dialogue[0] = "Ooooh, hello friend.";
        dialogue[1] = "It seems that you are new around here.";
        dialogue[2] = "If you'd like, I can show you how to resolve a conflict.";

        choiceDialogue = "Combat Tutorial";
        leftButtonText = "Yes";
        rightButtonText = "No";

        leftDialogue = new string[1];
        leftDialogue[0] = "Beary well. Let's tussle.";

        rightDialogue = new string[2];
        rightDialogue[0] = "Okay, friend.";
        rightDialogue[1] = "If you ever want to review the tutorial, I will be in the entryhall.";
    }

    // Update is called once per frame
    public override IEnumerator StartInteraction()
    {
        yield return StartCoroutine(talkController.StartDialogueSprite(dialogue, "default", 5, 10));
        yield return StartCoroutine(talkController.StartDialogueChoice(choiceDialogue, leftButtonText, rightButtonText));
        if (talkController.result)
        {
            yield return StartCoroutine(talkController.StartDialogueSprite(leftDialogue, "default", 5, 10));
            StartCoroutine(tutorialBear.GetComponent<BjorngelskogController>().BattleStart());
        }
        else
        {
            yield return StartCoroutine(talkController.StartDialogueSprite(rightDialogue, "default", 5, 10));
           
        }
    }
}
