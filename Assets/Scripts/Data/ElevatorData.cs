using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ElevatorData {

    public bool[] unlockedZones = new bool[9];
    public bool[][] unlockedElevators = new bool[9][];

    public ElevatorData()
    {
        for(int i = 0; i < 9; i++)
        {
            unlockedZones[i] = false;
        }

        unlockedElevators[0] = new bool[1];
        unlockedElevators[1] = new bool[2];
        unlockedElevators[2] = new bool[2];
        unlockedElevators[3] = new bool[2];
        unlockedElevators[4] = new bool[2];
        unlockedElevators[5] = new bool[2];
        unlockedElevators[6] = new bool[2];
        unlockedElevators[7] = new bool[2];
        unlockedElevators[8] = new bool[2];

    }

}
