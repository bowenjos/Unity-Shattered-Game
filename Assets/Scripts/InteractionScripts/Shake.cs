using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour {

    protected bool shaking;
    public float speed;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    IEnumerator ShakeObject(float time)
    {
        Transform obj = this.transform;
        Vector2 defaultPos = obj.position;

        float counter = 0f;

        while (counter < time)
        {
            counter += Time.deltaTime;
            float decreaseSpeed = speed;
            Vector2 tempPos = defaultPos + Random.insideUnitCircle * decreaseSpeed;
            obj.position = tempPos;
            yield return null;
        }

        obj.position = defaultPos;
        shaking = false;
        Debug.Log("Done");
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
