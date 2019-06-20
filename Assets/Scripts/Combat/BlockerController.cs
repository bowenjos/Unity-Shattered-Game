using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockerController : MonoBehaviour
{

    public GameObject BlockerCenter;
    public GameObject[] Blockers;

    public SpriteRenderer[] BlockersSprites;
    public BoxCollider2D[] BlockersBox;

    bool[] keys;


    // Start is called before the first frame update
    void Start()
    {
        keys = new bool[8];
        for(int i = 0; i < 12; i++)
        {
            BlockersSprites[i].color = new Color(1f, 1f, 1f, 0f);
            BlockersBox[i].enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(BattleController.BC.currentState == BattleController.BattleState.EnemyTurn)
        keys[0] = Input.GetKey(KeyCode.W);
        keys[1] = Input.GetKey(KeyCode.D);
        keys[2] = Input.GetKey(KeyCode.S);
        keys[3] = Input.GetKey(KeyCode.A);
        keys[4] = Input.GetKey(KeyCode.UpArrow);
        keys[5] = Input.GetKey(KeyCode.RightArrow);
        keys[6] = Input.GetKey(KeyCode.DownArrow);
        keys[7] = Input.GetKey(KeyCode.LeftArrow);
        StartCoroutine(ActivateBlockers());
    }

    public IEnumerator ActivateBlockers()
    {
        int count = 0;
        for(int i = 0; i < 8; i++)
        {
            if (keys[i])
            {
                count++;
            }
        }
        if(count == 1)
        {
            if (keys[0])
            {
                BlockersSprites[11].color = new Color(1f, 1f, 1f, 0.5f);
                BlockersSprites[0].color = new Color(1f, 1f, 1f, 0.5f);
                BlockersSprites[1].color = new Color(1f, 1f, 1f, 0.5f);
            }
            if (keys[1])
            {
                BlockersSprites[2].color = new Color(1f, 1f, 1f, 0.5f);
                BlockersSprites[3].color = new Color(1f, 1f, 1f, 0.5f);
                BlockersSprites[4].color = new Color(1f, 1f, 1f, 0.5f);
            }
            if (keys[2])
            {
                BlockersSprites[5].color = new Color(1f, 1f, 1f, 0.5f);
                BlockersSprites[6].color = new Color(1f, 1f, 1f, 0.5f);
                BlockersSprites[7].color = new Color(1f, 1f, 1f, 0.5f);
            }
            if (keys[3])
            {
                BlockersSprites[8].color = new Color(1f, 1f, 1f, 0.5f);
                BlockersSprites[9].color = new Color(1f, 1f, 1f, 0.5f);
                BlockersSprites[10].color = new Color(1f, 1f, 1f, 0.5f);
            }
            if (keys[4])
            {
                BlockersSprites[0].color = new Color(1f, 1f, 1f, 0.5f);
                BlockersSprites[2].color = new Color(1f, 1f, 1f, 0.5f);
                BlockersSprites[10].color = new Color(1f, 1f, 1f, 0.5f);
            }
            if (keys[5])
            {
                BlockersSprites[3].color = new Color(1f, 1f, 1f, 0.5f);
                BlockersSprites[1].color = new Color(1f, 1f, 1f, 0.5f);
                BlockersSprites[5].color = new Color(1f, 1f, 1f, 0.5f);
            }
            if (keys[6])
            {
                BlockersSprites[6].color = new Color(1f, 1f, 1f, 0.5f);
                BlockersSprites[8].color = new Color(1f, 1f, 1f, 0.5f);
                BlockersSprites[4].color = new Color(1f, 1f, 1f, 0.5f);
            }
            if (keys[7])
            {
                BlockersSprites[9].color = new Color(1f, 1f, 1f, 0.5f);
                BlockersSprites[11].color = new Color(1f, 1f, 1f, 0.5f);
                BlockersSprites[7].color = new Color(1f, 1f, 1f, 0.5f);
            }
        }
        if(count == 2)
        {

        }

        yield return null;
    }
}
