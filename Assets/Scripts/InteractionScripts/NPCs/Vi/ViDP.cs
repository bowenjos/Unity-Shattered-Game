using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViDP : CharacterInteraction {

    public string saveLocation;
    public string[] saveDialogue;

    // Use this for initialization
    void Start () {
        if (GameControl.control.DPMainData.viTalked == true)
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
                    dialogue[0][0] = "I see you have found the theatre hall. Maybe there is a show going on soon.";
                    break;
                case 1:
                    dialogue = new string[1][];
                    dialogue[0] = new string[3];
                    dialogue[0][0] = "I see that you have met William.";
                    dialogue[0][1] = "Don't mind him, he's a really nice guy, he's just not good at meeting new people.";
                    dialogue[0][2] = "You might find him in the room behind the stage, he's usually working there.";
                    break;
                case 6:
                    dialogue = new string[1][];
                    dialogue[0] = new string[] { "Oh? The performance is soon? That's exciting, I'll be sure to be there." };
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
        switch (GameControl.control.DPMainData.progression)
        {
            case 0:
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
