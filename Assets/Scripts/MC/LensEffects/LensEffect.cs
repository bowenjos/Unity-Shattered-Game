using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LensEffect : MonoBehaviour
{
    public int lensColor;
    public bool fired;

    void Start()
    {
        fired = false;
    }


    void OnTriggerStay2D(Collider2D col)
    {
 
        try
        {
            if (col.GetComponent<LensInteractionController>().lensColor == lensColor && !fired)
            {
                fired = true;
                StartCoroutine(col.GetComponent<LensInteractionController>().StartInteraction());
            }
        }
        catch (Exception e)
        {
            Debug.Log("Object In Lens " + e);
        }
    }

}
