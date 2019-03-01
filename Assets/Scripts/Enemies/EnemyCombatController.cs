using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombatController : MonoBehaviour {

    //A Class for storing data relevant to Monster's

    public bool fleeable;

    public double enemyHealth;
    public double enemyHealthMax;
    public string enemyEmotion;
    public int[,] enemyResistances;
    public int enemyLevel;

    public string[] introDialogue;
    public string[][] talkDialogue;
    public string[][] sitDialogue;
    public string[][] hugDialogue;
    public string[][] actDialogue;
    public string[][] affirmDialogue;
    public string[][] giftDialogue;

    public string[][] enemyChatter;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
