using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiltBackAndForth : MonoBehaviour
{
    public float angleMax;
    public float modifier;
    public float speed;



    Transform tf;

    // Start is called before the first frame update
    void Start()
    {
        tf = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        float value = (float)Math.Sin(speed * Time.time) * angleMax;
        Vector3 newAngle = new Vector3(0f, 0f, value);
        Quaternion from = tf.rotation;
        Quaternion to = Quaternion.Euler(newAngle);

        tf.localRotation = Quaternion.Lerp(from, to, speed);
    }
}
