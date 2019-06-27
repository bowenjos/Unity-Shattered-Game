using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WillSE : CharacterInteraction
{

    Animator anim;
    int typeConvo;
    int convoNumber;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        

        if (!GameControl.control.masks[0])
        {
            Destroy(this.gameObject);
        }

        if (!GameControl.control.WillData.SEConversationHad[0])
        {
            typeConvo = 0;
            convoNumber = 0;
            dialogue = new string[1][];
            dialogue[0] = new string[7];
            dialogue[0][0] = "I know I've already said it a thousand times already, but I wanted to thank you again.";
            dialogue[0][1] = "I feel...";
            dialogue[0][2] = "A little bit like I can breathe again.";
            dialogue[0][3] = "For the first time in a long time.";
            dialogue[0][4] = "Don't get me wrong. I'm still afraid of playing in front of people, but...";
            dialogue[0][5] = "Every breathe is a second chance so to speak.";
            dialogue[0][6] = "Even if that day isn't today, or even tomorrow, I will get there.";
        }
        else
        {
            typeConvo = 0;
            dialogue = new string[1][];
            dialogue[0] = new string[] { "Hey buddy, how is it going?" };
        }
    }

    public override IEnumerator StartInteraction()
    {
        talkControl = GameObject.Find("Talk UI(Clone)").GetComponent<TalkController>();
        anim.SetBool("Paused", true);
        switch (typeConvo)
        {
            case 0:
                yield return StartCoroutine(talkControl.StartDialogueSprite(dialogue[0], "default", 1, 0));
                break;
        }
        anim.SetBool("Paused", false);
        GameControl.control.WillData.SEConversationHad[convoNumber] = true;
        dialogue = new string[1][];
        dialogue[0] = new string[] { "Hey buddy, how is it going?" };

    }
}
