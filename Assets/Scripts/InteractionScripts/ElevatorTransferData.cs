using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorTransferData : MonoBehaviour {

    public string currentZone;
    public string currentRoom;
    public int zoneValue;
    public int roomValue;

    void Start()
    {
        GameControl.control.ElevatorData.unlockedElevators[zoneValue][roomValue] = true;
        GameControl.control.ElevatorData.unlockedZones[zoneValue] = true;

        GameControl.control.ElevatorData.unlockedZones[4] = true;
    }

    void Update()
    {
        if (GameControl.control.room != currentRoom && GameControl.control.room != "Elevator")
        {
            Destroy(this.gameObject);
        }
    }
}
