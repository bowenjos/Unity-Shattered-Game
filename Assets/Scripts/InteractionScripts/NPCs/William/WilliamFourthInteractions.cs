using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WilliamFourthInteractions : TriggerInteraction
{
    TalkController talkCanvas;
    JukeBoxController jukebox;
    public SpriteRenderer William;
    public SpriteRenderer otherWilliam;
    public Light thisLight;

  
    // Start is called before the first frame update
    void Start()
    {
        jukebox = GameObject.Find("JukeBox(Clone)").GetComponent<JukeBoxController>();
        talkCanvas = GameObject.Find("Talk UI(Clone)").GetComponent<TalkController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Visability(bool appearing)
    {
        float dissappear = 1f;
        thisLight.intensity = 0.8f;
        if (!appearing)
        {
            while (dissappear > 0)
            {
                dissappear -= 0.1f;
                thisLight.intensity -= 0.08f;
                William.color = new Color(1f, 1f, 1f, dissappear);
                yield return new WaitForSeconds(0.1f);
            }
        }
        else
        {
            dissappear = 0f;
            while (dissappear < 1f)
            {
                dissappear += .1f;
                thisLight.intensity += 0.08f;
                William.color = new Color(1f, 1f, 1f, dissappear);
                yield return new WaitForSeconds(0.1f);
            }
        }


    }
}
