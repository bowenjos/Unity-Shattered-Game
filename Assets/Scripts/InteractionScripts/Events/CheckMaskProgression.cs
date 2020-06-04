using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckMaskProgression : MonoBehaviour
{

    public int maskToCheck;
    public bool deleteIfMaskTrue;

    // Start is called before the first frame update
    void Start()
    {
        if (GameControl.control.masks[maskToCheck])
        {
            if (deleteIfMaskTrue)
            {
                Destroy(this.gameObject);
            }
        }
        else if(!deleteIfMaskTrue)
        {
            Destroy(this.gameObject);
        }
    }

}
