using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockerBlock : MonoBehaviour
{
    // Start is called before the first frame update
    public BlockerController blockController;

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.name != "Blocker")
        {
            DefaultAttack attack = col.gameObject.GetComponent<DefaultAttack>();
            
            if (!blockController.semiSolid)
            {
                Debug.Log("Blocked");
                Destroy(col.gameObject);
            }
            else if(blockController.semiSolid && !attack.marked)
            {
                SpriteRenderer sprite = attack.GetComponent<SpriteRenderer>();
                attack.damageValue = attack.damageValue / 2;
                sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, sprite.color.a / 2f);
                attack.marked = true;
            }

        }
    }
}
