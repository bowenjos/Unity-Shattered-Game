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
    bool[] blockers;

    int counter;
    bool counting;

    public bool semiSolid;


    // Start is called before the first frame update
    void Start()
    {
        counting = false;
        counter = 0;
        semiSolid = false;
        keys = new bool[8];
        blockers = new bool[12];
        for(int i = 0; i < 12; i++)
        {
            BlockersSprites[i].color = new Color(1f, 1f, 1f, 0f);
            BlockersBox[i].enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (BattleController.BC.currentState == BattleController.BattleState.EnemyTurn)
        {
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
    }

    public IEnumerator ActivateBlockers()
    {
        if (!counting)
        {
            for (int i = 0; i < 12; i++)
            {
                BlockersSprites[i].color = new Color(1f, 1f, 1f, 0.2f);
            }
        }

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
            counter = 0;
            semiSolid = true;
            for (int i = 0; i < 12; i++)
            {
                BlockersBox[i].enabled = false;
            }
            if (keys[0])
            {
                BlockersSprites[11].color = new Color(1f, 1f, 1f, 0.5f);
                BlockersSprites[0].color = new Color(1f, 1f, 1f, 0.5f);
                BlockersSprites[1].color = new Color(1f, 1f, 1f, 0.5f);
                BlockersBox[11].enabled = true;
                BlockersBox[0].enabled = true;
                BlockersBox[1].enabled = true;
            }
            if (keys[1])
            {
                BlockersSprites[2].color = new Color(1f, 1f, 1f, 0.5f);
                BlockersSprites[3].color = new Color(1f, 1f, 1f, 0.5f);
                BlockersSprites[4].color = new Color(1f, 1f, 1f, 0.5f);
                BlockersBox[2].enabled = true;
                BlockersBox[3].enabled = true;
                BlockersBox[4].enabled = true;
            }
            if (keys[2])
            {
                BlockersSprites[5].color = new Color(1f, 1f, 1f, 0.5f);
                BlockersSprites[6].color = new Color(1f, 1f, 1f, 0.5f);
                BlockersSprites[7].color = new Color(1f, 1f, 1f, 0.5f);
                BlockersBox[5].enabled = true;
                BlockersBox[6].enabled = true;
                BlockersBox[7].enabled = true;
            }
            if (keys[3])
            {
                BlockersSprites[8].color = new Color(1f, 1f, 1f, 0.5f);
                BlockersSprites[9].color = new Color(1f, 1f, 1f, 0.5f);
                BlockersSprites[10].color = new Color(1f, 1f, 1f, 0.5f);
                BlockersBox[8].enabled = true;
                BlockersBox[9].enabled = true;
                BlockersBox[10].enabled = true;
            }
            if (keys[4])
            {
                BlockersSprites[0].color = new Color(1f, 1f, 1f, 0.5f);
                BlockersSprites[2].color = new Color(1f, 1f, 1f, 0.5f);
                BlockersSprites[10].color = new Color(1f, 1f, 1f, 0.5f);
                BlockersBox[0].enabled = true;
                BlockersBox[2].enabled = true;
                BlockersBox[10].enabled = true;
            }
            if (keys[5])
            {
                BlockersSprites[3].color = new Color(1f, 1f, 1f, 0.5f);
                BlockersSprites[1].color = new Color(1f, 1f, 1f, 0.5f);
                BlockersSprites[5].color = new Color(1f, 1f, 1f, 0.5f);
                BlockersBox[3].enabled = true;
                BlockersBox[1].enabled = true;
                BlockersBox[5].enabled = true;
            }
            if (keys[6])
            {
                BlockersSprites[6].color = new Color(1f, 1f, 1f, 0.5f);
                BlockersSprites[8].color = new Color(1f, 1f, 1f, 0.5f);
                BlockersSprites[4].color = new Color(1f, 1f, 1f, 0.5f);
                BlockersBox[6].enabled = true;
                BlockersBox[8].enabled = true;
                BlockersBox[4].enabled = true;
            }
            if (keys[7])
            {
                BlockersSprites[9].color = new Color(1f, 1f, 1f, 0.5f);
                BlockersSprites[11].color = new Color(1f, 1f, 1f, 0.5f);
                BlockersSprites[7].color = new Color(1f, 1f, 1f, 0.5f);
                BlockersBox[9].enabled = true;
                BlockersBox[11].enabled = true;
                BlockersBox[7].enabled = true;
            }
        }
        else if (count == 2)
        {
            //semiSolid = false;
            if (keys[0] && keys[4] && !counting)
            {
                BlockersSprites[0].color = new Color(1f, 1f, 1f, 0.5f);
                BlockersBox[0].enabled = true;
                StartCoroutine(KeyHold(0, 4, 0));
            }
            if(keys[0] && keys[5] && !counting)
            {
                BlockersSprites[1].color = new Color(1f, 1f, 1f, 0.5f);
                BlockersBox[1].enabled = true;
                StartCoroutine(KeyHold(0, 5, 1));
            }
            if (keys[1] && keys[4] && !counting)
            {
                BlockersSprites[2].color = new Color(1f, 1f, 1f, 0.5f);
                BlockersBox[2].enabled = true;
                StartCoroutine(KeyHold(1, 4, 2));
            }
            if (keys[1] && keys[5] && !counting)
            {
                BlockersSprites[3].color = new Color(1f, 1f, 1f, 0.5f);
                BlockersBox[3].enabled = true;
                StartCoroutine(KeyHold(1, 5, 3));
            }
            if (keys[1] && keys[6] && !counting)
            {
                BlockersSprites[4].color = new Color(1f, 1f, 1f, 0.5f);
                BlockersBox[4].enabled = true;
                StartCoroutine(KeyHold(1, 6, 4));
            }
            if (keys[2] && keys[5] && !counting)
            {
                BlockersSprites[5].color = new Color(1f, 1f, 1f, 0.5f);
                BlockersBox[5].enabled = true;
                StartCoroutine(KeyHold(2, 5, 5));
            }
            if (keys[2] && keys[6] && !counting)
            {
                BlockersSprites[6].color = new Color(1f, 1f, 1f, 0.5f);
                BlockersBox[6].enabled = true;
                StartCoroutine(KeyHold(2, 6, 6));
            }
            if (keys[2] && keys[7] && !counting)
            {
                BlockersSprites[7].color = new Color(1f, 1f, 1f, 0.5f);
                BlockersBox[7].enabled = true;
                StartCoroutine(KeyHold(2, 7, 7));
            }
            if (keys[3] && keys[6] && !counting)
            {
                BlockersSprites[8].color = new Color(1f, 1f, 1f, 0.5f);
                BlockersBox[8].enabled = true;
                StartCoroutine(KeyHold(3, 6, 8));
            }
            if (keys[3] && keys[7] && !counting)
            {
                BlockersSprites[9].color = new Color(1f, 1f, 1f, 0.5f);
                BlockersBox[9].enabled = true;
                StartCoroutine(KeyHold(3, 7, 9));
            }
            if (keys[3] && keys[4] && !counting)
            {
                BlockersSprites[10].color = new Color(1f, 1f, 1f, 0.5f);
                BlockersBox[10].enabled = true;
                StartCoroutine(KeyHold(3, 4, 10));
            }
            if (keys[0] && keys[7] && !counting)
            {
                BlockersSprites[11].color = new Color(1f, 1f, 1f, 0.5f);
                BlockersBox[11].enabled = true;
                StartCoroutine(KeyHold(0, 7, 11));
            }
        }
        else
        {
            for (int i = 0; i < 12; i++)
            {
                BlockersBox[i].enabled = false;
            }

            counter = 0;


        }

        yield return null;
    }

    public IEnumerator KeyHold(int x, int y, int block)
    {
        semiSolid = false;
        counting = true;
        Debug.Log("Block Start");
        
        while (keys[x] && keys[y] && (counter < 4))
        {
            BlockersSprites[block].color = new Color(1f, 1f, 1f, 1f);
            counter++;
            Debug.Log(counter);
            yield return new WaitForSeconds(.05f);
        }
        Debug.Log("Block stop");
        counting = false;
        semiSolid = true;
    }

}
