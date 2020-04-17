using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemySpawner : MonoBehaviour
{

    public GameObject[] enemies;

    public GameObject enemy;

    // Start is called before the first frame update
    public void SpawnEnemy(EnemyCombatController.EnemyIDs enemyID, int enemyNumber)
    {
        switch (enemyID)
        {
            case EnemyCombatController.EnemyIDs.jitterbug:
                enemy = Instantiate(enemies[0]);
                break;
            case EnemyCombatController.EnemyIDs.fresnetic:
                enemy = Instantiate(enemies[1]);
                break;
            case EnemyCombatController.EnemyIDs.lunasee:
                enemy = Instantiate(enemies[2]);
                break;
            case EnemyCombatController.EnemyIDs.showbizzy:
                enemy = Instantiate(enemies[3]);
                break;
            case EnemyCombatController.EnemyIDs.tar:
                enemy = Instantiate(enemies[4]);
                break;
        }

        enemy.name = "Enemy";
        enemy.GetComponent<EnemyCombatController>().enemyNumber = enemyNumber;

    }

}

