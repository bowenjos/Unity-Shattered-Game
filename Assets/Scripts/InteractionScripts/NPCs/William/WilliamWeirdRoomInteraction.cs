using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WilliamWeirdRoomInteraction : TriggerInteraction
{
    TalkController talkCanvas;
    JukeBoxController jukebox;
    public SpriteRenderer William;
    public Light thisLight;

    public string[][] dialogue;

    // Start is called before the first frame update
    void Start()
    {
        if(GameControl.control.lens[0])
        {
            Destroy(this);
        }

        jukebox = GameObject.Find("JukeBox(Clone)").GetComponent<JukeBoxController>();
        talkCanvas = GameObject.Find("Talk UI(Clone)").GetComponent<TalkController>();

        dialogue = new string[5][];
        dialogue[0] = new string[4];
        dialogue[0][0] = "Hmm...";
        dialogue[0][1] = "Well.";
        dialogue[0][2] = "This does seem to be quite the predicament, huh.";
        dialogue[0][3] = "I have something for you, for helping me out. ";
        dialogue[1] = new string[2];
        dialogue[1][0] = "You should be able to use that to leave here.";
        dialogue[1][1] = "I mean you're not going to get much done trapped in here...";
        dialogue[2] = new string[1];
        dialogue[2][0] = "Oh... and good luck.";
        dialogue[3] = new string[2];
        dialogue[3][0] = "Wait one last thing. Sorry for the comment about you not getting much done trapped down.";
        dialogue[3][1] = "It just. I don't know, felt rude maybe...";
        dialogue[4] = new string[1];
        dialogue[4][0] = "Okay I'm leaving now byeeeee.......";
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override IEnumerator StartInteraction()
    {
        Debug.Log("Hello");
        GameControl.control.Freeze();
        yield return StartCoroutine(jukebox.FadeOut(0.6f));
        jukebox.StopSong();
        yield return new WaitForSeconds(0.5f);
        jukebox.PlaySong("WillsTheme");
        yield return StartCoroutine(jukebox.FadeIn(0.4f));
        yield return StartCoroutine(Visability(true));
        yield return StartCoroutine(talkCanvas.StartDialogueSprite(dialogue[0], "default", 1, 0));
        GameControl.control.Freeze();
        yield return StartCoroutine(talkCanvas.StartDialogueSolo(new string[] { "You received the Blue Lens" }));
        GameControl.control.lens[0] = true;
        GameControl.control.Freeze();
        yield return StartCoroutine(talkCanvas.StartDialogueSprite(dialogue[1], "default", 1, 0));
        GameControl.control.Freeze();
        yield return StartCoroutine(Visability(false));
        yield return new WaitForSeconds(.5f);
        yield return StartCoroutine(Visability(true));
        yield return StartCoroutine(talkCanvas.StartDialogueSprite(dialogue[2], "default", 1, 0));
        GameControl.control.Freeze();
        yield return StartCoroutine(Visability(false));
        yield return new WaitForSeconds(.5f);
        yield return StartCoroutine(Visability(true));
        yield return StartCoroutine(talkCanvas.StartDialogueSprite(dialogue[3], "default", 1, 0));
        GameControl.control.Freeze();
        StartCoroutine(Visability(false));
        yield return StartCoroutine(talkCanvas.StartDialogueSprite(dialogue[4], "default", 1, 0));

        jukebox.PlaySong("DP");
        Destroy(this);
        yield return null;
    }

    IEnumerator Visability(bool appearing)
    {
        float dissappear = 1f;
        thisLight.intensity = 0.8f;
        if (!appearing)
        {
            while (dissappear > 0)
            {
                dissappear -= 0.1f;
                thisLight.intensity -= 0.08f;
                William.color = new Color(1f, 1f, 1f, dissappear);
                yield return new WaitForSeconds(0.1f);
            }
        }
        else
        {
            dissappear = 0f;
            while (dissappear < 1f)
            {
                dissappear += .1f;
                thisLight.intensity += 0.08f;
                William.color = new Color(1f, 1f, 1f, dissappear);
                yield return new WaitForSeconds(0.1f);
            }
        }


    }

}
