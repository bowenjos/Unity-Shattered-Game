using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DPMainData {

    //Progression Data
    public int progression = 0;
    // 0 - Will In Basement (Moved once talked to)
    // 1 - Will in Maintainence (Gives key)
    // 2-4 Will in Maintainence (Progressed by activating levers)
    // 5 - Will in Maintainence (Progressed when talked to)
    // 6 - Will in Changing Rooms (Progressed when talked to)
    // 7 - Will in Reception (progressed when talked to)
    // 8 - Will in Office Rooms (Progresses when you approach the door to the practice room)
    // 9 - Battle with Tar
    // 10 - Cutscene after Tar
    // 11 - Will on stage (Talk to initiate final cutscene and Battle)

    public bool key = false;
    public bool williamTalked = false;
    public bool viTalked = false;
    public bool[] levers = { false, false, false };

    //Enemy Data to come

    //Bonus Stuff

}
