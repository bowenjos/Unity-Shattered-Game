using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Triggers when you use the notebook in the Blue Bedroom in Silent Entryhall

public class BBGhostInteraction : ChoiceInteraction
{

    public Sprite sit;
    public Sprite turn;
    public SpriteRenderer ghost;

    // Start is called before the first frame update
    void Start()
    {
        talkController = GameObject.Find("Talk UI(Clone)").GetComponent<TalkController>();
    }

    // Update is called once per frame
    public override IEnumerator StartInteraction()
    {
        yield return StartCoroutine(talkController.StartDialogueSolo(dialogue));
        yield return StartCoroutine(talkController.StartDialogueChoice(choiceDialogue, leftButtonText, rightButtonText));
        if (talkController.result)
        {
            ghost.sprite = turn;
            yield return StartCoroutine(talkController.StartDialogueSprite(leftDialogue, "default", 0, 9));
            ghost.sprite = sit;
        }
        else
        {
            yield return StartCoroutine(talkController.StartDialogueSolo(rightDialogue));
        }
    }
}
