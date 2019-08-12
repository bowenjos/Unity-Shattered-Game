using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WilliamDefaultInteraction : CharacterInteraction
{
    TalkController talkCanvas;
    JukeBoxController jukebox;
    public SpriteRenderer William;
    public Light thisLight;
    public Animator anim;

    public new string[] dialogue;

    // Start is called before the first frame update
    void Start()
    {
        jukebox = GameObject.Find("JukeBox(Clone)").GetComponent<JukeBoxController>();
        talkCanvas = GameObject.Find("Talk UI(Clone)").GetComponent<TalkController>();
    }

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
        yield return StartCoroutine(talkCanvas.StartDialogueSprite(dialogue, "default", 1, 0));
        anim.SetBool("Paused", false);
        jukebox.PlaySongPartway("DP", time);

    }
}
