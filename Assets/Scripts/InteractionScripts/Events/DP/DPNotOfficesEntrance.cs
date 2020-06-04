using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DPNotOfficesEntrance : DPOfficesEntrance
{
    // Start is called before the first frame update
    void Start()
    {
        if (!CheckCondition())
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
