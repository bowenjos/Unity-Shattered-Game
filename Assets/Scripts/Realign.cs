using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Realign : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y);
    }
}
