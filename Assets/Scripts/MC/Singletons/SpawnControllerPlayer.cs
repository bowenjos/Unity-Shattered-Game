using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnControllerPlayer : MonoBehaviour {

    public static SpawnControllerPlayer scp;

    public GameObject Player;
    public GameObject PlayerPrefab;

    public GameObject Camera;
    public GameObject CameraPrefab;

    public GameObject UI;
    public GameObject UIPrefab;

    public GameObject Overlay;
    public GameObject OverlayPrefab;

    public GameObject PauseMenu;
    public GameObject PauseMenuPrefab;

    public GameObject EventSystem;
    public GameObject EventSystemPrefab;

    public GameObject TransitionControl;
    public GameObject TransitionControlPrefab;

    public GameObject JukeBox;
    public GameObject JukeBoxPrefab;

    public GameObject SideBars;
    public GameObject SideBarsPrefab;

    public GameObject StormController;
    public GameObject StormControllerPrefab;

    public GameObject EncounterNode;
    public GameObject EncounterNodePrefab;

    public GameObject SpawnLocation;

    // Use this for initialization
    void Awake () {

        if (scp == null)
        {
            DontDestroyOnLoad(gameObject);
            scp = this;
        }
        else if (scp != this)
        {
            Destroy(gameObject);
        }

        //Player character spawn controller
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            //player already exists, so just move it to the spawn location and set the Player gameobject parameter
            Player = GameObject.FindGameObjectWithTag("Player");

        }
        else
        {
            //instantiate the player
            Player = Instantiate(PlayerPrefab);
            Player.transform.position = SpawnLocation.transform.position;
        }

        if(GameObject.Find("Camera(Clone)") != null)
        {
            Camera = GameObject.Find("Camera(Clone)");
        }
        else
        {
            Camera = Instantiate(CameraPrefab);
        }

        //UI Spawn controller
        if (GameObject.Find("Talk UI(Clone)") != null)
        {
            //player already exists, so just move it to the spawn location and set the Player gameobject parameter
            UI = GameObject.Find("Talk UI(Clone)");

        }
        else
        {
            //instantiate the UI
            UI = Instantiate(UIPrefab);
        }

        if(GameObject.Find("Overlay(Clone)") != null)
        {
            Overlay = GameObject.Find("Overlay(Clone)");
        }
        else
        {
            Overlay = Instantiate(OverlayPrefab);
        }

        if (GameObject.Find("PauseMenu(Clone)") != null)
        {
            PauseMenu = GameObject.Find("PauseMenu(Clone)");
        }
        else
        {
            PauseMenu = Instantiate(PauseMenuPrefab);
        }

        if (GameObject.Find("EventSystem(Clone)") != null)
        {
            EventSystem = GameObject.Find("EventSystem(Clone)");
        }
        else
        {
            EventSystem = Instantiate(EventSystemPrefab);
        }

        if (GameObject.Find("TransitionControl(Clone)") != null)
        {
            EventSystem = GameObject.Find("TransitionControl(Clone)");
        }
        else
        {
            EventSystem = Instantiate(TransitionControlPrefab);
        }

        if (GameObject.Find("JukeBox(Clone)") != null)
        {
            JukeBox = GameObject.Find("JukeBox(Clone)");
        }
        else
        {
            JukeBox = Instantiate(JukeBoxPrefab);
        }

        if (GameObject.Find("SideBars(Clone)") != null)
        {
            SideBars = GameObject.Find("SideBars(Clone)");
        }
        else
        {
            SideBars = Instantiate(SideBarsPrefab);
        }

        if(GameObject.Find("StormSystem(Clone)") != null)
        {
            StormController = GameObject.Find("StormSystem(Clone)");
        }
        else
        {
            StormController = Instantiate(StormControllerPrefab);
        }

        if (GameObject.Find("EncounterNode(Clone)") != null)
        {
            EncounterNode = GameObject.Find("EncounterNode(Clone)");
        }
        else
        {
            EncounterNode = Instantiate(EncounterNodePrefab);
        }
    }
	
}
