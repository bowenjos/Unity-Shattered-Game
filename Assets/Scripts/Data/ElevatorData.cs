using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ElevatorData {

    public bool[] unlockedZones = new bool[9];
    public bool[][] unlockedElevators = new bool[9][];

}
