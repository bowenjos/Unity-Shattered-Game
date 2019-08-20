using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StormController : MonoBehaviour {

    public AudioClip thunder1;
    public AudioClip thunder2;
    public AudioClip thunder3;

    public StormNodeData stormNode;
    public AudioSource rain;
    public AudioSource thunder;
    public Light lightning;

    bool tried;

	// Use this for initialization
	void Start () {
        tried = false;
        stormNode = GameObject.Find("StormNode").GetComponent<StormNodeData>();
        lightning.cookie = stormNode.thunderCookie;
        lightning.cookieSize = stormNode.cookieSize;
        rain.volume = stormNode.stormVolume;
        thunder.volume = stormNode.thunderVolume;
    }

    void OnSceneLoaded(Scene aScene, LoadSceneMode aMode)
    {
        stormNode = GameObject.Find("StormNode").GetComponent<StormNodeData>();
        lightning.cookie = stormNode.thunderCookie;
        lightning.cookieSize = stormNode.cookieSize;
        rain.volume = stormNode.stormVolume;
        thunder.volume = stormNode.thunderVolume;

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
        //30f + (range 0 to 60)
        yield return new WaitForSeconds(30f + Random.Range(0f, 60f));
        Thunder();
        yield return new WaitForSeconds(30f + Random.Range(0f, 60f));
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
        if (!stormNode.lightningOff)
        {
            StartCoroutine(Flash());
        }
    }

    IEnumerator Flash()
    {
        this.GetComponentInChildren<Light>().intensity = 5;
        yield return new WaitForSeconds(0.2f);
        this.GetComponentInChildren<Light>().intensity = 0;
    }

}
