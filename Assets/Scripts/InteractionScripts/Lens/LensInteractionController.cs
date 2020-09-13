using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LensInteractionController : MonoBehaviour
{

    public int lensColor;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual IEnumerator StartInteraction()
    {
        Debug.Log("Lens Interaction Start");
        yield return null;
    }

}
