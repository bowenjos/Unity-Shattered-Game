using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class EncounterNodeData : MonoBehaviour
{

    public EnemyCombatController.EnemyIDs enemyID;
    public int enemyNumber;
    public float enemyX;
    public float enemyY;
    public float playerX;
    public float playerY;

    // Start is called before the first frame update
    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnSceneLoaded(Scene aScene, LoadSceneMode mode)
    {
        if (aScene.name == "Encounter")
        {
            GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>().SpawnEnemy(enemyID, enemyNumber);
        }
    }

}
