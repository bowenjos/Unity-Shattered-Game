using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WilliamFourthInteractions : TriggerInteraction
{
    TalkController talkCanvas;
    JukeBoxController jukebox;
    public SpriteRenderer William;
    public SpriteRenderer otherWilliam;
    public Light thisLight;
    public Light otherLight;
    public Animator anim;

    string[][] dialogue;

    // Start is called before the first frame update
    void Start()
    {
        jukebox = GameObject.Find("JukeBox(Clone)").GetComponent<JukeBoxController>();
        talkCanvas = GameObject.Find("Talk UI(Clone)").GetComponent<TalkController>();

        dialogue = new string[1][];
        dialogue[0] = new string[13];
        dialogue[0][0] = "Hey, real quick before we go in there.";
        dialogue[0][1] = "I wanted to warn you that some of the crew are a bit...";
        dialogue[0][2] = "Full of themselves.";
        dialogue[0][3] = "I'm sorry for being blunt about it...";
        dialogue[0][4] = "...";
        dialogue[0][5] = "That was really rude I'm sorry.";
        dialogue[0][6] = "Lets just go...";
        dialogue[0][7] = "...";
        dialogue[0][8] = "No no..... No.";
        dialogue[0][9] = "We need to go in there.";
        dialogue[0][10] = "I'm just saying you should probably be prepared for whatevers gonna happen.";
        dialogue[0][11] = "Not to suggest that you're not prepared!";
        dialogue[0][12] = "Oh man...";
    }

    // Update is called once per frame

    public override IEnumerator StartInteraction()
    {
        float time;
        GameControl.control.Freeze();
        StartCoroutine(Visability(false, otherWilliam, otherLight));
        yield return StartCoroutine(jukebox.PauseOut(0.4f));
        time = jukebox.CurrentTime();
        jukebox.StopSong();
        anim.SetBool("Paused", true);
        jukebox.PlaySong("WillsTheme");
        StartCoroutine(Visability(true, William, thisLight));
        yield return StartCoroutine(jukebox.FadeIn(0.4f));
        yield return StartCoroutine(talkCanvas.StartDialogueSprite(dialogue[0], "default", 1, 0));
        GameControl.control.DPMainData.progression = 9;
        GameControl.control.DPMainData.viTalked = false;
        StartCoroutine(jukebox.FadeOut(0.4f));
        yield return StartCoroutine(Visability(false, William, thisLight));
        anim.SetBool("Paused", false);
        jukebox.PlaySongPartway("DP", time);
        yield return StartCoroutine(Visability(true, otherWilliam, otherLight));
        GameControl.control.DPMainData.progression = 9;
        GameControl.control.DPMainData.viTalked = false;

        Destroy(William.gameObject);
        Destroy(this.gameObject);
    }

    IEnumerator Visability(bool appearing, SpriteRenderer William, Light thisLight)
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
