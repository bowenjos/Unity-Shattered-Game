using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSelf : MonoBehaviour
{
    protected bool cooldown;
    public float angle;
    public float time;
    public bool counterClockwise;

    // Start is called before the first frame update
    void Start()
    {
        cooldown = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!cooldown)
        {
            StartCoroutine(Rotate());
        }
    }

    public IEnumerator Rotate()
    {
        cooldown = true;
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z + angle);
        yield return new WaitForSeconds(time);
        cooldown = false;
    }
}
