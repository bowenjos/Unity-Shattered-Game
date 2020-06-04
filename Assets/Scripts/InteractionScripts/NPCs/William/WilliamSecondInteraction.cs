using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WilliamSecondInteraction : CharacterInteraction {

    // Use this for initialization
    JukeBoxController jukebox;

    void Start () {

        if (GameControl.control.DPMainData.progression < 1 || GameControl.control.DPMainData.progression > 5)
        {
            Destroy(this.gameObject);
        }
        else if (!GameControl.control.DPMainData.williamTalked)
        {

            switch (GameControl.control.DPMainData.progression)
            {
                case 1:
                    dialogue = new string[3][];
                    dialogue[0] = new string[] { "...", "...", "...", "Hello again.", "Don't mind me. I'm just, uh, setting up for the performance tonight.", "...",
                                    "Are you just going to keep standing there?", "Look, uh, there is something I need help with actually.", "There is a show going on tonight, but the spotlights haven't been turned on yet.", "I'd do it myself, but I need to set up some things back here for the show, and unfortunately I'm the only person who knows how.", "Oh, before I forget, take this key. You probably wouldn't be able to get very far without it..."};
                    dialogue[1] = new string[] { "It will let you into some of the storage rooms downstairs.", "There should be three big switches.",
                                               "They're around here somewhere... I don't know why they put them where they did.", "Yeah...", "...", "See ya later.", "Ack, actually, one last thing.", "They're not all in the basement? Only like two of them, the other one is somewhere else...", "But still like, in this sort of general area... The rafters I think?", "That's stupid to say, I shouldn't tell you to go into the rafters what am I thinking.", "Stupid, stupid.",};
                    dialogue[2] = new string[] { "...", "Hey, I'm really sorry. But also I really appreciate you helping.", "I'll be here if you need me." };
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
        jukebox = GameObject.Find("JukeBox(Clone)").GetComponent<JukeBoxController>();
        jukebox.PlaySong("WillsTheme");
        StartCoroutine(jukebox.FadeIn(0.4f));

        switch (GameControl.control.DPMainData.progression)
        {
            case 1:
                yield return StartCoroutine(talkControl.StartDialogueSprite(dialogue[0], "default", 1, 0));
                GameControl.control.DPMainData.key = true;
                Debug.Log("KEY DP");
                yield return StartCoroutine(talkControl.StartDialogueSolo(new string[] { "You received the Dead Performance Key" }));
                GameControl.control.DPMainData.key = true;
                yield return StartCoroutine(talkControl.StartDialogueSprite(dialogue[1], "default", 1, 0));
                GameControl.control.Freeze();
                yield return new WaitForSeconds(1f);
                yield return StartCoroutine(talkControl.StartDialogueSprite(dialogue[2], "default", 1, 0));
                GameControl.control.DPMainData.progression = 2;
                DestroyThis();
                break;
            case 5:
                yield return StartCoroutine(talkControl.StartDialogueSprite(dialogue[0], "default", 1, 0));
                GameControl.control.DPMainData.progression = 6;
                DestroyThis();
                break;
            default:
                yield return StartCoroutine(talkControl.StartDialogueSprite(dialogue[0], "default", 1, 0));
                DestroyThis();
                break;
        }
        GameControl.control.DPMainData.williamTalked = true;
        jukebox.SetVolume(0.6f);
        jukebox.PlaySong("DP");
    }

    void DestroyThis()
    {
        switch (GameControl.control.DPMainData.progression)
        {
            case 3:
                dialogue = new string[1][];
                dialogue[0] = new string[] { "Just two more and then we can get this show on the road.", "Theres one more in the basement and then the other was in the rafters, but don't worry about that one I'll get someone else to do it maybe..." };
                break;
            case 4:
                dialogue = new string[1][];
                if (!GameControl.control.DPMainData.levers[2])
                {
                    dialogue[0] = new string[] { "Not long now...", "Just the one in the rafters left...", "I hope someone else comes along I don't want you to go up there..." };
                }
                else
                {
                    dialogue[0] = new string[] { "Not long now...", "Did you get the last one in basement?" };
                }
                break;
            case 6:
                dialogue = new string[1][];
                dialogue[0] = new string[] { "I'm going to go check on the actors." };
                break;
            default:
                dialogue = new string[1][];
                dialogue[0] = new string[] { "If you could, uh, find the three big switches that control the spotlights, I'd, uh, really appreciate it.", "There should be one in the basement" };
                break;

        }
    }
}
