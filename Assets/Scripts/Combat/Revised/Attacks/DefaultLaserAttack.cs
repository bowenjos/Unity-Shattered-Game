using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultLaserAttack : DefaultAttack
{
    // Update is called once per frame

    public override IEnumerator Attack(float speed)
    {
        float x = thisTransform.position.x;
        float y = thisTransform.position.y;

        float dxt = x / speed;
        float dyt = y / speed;

        for (float i = 0; i < speed; i += .012f)
        {
            thisTransform.position = new Vector3(-dxt * i + x, -dyt * i + y, 0);
            yield return new WaitForSeconds(.005f);

        }
    }

}
