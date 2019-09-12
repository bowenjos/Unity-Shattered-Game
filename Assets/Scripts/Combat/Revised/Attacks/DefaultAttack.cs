using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultAttack : MonoBehaviour
{
    protected Transform thisTransform;
    public float speed;
    public int damageValue;

    // Start is called before the first frame update
    void Start()
    {
        thisTransform = GetComponent<Transform>();
        StartCoroutine(Attack(speed));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual IEnumerator Attack(float speed)
    {
        float x = thisTransform.position.x;
        float y = thisTransform.position.y;

        float dxt = x / speed;
        float dyt = y / speed;


        for (float i = 0; i < speed; i += .01f)
        {
            thisTransform.position = new Vector3(-dxt * i + x, -dyt * i + y, 0);
            yield return new WaitForSeconds(.01f);

        }
    }
}
