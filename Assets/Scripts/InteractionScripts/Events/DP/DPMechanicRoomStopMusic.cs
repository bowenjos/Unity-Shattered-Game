using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DPMechanicRoomStopMusic : StopMusicTrigger
{
    // Start is called before the first frame update
    void Awake()
    {
        if (GameControl.control.DPMainData.progression < 1 || GameControl.control.DPMainData.progression > 5)
        {
            Destroy(this);
        }

        jukebox = GameObject.Find("JukeBox(Clone)").GetComponent<JukeBoxController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
