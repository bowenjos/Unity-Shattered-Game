using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WilliamThirdHalfInteraction : TriggerInteraction
{
    TalkController talkCanvas;
    public SpriteRenderer William;

    int count;
    string[] dialogue;
    public WilliamThirdInteraction wti;
    public PlayerController pc;
    public Transform pct;

    // Start is called before the first frame update
    void Start()
    {
        count = 0;
        talkCanvas = GameObject.Find("Talk UI(Clone)").GetComponent<TalkController>();
        pc = GameObject.Find("player(Clone)").GetComponent<PlayerController>();
        pct = pc.gameObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    public override IEnumerator StartInteraction()
    {
        dialogue = new string[1];
        if (count == 0)
        {
            dialogue[0] = "Hey, can we talk real quick?";
        }
        if (count == 1)
        {
            dialogue[0] = "You know, it would be easier if you just came over here...";
        }
        if (count == 2)
        {
            dialogue[0] = "...";
        }
        if (count == 3)
        {
            dialogue[0] = "Fine.";
        }

        GameControl.control.Freeze();
        yield return StartCoroutine(talkCanvas.StartDialogueSprite(dialogue, "default", 1, 0));
        if(count < 3)
        {
            pc.animator.SetInteger("walkDirection", 3);
            pct.position = new Vector3(pct.position.x - 0.02f, pct.position.y, pct.position.y);
        }
        if(count == 3)
        {
            yield return StartCoroutine(wti.StartInteraction());
            Destroy(this.gameObject);
        }
        count++;
        yield return null;

    }
}
