using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DPIntermissionBartender : ChoiceInteraction
{

    string[] leftDialogueTwo;
    string[] leftDialogueThree;

    string[] rightDialogueTwo;
    string[] rightDialogueThree;

    // Start is called before the first frame update
    void Start()
    {
        talkController = GameObject.Find("Talk UI(Clone)").GetComponent<TalkController>();

        dialogue = new string[4];
        dialogue[0] = "Oi' Lad.";
        dialogue[1] = "Ain't ya a wee bit small to be 'ere?";
        dialogue[2] = "Well I ain't one te judge.";
        dialogue[3] = "What'll ye 'ave?";

        choiceDialogue = "Menu";
        leftButtonText = "Lemonade";
        rightButtonText = "Water";

        leftDialogue = new string[2];
        leftDialogue[0] = "Aye.";
        leftDialogue[1] = "Well, don't ye tell ya Ma I gave ye this.";

        leftDialogueTwo = new string[2];
        leftDialogueTwo[0] = "You take a sip of the 'Lemonade' but it tastes more like a lemon lime soda than a lemonade.";
        leftDialogueTwo[1] = "You give the bartender a confused look.";

        leftDialogueThree = new string[5];
        leftDialogueThree[0] = "Oi', what's that look for then, aye?";
        leftDialogueThree[1] = "Ye asked for a lemonade didn't ye?";
        leftDialogueThree[2] = "Oh I get it, ye're one o' them types what thinks lemonade come from lemons.";
        leftDialogueThree[3] = "My mistake.";
        leftDialogueThree[4] = "Now ya Ma might really bite me 'ead off.";

        rightDialogue = new string[2];
        rightDialogue[0] = "Aye, tis what I like te see.";
        rightDialogue[1] = "Wee lad who 'preciates some good n' fresh water.";

        rightDialogueTwo = new string[2];
        rightDialogueTwo[0] = "The bartender hands you the water, and you drink it.";
        rightDialogueTwo[1] = "Or at least you try, as the water tastes of nothing and passes right through you.";

        rightDialogueThree = new string[5];
        rightDialogueThree[0] = "Aye, sorry.";
        rightDialogueThree[1] = "We've only got ghost water 'ere.";
        rightDialogueThree[2] = "For ghosts.";
        rightDialogueThree[3] = "I thought ye might be able ta drink it, but 't seems not.";
        rightDialogueThree[4] = "My bad.";


    }

    // Update is called once per frame


    public override IEnumerator StartInteraction()
    {

        yield return StartCoroutine(talkController.StartDialogueSprite(dialogue, "default", 3, 10));
        yield return StartCoroutine(talkController.StartDialogueChoice(choiceDialogue, leftButtonText, rightButtonText));
        if (talkController.result)
        {
            yield return StartCoroutine(talkController.StartDialogueSprite(leftDialogue, "default", 3, 10));
            yield return StartCoroutine(talkController.StartDialogueSolo(leftDialogueTwo));
            yield return StartCoroutine(talkController.StartDialogueSprite(leftDialogueThree, "default", 3, 10));

        }
        else
        {
            yield return StartCoroutine(talkController.StartDialogueSprite(rightDialogue, "default", 3, 10));
            yield return StartCoroutine(talkController.StartDialogueSolo(rightDialogueTwo));
            yield return StartCoroutine(talkController.StartDialogueSprite(rightDialogueThree, "default", 3, 10));
        }

    }
}
