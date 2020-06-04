using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfflictionMod : MonoBehaviour
{
    public int affliction; //what the affliction is
    public int numTurns; //how many turns it will be applied for
    public int oneInWhat; //one in how many chances of being afflicted (1/oneInWhat)

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Afflict()
    {
        int random = Random.Range(0, oneInWhat);
        if(random == 0)
        {
            BattleController.BC.playerAffliction = affliction;
            if(BattleController.BC.turnsAfflicted < numTurns)
            {
                BattleController.BC.turnsAfflicted = numTurns;
            }
        }
    }
}
