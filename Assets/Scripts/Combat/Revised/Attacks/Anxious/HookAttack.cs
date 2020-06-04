using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookAttack : DefaultAttack
{
    public RotateAroundOnCommand rotater;
    //Transform thisTransform;
    Transform targetTransform;

    // Start is called before the first frame update
    void Start()
    {
        marked = false;
        thisTransform = GetComponent<Transform>();
        rotater = GetComponent<RotateAroundOnCommand>();
        targetTransform = GameObject.Find("EnemyAttackPhaseSprite").GetComponent<Transform>();
        StartCoroutine(FailSafe(10f));
    }

    void Update()
    {
        if (thisTransform.position.x > targetTransform.position.x)
        {
            double value = Mathf.Atan((targetTransform.position.y - thisTransform.position.y) / (targetTransform.position.x - thisTransform.position.x)) * (180 / Mathf.PI) + 90; //* (Mathf.PI/2);
            Vector3 newAngle = new Vector3(0f, 0f, (float)value);
            Quaternion from = thisTransform.rotation;
            Quaternion to = Quaternion.Euler(newAngle);
            thisTransform.localRotation = Quaternion.Lerp(from, to, 1);
        }
        else
        {
            double value = Mathf.Atan((targetTransform.position.y - thisTransform.position.y) / (targetTransform.position.x - thisTransform.position.x)) * (180 / Mathf.PI) + 270; //* (Mathf.PI/2);
            Vector3 newAngle = new Vector3(0f, 0f, (float)value);
            Quaternion from = thisTransform.rotation;
            Quaternion to = Quaternion.Euler(newAngle);
            thisTransform.localRotation = Quaternion.Lerp(from, to, 1);
        }
    }

    public override IEnumerator Attack(float speed)
    {
        yield return null;
    }

    public IEnumerator Rotate(bool falseIsCWTrueIsCCW, float radius, int numRotations, float finalY)
    {
        Debug.Log("Hook Attack: Time to rotate");
        rotater = GetComponent<RotateAroundOnCommand>();
        yield return StartCoroutine(rotater.StartRotation(falseIsCWTrueIsCCW, radius, numRotations, this.transform.position.y + radius + finalY));
    }

    public IEnumerator MoveHook(float time, float distance)
    {
        Vector3 current = this.transform.position;
        for(int i = 0; i < 10; i++)
        {
            this.transform.position = new Vector3(current.x, current.y - (distance * i / 10), current.z);
            yield return new WaitForSeconds(time / 10);
        }
    }

    public void LaunchHook(Vector3 velocity)
    {
        this.GetComponent<Rigidbody2D>().velocity = velocity;
    }

    
}
