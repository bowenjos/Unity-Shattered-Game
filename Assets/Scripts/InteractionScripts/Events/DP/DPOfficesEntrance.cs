using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DPOfficesEntrance : ConditionalAction
{
    // Start is called before the first frame update
    void Start()
    {
        if (CheckCondition())
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override bool CheckCondition()
    {
        if(GameControl.control.DPMainData.progression > 6)
        {
            return true;
        }
        return false;
    }
}
