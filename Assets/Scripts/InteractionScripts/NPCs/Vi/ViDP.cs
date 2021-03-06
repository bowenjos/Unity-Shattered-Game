﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViDP : CharacterInteraction {

    public string saveLocation;
    public string[] saveDialogue;

    // Use this for initialization
    void Start () {
        Debug.Log(GameControl.control.DPMainData.progression);
        if (GameControl.control.DPMainData.viTalked)
        {
            DestroyThis();
        }
        else
        {
            switch (GameControl.control.DPMainData.progression)
            {
                case 0:
                    dialogue = new string[1][];
                    dialogue[0] = new string[1];
                    //dialogue[0][0] = "<color=red>this</color> <color=orange>is</color> <color=yellow>a</color> <color=lime>test</color> <color=blue>line</color>. <color=purple>purple</color> <color=#ff80ffff>pink</color>.";
                    dialogue[0][0] = "I see you have found the theatre hall. Maybe there is a show going on soon.";
                    break;
                case 1:
                    dialogue = new string[1][];
                    dialogue[0] = new string[3];
                    dialogue[0][0] = "I see that you have met <color=blue>William</color>.";
                    dialogue[0][1] = "Don't mind him, he's a really nice guy, he's just not good at meeting new people.";
                    dialogue[0][2] = "You might find him in the room behind the stage, he's usually working there.";
                    break;
                case 7:
                    dialogue = new string[1][];
                    dialogue[0] = new string[] { "The crew is missing? My, that is concerning." };
                    break;
                case 8:
                    dialogue = new string[3][];
                    dialogue[0] = new string[] { "Huh..." };
                    dialogue[1] = new string[] { "Oh it's nothing." };
                    dialogue[2] = new string[] { "I've just never seen William open up to anyone so quickly before." };
                    break;
                default:
                    DestroyThis();
                    break;
            }
        }
    }

    // Update is called once per frame
    public override IEnumerator StartInteraction()
    {
        talkControl = GameObject.Find("Talk UI(Clone)").GetComponent<TalkController>();
        GameControl.control.Freeze();
        switch (GameControl.control.DPMainData.progression)
        {
            case 0:
                yield return StartCoroutine(talkControl.StartDialogueSprite(dialogue[0], "vi", 0, 0));
                DestroyThis();
                break;
            case 1:
                yield return StartCoroutine(talkControl.StartDialogueSprite(dialogue[0], "vi", 0, 0));
                DestroyThis();
                break;
            case 5:
                yield return StartCoroutine(talkControl.StartDialogueSprite(dialogue[0], "vi", 0, 0));
                DestroyThis();
                break;
            default:
                DestroyThis();
                break;
        }
    }

    private void DestroyThis()
    {
        this.transform.gameObject.AddComponent<SaveInteract>();
        this.transform.gameObject.GetComponent<SaveInteract>().saveDialogue = saveDialogue;
        this.transform.gameObject.GetComponent<SaveInteract>().location = saveLocation;
        GameControl.control.DPMainData.viTalked = true;
        Destroy(this);
    }
}
