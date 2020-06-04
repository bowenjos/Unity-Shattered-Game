using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ElevatorExitInteraction : SceneChangeInteract {

    public override IEnumerator StartInteraction()
    {
        //Animate
        GameControl.control.Freeze();
        TC = GameObject.Find("TransitionControl(Clone)").GetComponent<TransitionController>();
        player = GameObject.Find("player(Clone)");
        yield return StartCoroutine(TC.transitionOut());
        TC.elevatorTransfer = true;
        SceneManager.LoadScene(targetSceneName, LoadSceneMode.Single);
        this.GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f, 0f);
        GameControl.control.room = targetSceneName;
        GameControl.control.zone = zone;
        TC.newZone = zone;
        Destroy(this.gameObject);

    }
}
