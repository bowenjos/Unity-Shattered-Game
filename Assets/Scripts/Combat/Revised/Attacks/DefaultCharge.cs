using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultCharge : MonoBehaviour
{
    public Sprite[] charge;
    public Sprite blank;
    public SpriteRenderer thisSprite;

    // Start is called before the first frame update
    void Start()
    {
        thisSprite.sprite = blank;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator animate(float time)
    {
        thisSprite.sprite = charge[0];
        yield return new WaitForSeconds(time/5f);
        thisSprite.sprite = charge[1];
        yield return new WaitForSeconds(time / 5f);
        thisSprite.sprite = charge[2];
        yield return new WaitForSeconds(time / 5f);
        thisSprite.sprite = charge[3];
        yield return new WaitForSeconds(time / 5f);
        thisSprite.sprite = charge[4];
        yield return new WaitForSeconds(time / 5f);
        thisSprite.sprite = blank;
    }

}
