using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WilliamSecondInteraction : CharacterInteraction {

	// Use this for initialization
	void Start () {
        if (GameControl.control.DPMainData.progression < 1 || GameControl.control.DPMainData.progression > 5)
        {
            Destroy(this.gameObject);
        }
        else if (GameControl.control.DPMainData.williamTalked == false)
        {
            switch (GameControl.control.DPMainData.progression)
            {
                case 1:
                    dialogue = new string[2][];
                    dialogue[0] = new string[] { "...", "...", "...", "Hello again.", "Don't mind me. I'm just, uh, setting up for the performance tonight.", "...",
                                    "Are you just going to keep standing there?", "Look, uh, if you want to help take this."};
                    dialogue[1] = new string[] { "It'll let you push some of these boxes around.", "If you could, uh, find the three big switches that control the spotlights, I'd, uh, really appreciate it.",
                                               "I don't know why they put them where they did.", "I think you should probabaly do the one in the basement first, but it's up to you.", "Yeah...", "...", "See ya later." };
                    break;
                case 3:
                    dialogue = new string[1][];
                    dialogue[0] = new string[] { "Hey thanks, you got one of them.", "Just two more and then we can get this show on the road.", "...", "I'm not very good at this, I'm just gonna" };
                    break;
                case 4:
                    dialogue = new string[1][];
                    dialogue[0] = new string[] { "Hey good job, only one left.", "Only like... a little longer and then we can start the show... ha ha... ha.", "Not long now..." };
                    break;
                case 5:
                    dialogue = new string[1][];
                    dialogue[0] = new string[] { "You got them all... Hurray.", "The show can start soon...", "...", "I'm going to go check on the actors." };
                    break;
                default:
                    //DestroyThis();
                    break;
            }
        }
        else
        {
            DestroyThis();
        }
        
    }

    public override IEnumerator StartInteraction()
    {
        talkControl = GameObject.Find("Talk UI(Clone)").GetComponent<TalkController>();
        switch (GameControl.control.DPMainData.progression)
        {
            case 1:
                yield return StartCoroutine(talkControl.StartDialogueSprite(dialogue[0], "default", 0, 0));
                GameControl.control.lens[0] = true;
                Debug.Log("Blue Lens Received");
                yield return StartCoroutine(talkControl.StartDialogueSolo(new string[] { "You received the Blue Lens!" }));
                yield return StartCoroutine(talkControl.StartDialogueSprite(dialogue[1], "default", 0, 0));
                GameControl.control.DPMainData.progression = 2;
                DestroyThis();
                break;
            case 5:
                yield return StartCoroutine(talkControl.StartDialogueSprite(dialogue[0], "default", 0, 0));
                GameControl.control.DPMainData.progression = 6;
                DestroyThis();
                break;
            default:
                yield return StartCoroutine(talkControl.StartDialogueSprite(dialogue[0], "default", 0, 0));
                DestroyThis();
                break;
        }
        GameControl.control.DPMainData.williamTalked = true;
    }

    void DestroyThis()
    {
        switch (GameControl.control.DPMainData.progression)
        {
            case 4:
                dialogue = new string[1][];
                dialogue[0] = new string[] { "Just two more and then we can get this show on the road." };
                break;
            case 5:
                dialogue = new string[1][];
                dialogue[0] = new string[] { "Not long now..." };
                break;
            case 6:
                dialogue = new string[1][];
                dialogue[0] = new string[] { "I'm going to go check on the actors." };
                break;
            default:
                dialogue = new string[1][];
                dialogue[0] = new string[] { "If you could, uh, find the three big switches that control the spotlights, I'd, uh, really appreciate it." };
                break;

        }
    }
}
