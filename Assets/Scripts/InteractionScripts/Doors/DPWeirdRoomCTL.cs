using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DPWeirdRoomCTL : MonoBehaviour
{
    void Start()
    {
        CheckCondition();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckCondition()
    {
        if (GameControl.control.DPMainData.key)
        {
            SceneChangeInteract comp = this.gameObject.AddComponent<SceneChangeInteract>();
            if (GameControl.control.DPMainData.levers[0])
            {
                comp.targetSceneName = "DPLessStrangeRoom";
                comp.zone = "DP";
                comp.targetX = 2.1f;
                comp.targetY = 0.75f;
            }
            else
            {
                comp.targetSceneName = "DPStrangeRoom";
                comp.zone = "DP";
                comp.targetX = -6.8f;
                comp.targetY = 1f;
            }
        }
        else
        {
            DialogueInteraction comp = this.gameObject.AddComponent<DialogueInteraction>();
            comp.dialogue = new string[1];
            comp.dialogue[0] = "The door is locked with a key.";
           
        }
        Destroy(this);
    }
}
