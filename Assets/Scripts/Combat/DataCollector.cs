using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataCollector : MonoBehaviour {

    protected GameObject Player;
    protected GameObject Enemy;
    protected EnemyCombatControl ECC;
    protected GameObject EnemySprite;
    protected GameObject Talk;
    protected BattleFlow bf;
    protected GameObject playerTalk;
    protected EnemyHornController eHC;

    // Use this for initialization
    void Start () {
        Debug.Log("Two of them");
        Player = GameObject.Find("player(Clone)");
        Enemy = GameObject.Find("Enemy");
        ECC = Enemy.GetComponent<EnemyCombatControl>();
        Debug.Log(Player);
        Player.SetActive(false);

        EnemySprite = GameObject.Find("EnemySprite");
        eHC = GameObject.Find("EnemyHorn").GetComponent<EnemyHornController>();
        Talk = GameObject.Find("FoeChatterText");
        bf = GameObject.Find("BattleControl").GetComponent<BattleFlow>();
        playerTalk = GameObject.Find("PlayerChatterText");

        EnemySprite.GetComponent<Image>().sprite = ECC.battleSprite;
        eHC.hornTempo = ECC.tempo;
        bf.enemyHealth = ECC.enemyHealth;
        bf.enemyHealthReset = ECC.enemyHealthReset;
        bf.enemyEmotion = ECC.enemyEmotion;
        bf.enemyLevelState = ECC.enemyLevelState;
        bf.oldScene = ECC.sceneName;
        bf.fleeable = ECC.fleeable;
        bf.approachDialogue = ECC.dialogueApproach;
        bf.idleDialogue = ECC.dialogueIdle;
        bf.talkDialogue = ECC.dialoguePlayerTalk;
        bf.stateChangeDialogue = ECC.dialogueStateChange;
        bf.enemyResistences = ECC.enemyResistences;
        bf.Player = Player;
        GameObject.Find("Mirror").GetComponent<MirrorController>().damageValue = ECC.enemyDamageValue;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
