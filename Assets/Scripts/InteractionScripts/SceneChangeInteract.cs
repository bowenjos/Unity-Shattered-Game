using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeInteract : InteractionController {

    protected GameObject player;
    protected TransitionController TC;
    public string targetSceneName;
    public string zone;
    public float targetX;
    public float targetY;

    public bool immediate;

	// Use this for initialization
	void Awake () { 
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public override IEnumerator StartInteraction()
    {
        //Animate
        GameControl.control.Freeze();
        TC = GameObject.Find("TransitionControl(Clone)").GetComponent<TransitionController>();
        player = GameObject.Find("player(Clone)");
        if (!immediate) { yield return StartCoroutine(TC.transitionOut()); }
        else { yield return StartCoroutine(TC.transitionNow()); }
        
        SceneManager.LoadScene(targetSceneName, LoadSceneMode.Single);
        GameControl.control.room = targetSceneName;
        GameControl.control.zone = zone;
        TC.newZone = zone;
        //GameControl.control.zone = zone;
        player.transform.position = new Vector3(targetX, targetY, 0);
    }
}
