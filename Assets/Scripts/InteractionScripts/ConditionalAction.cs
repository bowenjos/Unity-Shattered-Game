using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionalAction : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (CheckCondition())
        {
            Destroy(GetComponent<InteractionController>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual bool CheckCondition()
    {
        return false;
    }

}
