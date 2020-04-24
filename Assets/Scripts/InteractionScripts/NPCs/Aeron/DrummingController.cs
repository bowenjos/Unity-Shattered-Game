using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrummingController : InstrumentController
{

    public SpriteRenderer thisSprite;

    public Sprite bigDrumSprite;
    public Sprite cymbalSprite;
    public Sprite leftTomSprite;
    public Sprite rightTomSprite;
    public Sprite hiHatSprite;
    public Sprite smallDrumSprite;
    public Sprite neutralSprite;

    public Sprite pausedSprite;

    public Texture bigDrumCookie;
    public Texture cymbalCookie;
    public Texture leftTomCookie;
    public Texture rightTomCookie;
    public Texture hiHatCookie;
    public Texture smallDrumCookie;
    public Texture neutralCookie;

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
        yield return StartCoroutine(MeasureOne());
        yield return StartCoroutine(MeasureTwo());
        yield return StartCoroutine(MeasureThree());
        yield return StartCoroutine(MeasureTwo());
        changed = false;
    }

    public virtual IEnumerator MeasureOne()
    {
        yield return StartCoroutine(DrumHit(rightTomSprite, rightTomCookie));
        yield return StartCoroutine(DrumHit(neutralSprite, neutralCookie));
        yield return StartCoroutine(DrumHit(leftTomSprite, leftTomCookie));
        yield return StartCoroutine(DrumHit(rightTomSprite, rightTomCookie));
        yield return StartCoroutine(DrumHit(rightTomSprite, rightTomCookie));
        yield return StartCoroutine(DrumHit(neutralSprite, neutralCookie));
        yield return StartCoroutine(DrumHit(leftTomSprite, leftTomCookie));
        yield return StartCoroutine(DrumHit(neutralSprite, neutralCookie));
    }

    public virtual IEnumerator MeasureTwo()
    {
        yield return StartCoroutine(DrumHit(rightTomSprite, rightTomCookie));
        yield return StartCoroutine(DrumHit(neutralSprite, neutralCookie));
        yield return StartCoroutine(DrumHit(leftTomSprite, leftTomCookie));
        yield return StartCoroutine(DrumHit(neutralSprite, neutralCookie));
        yield return StartCoroutine(DrumHit(rightTomSprite, rightTomCookie));
        yield return StartCoroutine(DrumHit(neutralSprite, neutralCookie));
        yield return StartCoroutine(DrumHit(leftTomSprite, leftTomCookie));
        yield return StartCoroutine(DrumHit(neutralSprite, neutralCookie));
    }

    public virtual IEnumerator MeasureThree()
    {
        yield return StartCoroutine(DrumHit(rightTomSprite, rightTomCookie));
        yield return StartCoroutine(DrumHit(neutralSprite, neutralCookie));
        yield return StartCoroutine(DrumHit(cymbalSprite, cymbalCookie));
        yield return StartCoroutine(DrumHit(rightTomSprite, rightTomCookie));
        yield return StartCoroutine(DrumHit(rightTomSprite, rightTomCookie));
        yield return StartCoroutine(DrumHit(neutralSprite, neutralCookie));
        yield return StartCoroutine(DrumHit(cymbalSprite, cymbalCookie));
        yield return StartCoroutine(DrumHit(neutralSprite, neutralCookie));
    }

    public IEnumerator DrumHit(Sprite drumHitSprite, Texture drumHitCookie)
    {
        thisSprite.sprite = drumHitSprite;
        thisLight.cookie = drumHitCookie;
        yield return new WaitForSeconds(60f / tempo);
    }
}
