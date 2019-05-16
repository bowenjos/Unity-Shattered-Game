using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DPLessWeirdRoomMusicChange : TriggerInteraction
{
    JukeBoxController jukebox;

    // Start is called before the first frame update
    void Start()
    {
        jukebox = GameObject.Find("JukeBox(Clone)").GetComponent<JukeBoxController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override IEnumerator StartInteraction()
    {
        jukebox.ReplaceSongPartway("DP");
        jukebox.SetVolume(0.6f);
        yield return null;
    }

} 
