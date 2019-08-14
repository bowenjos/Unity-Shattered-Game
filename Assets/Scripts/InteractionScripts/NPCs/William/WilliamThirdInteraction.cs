using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WilliamThirdInteraction : CharacterInteraction
{
    TalkController talkCanvas;
    JukeBoxController jukebox;
    public SpriteRenderer William;
    public Light thisLight;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        jukebox = GameObject.Find("JukeBox(Clone)").GetComponent<JukeBoxController>();
        talkCanvas = GameObject.Find("Talk UI(Clone)").GetComponent<TalkController>();

        dialogue = new string[1][];
        dialogue[0] = new string[6];
        dialogue[0][0] = "Look... I. I really appreciate your help.";
        dialogue[0][1] = "I'm sorry again how I ran away when I first saw you.";
        dialogue[0][2] = "I'm supposed to be playing tonight,";
        dialogue[0][3] = "but it's the first time I'll have played for an Audience.";
        dialogue[0][4] = "Before this, Elise was the only person who has ever seen me play.";
        dialogue[0][5] = "Now, lets go find those actors.";
    }

    // Update is called once per frame
    public override IEnumerator StartInteraction()
    {
        float time;
        GameControl.control.Freeze();
        yield return StartCoroutine(jukebox.PauseOut(0.4f));
        time = jukebox.CurrentTime();
        jukebox.StopSong();
        anim.SetBool("Paused", true);
        jukebox.PlaySong("WillsTheme");
        yield return StartCoroutine(jukebox.FadeIn(0.4f));
        yield return StartCoroutine(talkCanvas.StartDialogueSprite(dialogue[0], "default", 1, 0));
        anim.SetBool("Paused", false);
        jukebox.PlaySongPartway("DP", time);
        GameControl.control.DPMainData.progression = 8;
        GameControl.control.DPMainData.viTalked = false;
    }
}
