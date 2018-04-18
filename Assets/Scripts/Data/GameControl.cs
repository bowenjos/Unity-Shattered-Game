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

    //Rooms
    public MainRoom MainRoom = new MainRoom();

    //Player Data
    public string playerName;
    public float health;
    public float maxHealth;
    public float money;
    public int numMasks;
    public float playedTime;

    //Not Stored Data
    public int curLens;
    public bool aggroable;
    public bool paused;
    public bool frozen;

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

    public IEnumerator Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/playerSave.dat", FileMode.OpenOrCreate);

        PlayerData data = new PlayerData();

        GameControl.control.saved = false;

        data.MainRoom = MainRoom;

        data.playerName = playerName;
        data.maxHealth = maxHealth;
        data.health = health;
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

            MainRoom = data.MainRoom;

            playerName = data.playerName;
            maxHealth = data.maxHealth;
            health = data.health;
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
    public MainRoom MainRoom;

    public string playerName;
    public float health;
    public float maxHealth;
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
