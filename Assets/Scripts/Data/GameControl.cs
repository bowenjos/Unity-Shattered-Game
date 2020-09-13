using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class GameControl : MonoBehaviour {

    public static GameControl control;

    //NPCS
    public ViData ViData = new ViData();
    public WillData WillData = new WillData();
    public YolandaData YolandaData = new YolandaData();
    public EliseData EliseData = new EliseData();

    //ZoneData
    public ElevatorData ElevatorData = new ElevatorData();
    public DPMainData DPMainData = new DPMainData();
    public FMMainData FMMainData = new FMMainData();

    //Other
    public EnemyData EnemyData;
    
    //Player Data
    public string playerName;
    public int health;
    public int maxHealth;
    public int healFactor;
    public float money;
    public int numMasks;
    public float playedTime;
    public bool[] doors = new bool[10];
    public bool[] keys = new bool[5];

    /**
    * Doors
    * 0: Dead Performance (Basement Weird Room)
    * 1: SilentEntryhall - (Silent Entryhall Basement)
    * 2: Dead Performance (Stage - Reception)
    * 3: SilentEntryhall (Forgotten Mezzanine Dining Hall)
    * 4: SilentEntryhall (Forgotten Mezzanine Mezzanine)
    * 5: SilentEntryhall (Desolate Heart Entrance)
    * 6: ForgottenMezzanine (Mezzanine Door)
    **/

    /**
    * Keys
    * 0: SilentEntryhall - CavernousDescent (Door 1)
    * 1: ForgottenMezzanine - Mezzanine Door (Door 6)
    **/

    //Not Stored Data
    public int curLens;
    public bool aggroable;
    public bool paused;
    public bool frozen;
    public bool stunned;
    public bool playerFrozen;
    public bool encounter;

    //Player Items Data
    public string[] items = new string[10];
    public bool[] lens = new bool[7];
    public bool[] masks = new bool[7];
    public bool lantern;

    //Player Location Data
    public string room;
    public string zone;
    public string saveRoomName;

    //Last Save Data
    public float playedTimeTemp;
    public bool[] lensTemp = new bool[7];
    public bool[] masksTemp = new bool[7];
    public string saveRoomNameTemp;

    public bool saved;

    // Use this for initialization
    void Awake () {

        //Cursor.visible = false;

        if (control == null)
        {
            DontDestroyOnLoad(gameObject);
            control = this;
        }
        else if (control != this)
        {
            Destroy(gameObject);
        }

    }
	
	// Update is called once per frame
	void Update () {
        playedTime += Time.deltaTime;
	}

    public void Pause()
    {
        paused = true;
        frozen = true;
    }

    public void Unpause()
    {
        paused = false;
        frozen = false;
    }

    public void Freeze()
    {
        frozen = true;
    }

    public void Unfreeze()
    {
        frozen = false;
    }

    public void PlayerFreeze()
    {
        playerFrozen = true;
    }

    public void PlayerUnfreeze()
    {
        playerFrozen = false;
    }

    public IEnumerator Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/playerSave.dat", FileMode.OpenOrCreate);

        PlayerData data = new PlayerData();

        GameControl.control.saved = false;

        data.ElevatorData = ElevatorData;
        data.ViData = ViData;
        data.WillData = WillData;
        data.YolandaData = YolandaData;
        data.EliseData = EliseData;
        data.EnemyData = EnemyData;
        data.DPMainData = DPMainData;

        data.playerName = playerName;
        data.maxHealth = maxHealth;
        data.health = health;
        data.healFactor = healFactor;
        data.money = money;
        data.numMasks = numMasks;
        data.playedTime = playedTime;

        data.items = items;
        data.lens = lens;
        data.masks = masks;

        data.room = room;
        data.zone = zone;
        data.saveRoomName = saveRoomName;
        data.lantern = lantern;

        bf.Serialize(file, data);
        file.Close();

        yield return null;
    }

    public void Load()
    {
        if(File.Exists(Application.persistentDataPath + "/playerSave.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerSave.dat", FileMode.Open);
            PlayerData data = (PlayerData) bf.Deserialize(file);
            file.Close();

            ElevatorData = data.ElevatorData;
            ViData = data.ViData;
            WillData = data.WillData;
            YolandaData = data.YolandaData;
            EliseData = data.EliseData;

            DPMainData = data.DPMainData;

            playerName = data.playerName;
            maxHealth = data.maxHealth;
            health = data.health;
            healFactor = data.healFactor;
            money = data.money;
            numMasks = data.numMasks;
            playedTime = data.playedTime;

            items = data.items;
            lens = data.lens;
            masks = data.masks;

            room = data.room;
            zone = data.zone;
            saveRoomName = data.saveRoomName;
            lantern = data.lantern;
        }
    }

    public void LoadTemp()
    {
        if (File.Exists(Application.persistentDataPath + "/playerSave.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerSave.dat", FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();

            playerName = data.playerName;
            playedTimeTemp = data.playedTime;
            lensTemp = data.lens;
            masksTemp = data.masks;
            saveRoomNameTemp = data.saveRoomName;
        }
        else
        {
            playedTimeTemp = 0f;
            lensTemp = new bool[]{ false, false, false, false, false, false, false};
            masksTemp = new bool[] { false, false, false, false, false, false, false };
        }
    }
}

[Serializable]
class PlayerData
{
    public ViData ViData;
    public WillData WillData;
    public YolandaData YolandaData;
    public EliseData EliseData;

    public EnemyData EnemyData;

    public ElevatorData ElevatorData;
    public DPMainData DPMainData;

    public string playerName;
    public int health;
    public int maxHealth;
    public int healFactor;
    public float money;
    public int numMasks;
    public float playedTime;

    public string[] items = new string[10];
    public bool[] lens = new bool[7];
    public bool[] masks = new bool[7];

    public string room;
    public string zone;
    public string saveRoomName;
    public bool lantern;
}
