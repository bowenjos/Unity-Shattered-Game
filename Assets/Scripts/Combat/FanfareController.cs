using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanfareController : MonoBehaviour
{
    public AudioSource[] fanfares;

    public void PlayFanfare()
    {
        fanfares[0].Play();
        for(int i = 1; i < 8; i++)
        {
            if (GameControl.control.masks[i-1])
            {
                fanfares[i].Play();
            }
        }
    }
}
