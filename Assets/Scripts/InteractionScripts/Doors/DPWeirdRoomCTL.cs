using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DPWeirdRoomCTL : ChangeTargetLocation
{
    // Update is called once per frame
    void Update()
    {
        
    }

    public override bool CheckCondition()
    {
        if (GameControl.control.DPMainData.levers[0])
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
