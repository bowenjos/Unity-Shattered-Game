using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour {

    protected bool shaking;
    public float speed;

    // Use this for initialization
    void Start () {
		
	}

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    public IEnumerator ShakeObject(float time)
    {
        Transform obj = this.transform;
        Vector2 defaultPos = obj.position;
        Vector3 cuPos = obj.position;

        float counter = 0f;

        while (counter < time)
        {
            counter += Time.deltaTime;
            float decreaseSpeed = speed;
            Vector2 tempPos = defaultPos + Random.insideUnitCircle * decreaseSpeed;
            obj.position = new Vector3(tempPos.x, tempPos.y, cuPos.z);
            yield return null;
        }

        obj.position = cuPos;
        shaking = false;
    }

    public void StartShake(float time)
    {
        if (shaking)
        {
            return;
        }
        shaking = true;
        StartCoroutine(ShakeObject(time));
    }
}
