using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WilliamMiniBossInteraction : TriggerInteraction
{
    TalkController talkCanvas;
    JukeBoxController jukebox;

    public Light thisLight;
    public Texture cookie;
    public Texture getUpCookie;
    public InstrumentController wgc;
    public SpriteRenderer William;

    public string[][] dialogue;

    // Start is called before the first frame update
    void Start()
    {
        if (GameControl.control.DPMainData.progression != 9)
        {
            Destroy(this.gameObject);
        }
        talkCanvas = GameObject.Find("Talk UI(Clone)").GetComponent<TalkController>();
        jukebox = GameObject.Find("JukeBox(Clone)").GetComponent<JukeBoxController>();

        dialogue = new string[2][];
        dialogue[0] = new string[3];
        dialogue[0][0] = "Oh hey Will, my man.";
        dialogue[0][1] = "We were just getting some last minute reps in before the big show.";
        dialogue[0][2] = "You gotta stay in shape if you want to be able to do long performances.";

        dialogue[1] = new string[4];
        dialogue[1][0] = "Oh... No, I was just-";
        dialogue[1][1] = "The shows starting soon, so I wanted to-";
        dialogue[1][2] = "Make sure everything was in order.";
        dialogue[1][3] = "For the show...";
        dialogue[1][4] = "Cause it's starting... soon.";

    }

    public override IEnumerator StartInteraction()
    {
        GameControl.control.Freeze();
        //yield return StartCoroutine(talkCanvas.StartDialogueSprite(dialogue[0], "default", 5, 10));
        yield return null;
    }
}
