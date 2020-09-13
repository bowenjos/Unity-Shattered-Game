using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YolandaMasterBedroomInteraction : TriggerInteraction
{
    TalkController talkCanvas;
    public string[][] dialogue;
    JukeBoxController jukebox;
    public ViolinController vc;

    // Start is called before the first frame update
    void Start()
    {
        jukebox = GameObject.Find("JukeBox(Clone)").GetComponent<JukeBoxController>();
        talkCanvas = GameObject.Find("Talk UI(Clone)").GetComponent<TalkController>();

        if (GameControl.control.FMMainData.progression == 0 || GameControl.control.FMMainData.progression == 1)
        {
            dialogue = new string[1][];
            dialogue[0] = new string[] { "If you want to get to the shrine, you may have to take a weird route.", "A lot of things around here are in disrepair..."};
        }
    }

    // Update is called once per frame

    public override IEnumerator StartInteraction()
    {
        float time;

        GameControl.control.Freeze();
        StartCoroutine(jukebox.PauseOut(0.4f));
        time = jukebox.CurrentTime();
        jukebox.StopSong();
        vc.paused = true;

        yield return StartCoroutine(talkCanvas.StartDialogueSprite(dialogue[0], "default", 2, 0));

        jukebox.PlaySongPartway("FM", time);
        StartCoroutine(jukebox.FadeIn(0.4f));
        vc.paused = false;
    }
}
