using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInteraction : InteractionController {

    public TalkController talkControl;

    public int character;
    public int mood;
    protected string[][] dialogue;
    public string[] smallDialogue;

    // Use this for initialization
    void Start()
    {
        talkControl = GameObject.Find("Talk UI(Clone)").GetComponent<TalkController>();
    }

    public override IEnumerator StartInteraction()
    {

        yield return StartCoroutine(talkControl.GetComponent<TalkController>().StartDialogueSprite(smallDialogue, "generic", character, mood));
    }
}
