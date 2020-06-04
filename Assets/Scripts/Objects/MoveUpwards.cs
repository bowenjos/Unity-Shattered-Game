using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUpwards : MonoBehaviour
{
    public float speed;
    public float timer;

    Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = new Vector3(0, speed, 0);
        StartCoroutine(DeleteAfterTimer());
        
    }

    // Update is called once per frame
    public IEnumerator DeleteAfterTimer()
    {
        yield return new WaitForSeconds(timer);
        Destroy(this.gameObject);
    }
}
