using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefab;
    public bool isTimer;
    public bool repeatedlySpawn;
    public float delaySeconds;

    bool waiting;
    int counter;

    // Start is called before the first frame update
    void Start()
    {
        waiting = false;
        GameObject thisObject = Spawn();
        StartCoroutine(Activate(thisObject));
        StartCoroutine(Wait());
    }

    // Update is called once per frame
    void Update()
    {
        if (!waiting && repeatedlySpawn)
        {
            GameObject thisObject = Spawn();
            StartCoroutine(Activate(thisObject));
            StartCoroutine(Wait());
        }
    }

    public GameObject Spawn()
    {
        GameObject thisObject = Instantiate(prefab);
        thisObject.transform.position = this.transform.position;
        thisObject.GetComponent<SwaySideToSide>().special = counter;
        waiting = true;
        counter++;
        return thisObject;
    }

    public IEnumerator Activate(GameObject thisObject)
    {
        yield return new WaitForEndOfFrame();
        thisObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
    }

    public IEnumerator Wait()
    {
        
        yield return new WaitForSeconds(delaySeconds);
        waiting = false;
    }
}
