using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViolinController : InstrumentController
{
    public SpriteRenderer thisSprite;

    public Sprite[] backSprite;
    public Texture[] backCookie;
    public Sprite[] forthSprite;
    public Texture[] forthCookie;
    public Sprite pausedSprite;
    public Texture pausedCookie;

    public float tempo;
    public bool paused;

    // Start is called before the first frame update
    void Start()
    {
        //Will need to change later
        if (GameControl.control.DPMainData.progression != 0)
        {
            Destroy(this.gameObject);
        }
    
        //anim = this.gameObject.GetComponent<Animator>();
        thisLight = this.gameObject.GetComponentInChildren<Light>();
    }

    void Update()
    {
        if(paused)
        {
            thisSprite.sprite = pausedSprite;
            thisLight.cookie = pausedCookie;
        }
        if (!changed && !paused)
        {
            StartCoroutine(Change());
        }
    }

    public override IEnumerator Change()
    {
        changed = true;
        int style = Random.Range(0, 2);
        thisSprite.sprite = forthSprite[style];
        thisLight.cookie = forthCookie[style];
        yield return new WaitForSeconds(60f/tempo);
        thisSprite.sprite = backSprite[style];
        thisLight.cookie = backCookie[style];
        yield return new WaitForSeconds(60f/tempo);
        changed = false;
    }

}
