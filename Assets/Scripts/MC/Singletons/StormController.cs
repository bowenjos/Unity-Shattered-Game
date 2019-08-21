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
    public SpriteRenderer lightningReal;

    bool tried;

	// Use this for initialization
	void Start () {
        SceneManager.sceneLoaded += OnSceneLoaded;
        tried = false;
        stormNode = GameObject.Find("StormNode").GetComponent<StormNodeData>();
        lightningReal.sprite = stormNode.thunderCookie;
        rain.volume = stormNode.stormVolume;
        thunder.volume = stormNode.thunderVolume;
    }

    void OnSceneLoaded(Scene aScene, LoadSceneMode aMode)
    {
        Debug.Log("loaded");
        stormNode = GameObject.Find("StormNode").GetComponent<StormNodeData>();
        lightningReal.sprite = stormNode.thunderCookie;
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
        //this is a hopefully temporary solution to the fact OnSceneLoaded doesn't seem to run
        tried = true;
        stormNode = GameObject.Find("StormNode").GetComponent<StormNodeData>();
        lightningReal.sprite = stormNode.thunderCookie;
        rain.volume = stormNode.stormVolume;
        thunder.volume = stormNode.thunderVolume;
        //END FIX
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
        for (float i = 0f; i < 1f; i += 0.2f)
        {
            lightningReal.color = new Color(1f, 1f, 1f, i);
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(0.3f);
        for (float i = 1f; i > 0f; i -= 0.1f)
        {
            lightningReal.color = new Color(1f, 1f, 1f, i);
            yield return new WaitForSeconds(0.01f);
        }
        lightningReal.color = new Color(1f, 1f, 1f, 0f);
    }

}
