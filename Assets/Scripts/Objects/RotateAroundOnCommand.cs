using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAroundOnCommand : RotateAround
{
    // Start is called before the first frame update
    void Start()
    {
        Radius = .75f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator StartRotation(bool falseIsCWTrueIsCCW, float newRadius, int numRotations, float endY)
    {
        int curRotations = 0;
        int delay = 0;
        Radius = newRadius;

        _angle = Mathf.PI;
        Debug.Log("Rotating...");
        while(curRotations < numRotations)
        {
            Debug.Log(curRotations);
            if (falseIsCWTrueIsCCW)
            {
                _angle -= RotateSpeed * Time.deltaTime;
            }
            else
            {
                _angle += RotateSpeed * Time.deltaTime;
            }
            if(transform.position.x > transform.position.x-0.1f && transform.position.x < transform.position.x+0.1f && delay < 0)
            {
                curRotations++;
                Debug.Log(curRotations);
                delay = 10;
            }

            var offset = new Vector2(Mathf.Sin(_angle), Mathf.Cos(_angle)) * Radius;
            transform.position = _centre + offset;
            delay--;
            yield return new WaitForEndOfFrame();
        }
        while(transform.position.y > endY+.05f || transform.position.y < endY - .05f)
        {
            Debug.Log("Looking for a way out.");
            if (falseIsCWTrueIsCCW)
            {
                _angle -= RotateSpeed * Time.deltaTime;
            }
            else
            {
                _angle += RotateSpeed * Time.deltaTime;
            }
            var offset = new Vector2(Mathf.Sin(_angle), Mathf.Cos(_angle)) * Radius;
            transform.position = _centre + offset;
            yield return new WaitForEndOfFrame();
        }

    }
}
