using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormController : MonoBehaviour {

    public AudioClip thunder1;
    public AudioClip thunder2;
    public AudioClip thunder3;

    bool tried;

	// Use this for initialization
	void Start () {
        tried = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(tried == false)
        {
            StartCoroutine(tryThunder());
        }
	}

    IEnumerator tryThunder()
    {
        tried = true;
        yield return new WaitForSeconds(30f + Random.Range(-15f, 15f));
        Thunder();
        yield return new WaitForSeconds(30f + Random.Range(-15f, 15f));
        tried = false;

    }

    void Thunder()
    {
        int rand = Random.Range(0, 3);
        this.GetComponent<AudioSource>();
        switch (rand)
        {
            case 0:
                this.GetComponent<AudioSource>().clip = thunder1;
                break;
            case 1:
                this.GetComponent<AudioSource>().clip = thunder2;
                break;
            case 2:
                this.GetComponent<AudioSource>().clip = thunder2;
                break;
        }
        this.GetComponent<AudioSource>().Play();
        StartCoroutine(Flash());
    }

    IEnumerator Flash()
    {
        this.GetComponentInChildren<Light>().intensity = 10;
        yield return new WaitForSeconds(0.1f);
        this.GetComponentInChildren<Light>().intensity = 0;
    }

}
