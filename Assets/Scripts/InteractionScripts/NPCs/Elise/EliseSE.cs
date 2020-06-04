using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliseSE : CharacterInteraction
{
    Animator anim;

    //used to set how the dialogue works
    int typeConvo;
    //used to determine which conversation is happening
    int convoNumber;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        if (!GameControl.control.masks[2])
        {
            Destroy(this.gameObject);
        }

        if (!GameControl.control.EliseData.SEConversationHad[0])
        {
            typeConvo = 0;
            convoNumber = 0;
            dialogue = new string[1][];
            dialogue[0] = new string[6];
            dialogue[0][0] = "Thanks.";
            dialogue[0][1] = "...";
            dialogue[0][2] = "If you're expecting much more out of me, I'm sorry.";
            dialogue[0][3] = "I'm still figuring things out in my head.";
            dialogue[0][4] = "But you've been a good friend to me, and i won't soon forget that.";
            dialogue[0][5] = "Now get outta here before I have to give you a noogie for making me all sappy.";

        }
        else
        {
            typeConvo = 0;
            dialogue = new string[1][];
            dialogue[0] = new string[] { "Sup." };
        }
    }

    public override IEnumerator StartInteraction()
    {
        talkControl = GameObject.Find("Talk UI(Clone)").GetComponent<TalkController>();
        anim.SetBool("Paused", true);
        switch (typeConvo)
        {
            case 0:
                yield return StartCoroutine(talkControl.StartDialogueSprite(dialogue[0], "default", 3, 1));
                break;
        }
        anim.SetBool("Paused", false);
        GameControl.control.EliseData.SEConversationHad[convoNumber] = true;
        dialogue = new string[1][];
        dialogue[0] = new string[] { "Sup." };

    }
}
