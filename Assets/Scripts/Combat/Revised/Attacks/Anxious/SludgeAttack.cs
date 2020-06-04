using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SludgeAttack : DefaultAttack
{
    // Start is called before the first frame update
    void Start()
    {
        marked = false;
        thisTransform = GetComponent<Transform>();
        StartCoroutine(FailSafe(30f));
        StartCoroutine(Attack(speed));

        //Random chance to be an affliction attack anytime this attack is spawned.
        int random = Random.Range(0, 3);
        if (random == 0)
        {
            this.GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 1f);
            AfflictionMod afflictionMod = this.gameObject.AddComponent<AfflictionMod>();
            afflictionMod.affliction = 1;
            afflictionMod.numTurns = 3;
            afflictionMod.oneInWhat = 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (thisTransform.position.x > Vector3.zero.x)
        {
            double value = Mathf.Atan((Vector3.zero.y - thisTransform.position.y) / (Vector3.zero.x - thisTransform.position.x)) * (180 / Mathf.PI) + 90; //* (Mathf.PI/2);
            Vector3 newAngle = new Vector3(0f, 0f, (float)value);
            Quaternion from = thisTransform.rotation;
            Quaternion to = Quaternion.Euler(newAngle);
            thisTransform.localRotation = Quaternion.Lerp(from, to, 1);
        }
        else
        {
            double value = Mathf.Atan((Vector3.zero.y - thisTransform.position.y) / (Vector3.zero.x - thisTransform.position.x)) * (180 / Mathf.PI) + 270; //* (Mathf.PI/2);
            Vector3 newAngle = new Vector3(0f, 0f, (float)value);
            Quaternion from = thisTransform.rotation;
            Quaternion to = Quaternion.Euler(newAngle);
            thisTransform.localRotation = Quaternion.Lerp(from, to, 1);
        }
    }

    public override IEnumerator Attack(float speed)
    {
        yield return new WaitForSeconds(speed);
        float x = thisTransform.position.x;
        float y = thisTransform.position.y;

        float dxt = x / 2f;
        float dyt = y / 2f;

        for (float i = 0; i < 2f; i += .01f)
        {
            if (BattleController.BC.currentState == BattleController.BattleState.Dying)
            {
                Destroy(this);
            }
            thisTransform.position = new Vector3(-dxt * i + x, -dyt * i + y, 0);
            yield return new WaitForSeconds(.01f);

        }
    }

}
