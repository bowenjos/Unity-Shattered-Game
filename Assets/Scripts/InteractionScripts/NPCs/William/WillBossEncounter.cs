using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WillBossEncounter : ChoiceInteraction
{
    public WillBossData WillBoss;

    // Start is called before the first frame update
    void Start()
    {
        talkCanvas = GameObject.Find("Talk UI(Clone)");
        talkController = talkCanvas.GetComponent<TalkController>();

        dialogue = new string[1];
        dialogue[0] = "If you take your seat the show will begin.";

        choiceDialogue = "Take your seat?";
        leftButtonText = "Yes";
        rightButtonText = "No";

        leftDialogue = new string[1];
        leftDialogue[0] = "Let the show begin.";

        rightDialogue = new string[2];
        rightDialogue[0] = "Take your time.";
        rightDialogue[1] = "The show will not start without you.";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override IEnumerator StartInteraction()
    {
        yield return StartCoroutine(talkController.StartDialogueSolo(dialogue));
        yield return StartCoroutine(talkController.StartDialogueChoice(choiceDialogue, leftButtonText, rightButtonText));
        if (talkController.result)
        {
            yield return StartCoroutine(talkController.StartDialogueSolo(leftDialogue));

            StartCoroutine(WillBoss.BattleStart());
        }
        else
        {
            yield return StartCoroutine(talkController.StartDialogueSolo(rightDialogue));
        }
    }
}
