using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ElevatorData {

    public bool[] unlockedZones = new bool[9];
    public bool[][] unlockedElevators = new bool[9][];
    public string[][] roomElevators = new string[9][];
    public string[][] nameElevators = new string[9][];

    public ElevatorData()
    {
        for(int i = 0; i < 9; i++)
        {
            unlockedZones[i] = false;
            unlockedElevators[i] = new bool[4];
            nameElevators[i] = new string[4];
            roomElevators[i] = new string[4];
        }

        //Silent Entryhall
        nameElevators[0][0] = ""; roomElevators[0][0] = "";
        nameElevators[0][1] = ""; roomElevators[0][1] = "";
        nameElevators[0][2] = ""; roomElevators[0][2] = "";
        nameElevators[0][3] = ""; roomElevators[0][3] = "";

        //Dead Performance
        nameElevators[1][0] = "Rafters"; roomElevators[1][0] = "DPRafters";
        nameElevators[1][1] = "Lobby"; roomElevators[1][1] = "DPReception";
        nameElevators[1][2] = ""; roomElevators[1][2] = "";
        nameElevators[1][3] = ""; roomElevators[1][3] = "";

        //Forgotten Mezzanine
        nameElevators[2][0] = ""; roomElevators[2][0] = "";
        nameElevators[2][1] = ""; roomElevators[2][1] = "";
        nameElevators[2][2] = ""; roomElevators[2][2] = "";
        nameElevators[2][3] = ""; roomElevators[2][3] = "";

        //Frigid Loft
        nameElevators[3][0] = ""; roomElevators[3][0] = "";
        nameElevators[3][1] = ""; roomElevators[3][1] = "";
        nameElevators[3][2] = ""; roomElevators[3][2] = "";
        nameElevators[3][3] = ""; roomElevators[3][3] = "";

        //Natural Banquet
        nameElevators[4][0] = ""; roomElevators[4][0] = "";
        nameElevators[4][1] = ""; roomElevators[4][1] = "";
        nameElevators[4][2] = ""; roomElevators[4][2] = "";
        nameElevators[4][3] = ""; roomElevators[4][3] = "";

        //Festered Kiln
        nameElevators[5][0] = ""; roomElevators[5][0] = "";
        nameElevators[5][1] = ""; roomElevators[5][1] = "";
        nameElevators[5][2] = ""; roomElevators[5][2] = "";
        nameElevators[5][3] = ""; roomElevators[5][3] = "";

        //Departure Sandbox
        nameElevators[6][0] = ""; roomElevators[6][0] = "";
        nameElevators[6][1] = ""; roomElevators[6][1] = "";
        nameElevators[6][2] = ""; roomElevators[6][2] = "";
        nameElevators[6][3] = ""; roomElevators[6][3] = "";

        //Cavernous Decline
        nameElevators[7][0] = ""; roomElevators[7][0] = "";
        nameElevators[7][1] = ""; roomElevators[7][1] = "";
        nameElevators[7][2] = ""; roomElevators[7][2] = "";
        nameElevators[7][3] = ""; roomElevators[7][3] = "";

        //Desolate Heart
        nameElevators[8][0] = ""; roomElevators[8][0] = "";
        nameElevators[8][1] = ""; roomElevators[8][1] = "";
        nameElevators[8][2] = ""; roomElevators[8][2] = "";
        nameElevators[8][3] = ""; roomElevators[8][3] = "";
    }

}
