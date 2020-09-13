using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YolandaFirstInteraction : TriggerInteraction
{
    TalkController talkCanvas;
    JukeBoxController jukebox;
    public string[][] dialogue;

    public ViolinController vc;

    // Start is called before the first frame update
    void Start()
    {
        if (GameControl.control.FMMainData.progression != 0)
        {
            Destroy(this.gameObject);
        }
        jukebox = GameObject.Find("JukeBox(Clone)").GetComponent<JukeBoxController>();
        talkCanvas = GameObject.Find("Talk UI(Clone)").GetComponent<TalkController>();

        dialogue = new string[1][];
        dialogue[0] = new string[24];
        dialogue[0][0] = "Did my playing bring you here, child?";
        dialogue[0][1] = "I get so lost in my head when I play, I fail to remember there are others around.";
        dialogue[0][2] = "Can I do anything for you?";
        dialogue[0][3] = "Thinking about it... What are you doing here, child?";
        dialogue[0][4] = "It's much past your bedtime and this place...";
        dialogue[0][5] = "It isn't safe for you. You shouldn't be here.";
        dialogue[0][6] = "...";
        dialogue[0][7] = "I can tell from the look on your face that you wouldn't be here if you had a choice.";
        dialogue[0][8] = "When you get as old a mother as me, you can tell these things hoo hoo.";
        dialogue[0][9] = "...";
        dialogue[0][10] = "I shouldn't ask this of you.";
        dialogue[0][11] = "Certainly not when I have already spoke of how unsafe you are here, and yet...";
        dialogue[0][12] = "There is a place in this house I cannot go.";
        dialogue[0][13] = "Or at least... I wish not to go to that place anymore.";
        dialogue[0][14] = "And yet...";
        dialogue[0][15] = "Please.";
        dialogue[0][16] = "Help this poor old woman out.";
        dialogue[0][17] = "Take these flowers.";
        dialogue[0][18] = "I would ask that you please lay them down at the shrine of an old friend.";
        dialogue[0][19] = "You will know it when you see it.";
        dialogue[0][20] = "Oh, fool that I am.";
        dialogue[0][21] = "Please take this key, it'll open the door out to the mezzanine.";
        dialogue[0][22] = "The shrine is that way.";
        dialogue[0][23] = "You would be doing this old fool a favor she cannot hope to repay.";

    }

    // Update is called once per frame
    public override IEnumerator StartInteraction()
    {
        float time;

        GameControl.control.Freeze();
        yield return new WaitForSeconds(1.0f);
        yield return StartCoroutine(jukebox.PauseOut(0.4f));
        time = jukebox.CurrentTime();
        jukebox.StopSong();
        vc.paused = true;

        yield return StartCoroutine(talkCanvas.StartDialogueSprite(dialogue[0], "default", 2, 0));

        jukebox.PlaySongPartway("FM", time);
        StartCoroutine(jukebox.FadeIn(0.4f));

        vc.paused = false;
        GameControl.control.FMMainData.key = true;
        GameControl.control.keys[1] = true;
        GameControl.control.DPMainData.progression = 1;
        GameControl.control.DPMainData.viTalked = false;
        Destroy(this.gameObject);
    }

}
