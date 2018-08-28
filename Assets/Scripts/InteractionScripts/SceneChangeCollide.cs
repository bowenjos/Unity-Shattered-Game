using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeCollide : MonoBehaviour {

    protected GameObject player;
    protected TransitionController TC;
    public string targetSceneName;
    public string zone;
    public float targetX;
    public float targetY;

    // Use this for initialization
    void Awake () {
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "player(Clone)")
        {
            StartCoroutine(Change());
        }
    }

    IEnumerator Change()
    {
        TC = GameObject.Find("TransitionControl(Clone)").GetComponent<TransitionController>();
        player = GameObject.Find("player(Clone)");
        GameControl.control.Freeze();
        yield return StartCoroutine(TC.transitionOut());
        SceneManager.LoadScene(targetSceneName, LoadSceneMode.Single);
        GameControl.control.room = targetSceneName;
        GameControl.control.zone = zone;
        TC.newZone = zone;
        //GameControl.control.zone = zone;
        player.transform.position = new Vector3(targetX, targetY, 0);
        Debug.Log("Finished Prepping");
    }
}
