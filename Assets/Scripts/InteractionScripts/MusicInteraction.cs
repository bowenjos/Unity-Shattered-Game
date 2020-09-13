using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicInteraction : DialogueInteraction
{
    JukeBoxController jukebox;
    public string songName;
    public bool hasDialogue;
    public float waitTime;

    string currentSong;

    // Start is called before the first frame update
    void Start()
    {
        talkCanvas = GameObject.Find("Talk UI(Clone)");
        jukebox = GameObject.Find("JukeBox(Clone)").GetComponent<JukeBoxController>();
        
    }

    // Update is called once per frame
    public override IEnumerator StartInteraction()
    {
        GameControl.control.Freeze();
        yield return StartCoroutine(jukebox.PauseOut(0.4f));
        currentSong = jukebox.CurrentSong();
        jukebox.StopSong();
        jukebox.PlaySong(songName);
        yield return StartCoroutine(jukebox.FadeIn(0.01f));
        if (hasDialogue)
        {
            yield return StartCoroutine(talkCanvas.GetComponent<TalkController>().StartDialogueSolo(dialogue));
        }
        else
        {
            yield return new WaitForSeconds(waitTime);
        }
        yield return StartCoroutine(jukebox.PauseOut(0.05f));
        jukebox.StopSong();
        jukebox.PlaySong(currentSong);
        StartCoroutine(jukebox.FadeIn(0.4f));


    }
}
