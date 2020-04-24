using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingingController : InstrumentController
{

    public SpriteRenderer thisSprite;

    public Sprite upSprite;
    public Sprite downSprite;
    public Sprite pausedSprite;
    public Texture pausedCookie;

    public float tempo;
    public bool paused;

    // Start is called before the first frame update
    void Start()
    {
        thisLight = this.gameObject.GetComponentInChildren<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if (paused)
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
        thisSprite.sprite = upSprite;
        thisLight.cookie = Cookie1;
        yield return new WaitForSeconds(60f / tempo);
        thisSprite.sprite = downSprite;
        thisLight.cookie = Cookie0;
        yield return new WaitForSeconds(60f / tempo);
        changed = false;
    }
}
