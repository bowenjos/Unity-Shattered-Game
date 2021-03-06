﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopMusicTrigger : TriggerInteraction
{
    protected JukeBoxController jukebox;

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
        if (jukebox.IsPlaying())
        {
            jukebox.StopSong();
        }
        yield return null;
    }
}
