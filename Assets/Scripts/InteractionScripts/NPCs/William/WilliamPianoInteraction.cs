using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WilliamPianoInteraction : CharacterInteraction
{
    TalkController talkCanvas;
    public SpriteRenderer William;

    public Sprite forward;
    public Sprite backward;

    protected bool talked;

    // Start is called before the first frame update
    void Start()
    {
        if(GameControl.control.DPMainData.progression > 8)
        {
            Destroy(this.gameObject);
        }

        talked = false;
        talkCanvas = GameObject.Find("Talk UI(Clone)").GetComponent<TalkController>();

        dialogue = new string[3][];
        dialogue[0] = new string[2];
        dialogue[0][0] = "...";
        dialogue[0][1] = "Hmm...";
        dialogue[1] = new string[3];
        dialogue[1][0] = "Oh sorry.";
        dialogue[1][1] = "I didn't see you there.";
        dialogue[1][2] = "I was just thinking about a friend...";
        dialogue[2] = new string[1];
        dialogue[2][0] = "I wonder where he is...";
    }


    public override IEnumerator StartInteraction()
    {
        if (!talked)
        {
            GameControl.control.Freeze();
            yield return StartCoroutine(talkCanvas.StartDialogueSprite(dialogue[0], "default", 1, 0));
            GameControl.control.Freeze();
            William.sprite = forward;
            yield return StartCoroutine(talkCanvas.StartDialogueSprite(dialogue[1], "default", 1, 0));
            William.sprite = backward;
        }
        if (talked)
        {
            GameControl.control.Freeze();
            yield return StartCoroutine(talkCanvas.StartDialogueSprite(dialogue[2], "default", 1, 0));
        }
        talked = true;
        yield return null;

    }
}
