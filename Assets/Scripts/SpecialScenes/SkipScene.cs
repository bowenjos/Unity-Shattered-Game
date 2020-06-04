using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipScene : MonoBehaviour
{

    public string targetSceneName;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitForKeyDown("Submit"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected IEnumerator WaitForKeyDown(string keyCode)
    {
        do
        {
            yield return null;
        } while (!Input.GetButtonDown(keyCode));

        SceneManager.LoadScene(targetSceneName, LoadSceneMode.Single);
    }
}
