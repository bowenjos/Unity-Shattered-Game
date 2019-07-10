using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YolandaSE : CharacterInteraction
{
    ViolinController anim;
    int typeConvo;
    int convoNumber;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<ViolinController>();

        if (!GameControl.control.masks[1])
        {
            Destroy(this.gameObject);
        }

        if (!GameControl.control.YolandaData.SEConversationHad[0])
        {
            typeConvo = 0;
            convoNumber = 0;
            dialogue = new string[1][];
            dialogue[0] = new string[1];
            dialogue[0][0] = "This is placeholder text, because I'm not sure what to say yet.";
        }
        else
        {
            typeConvo = 0;
            dialogue = new string[1][];
            dialogue[0] = new string[] { "Hello, Child." };
        }
    }

    // Update is called once per frame
    public override IEnumerator StartInteraction()
    {
        talkControl = GameObject.Find("Talk UI(Clone)").GetComponent<TalkController>();
        anim.paused = true;
        switch (typeConvo)
        {
            case 0:
                yield return StartCoroutine(talkControl.StartDialogueSprite(dialogue[0], "default", 2, 1));
                break;
        }
        anim.paused = false;
        GameControl.control.YolandaData.SEConversationHad[convoNumber] = true;
        dialogue = new string[1][];
        dialogue[0] = new string[] { "Hello, Child." };

    }
}
