using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestInteraction : DialogueInteraction
{
    // Start is called before the first frame update
    public int chestNumber;
    public Sprite chestOpen;

    void Awake()
    {
        if(GameControl.control.mirrorPolish[chestNumber])
        {
            this.GetComponent<SpriteRenderer>().sprite = chestOpen;
            Destroy(this);
        }

        dialogue = new string[2];
        dialogue[0] = "You found some Mirror Polish!";
        dialogue[1] = "Your Max Health has gone up by 10.";
    }

    public override IEnumerator StartInteraction()
    {
        this.GetComponent<SpriteRenderer>().sprite = chestOpen;
        this.GetComponent<AudioSource>().Play();
        GameControl.control.maxHealth += 10;
        GameControl.control.mirrorPolish[chestNumber] = true;
        yield return StartCoroutine(talkCanvas.GetComponent<TalkController>().StartDialogueSolo(dialogue));
        Destroy(this);
    }
}
