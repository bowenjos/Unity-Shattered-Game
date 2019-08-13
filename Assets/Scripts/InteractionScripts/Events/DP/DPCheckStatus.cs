using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DPCheckStatus : MonoBehaviour
{
    public int lowerlimit;
    public int upperlimit;

    // Start is called before the first frame update
    void Start()
    {
        if ((GameControl.control.DPMainData.progression < lowerlimit) || (GameControl.control.DPMainData.progression > upperlimit))
        {
            Destroy(this.gameObject);
        }
    }


}
