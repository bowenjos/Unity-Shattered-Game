using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeTargetLocation : MonoBehaviour
{
    public string newTargetLocation;
    public float x;
    public float y;

    // Start is called before the first frame update
    void Start()
    {
        if (CheckCondition())
        {
            SceneChangeInteract SCI = GetComponent<SceneChangeInteract>();
            SceneChangeCollide SCC = GetComponent<SceneChangeCollide>();
            if(SCI != null)
            {
                SCI.targetSceneName = newTargetLocation;
                SCI.targetX = x;
                SCI.targetY = y;
            }
            else if(SCC != null)
            {
                SCC.targetSceneName = newTargetLocation;
                SCC.targetX = x;
                SCC.targetY = y;
            }
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
