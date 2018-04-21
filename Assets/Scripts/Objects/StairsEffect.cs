using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsEffect : MonoBehaviour {

    public float intensity;
    public int direction;

    protected PlayerController pCon;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            pCon = col.gameObject.GetComponent<PlayerController>();
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (pCon != null)
        {
            if (pCon.walking && pCon.direction == 1)
            {
                if (direction == 1)
                {
                    col.gameObject.transform.position += Vector3.down * intensity * Time.deltaTime;
                }
                else if (direction == 3)
                {
                    col.gameObject.transform.position += Vector3.up * intensity * Time.deltaTime;
                }
            }
            else if (pCon.walking && pCon.direction == 3)
            {
                if (direction == 1)
                {
                    col.gameObject.transform.position += Vector3.up * intensity * Time.deltaTime;
                }
                else if (direction == 3)
                {
                    col.gameObject.transform.position += Vector3.down * intensity * Time.deltaTime;
                }
            }
        }
    }
}
