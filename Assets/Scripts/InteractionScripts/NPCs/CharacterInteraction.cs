using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInteraction : InteractionController {

    public TalkController talkControl;

    protected string[][] dialogue;

    // Use this for initialization
    void Start()
    {
        talkControl = GameObject.Find("Talk UI(Clone)").GetComponent<TalkController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override IEnumerator StartInteraction()
    {
        yield return null;
        //yield return StartCoroutine(talkCanvas.GetComponent<TalkController>().StartDialogueSolo(dialogue));
    }
}
