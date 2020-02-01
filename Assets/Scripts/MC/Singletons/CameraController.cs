using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour {

    public Transform Player;
    public SpriteRenderer blackOut;

    public Vector2 Margin;
    public Vector2 Smoothing;

    public BoxCollider2D Bounds;
    public Shake shaker;

    private Vector3 min, max;

    public float ortho;
    public bool IsFollowing { get; set; }

	// Use this for initialization
	void Start () {
        Bounds = GameObject.Find("CameraBounds").GetComponent<BoxCollider2D>();
        min = Bounds.bounds.min;
        max = Bounds.bounds.max;
        ortho = GetComponent<Camera>().orthographicSize;
        Player = GameObject.Find("player(Clone)").transform;
        transform.position = new Vector3(Player.position.x, Player.position.y, -10);
        IsFollowing = true;
	}

	
	// Update is called once per frame
	void Update () {
        var x = transform.position.x;
        var y = transform.position.y;

        if (IsFollowing)
        {
            if(Mathf.Abs(x - Player.position.x) > Margin.x)
            {
                x = Mathf.Lerp(x, Player.position.x, Smoothing.x * Time.deltaTime);
            }
            if(Mathf.Abs(y - Player.position.y) > Margin.y)
            {
                y = Mathf.Lerp(y, Player.position.y, Smoothing.y * Time.deltaTime);
            }
        }
        var cameraHalfWidth = ortho * ((float)Screen.width*0.75f / Screen.height);

        x = Mathf.Clamp(x, min.x + cameraHalfWidth, max.x - cameraHalfWidth);
        y = Mathf.Clamp(y, min.y + ortho, max.y - ortho);

        transform.position = new Vector3(x, y, transform.position.z);
	}

    public IEnumerator MoveFromPointToPoint(Vector2 fromPoint, Vector2 toPoint, float time)
    {
        IsFollowing = false;
        float xn = fromPoint.x;
        float yn = fromPoint.y;
        float dx = fromPoint.x - toPoint.x;
        float dy = fromPoint.y - toPoint.y;
        float dxt = dx / time;
        float dyt = dy / time;

        this.transform.position = new Vector3(fromPoint.x, fromPoint.y, this.transform.position.z);

        for (float i = 0; i < time; i += .01f)
        {
            this.transform.position = new Vector3(-dxt * i + xn, -dyt * i + yn, this.transform.position.z);
            yield return new WaitForSeconds(.01f);

        }
        this.transform.position = new Vector3(toPoint.x, toPoint.y, this.transform.position.z);
        IsFollowing = true;
        yield return null;
    }

    public IEnumerator ShakeCamera(float time, float intensity)
    {
        shaker.SetSpeed(intensity);
        yield return StartCoroutine(shaker.ShakeObject(time));
    }

    public IEnumerator FadeIn(float time)
    {
        float currentLevel = 1f;

        for(float dt = 0; dt < time; dt += Time.deltaTime)
        {
            currentLevel = 1 - dt / time;
            blackOut.color = new Color(0, 0, 0, currentLevel);
            yield return null;
        }
        blackOut.color = new Color(0, 0, 0, 0);
    }

    public IEnumerator FadeOut(float time)
    {
        float currentLevel = 1f;

        for (float dt = 0; dt < time; dt += Time.deltaTime)
        {
            currentLevel = dt / time;
            blackOut.color = new Color(0, 0, 0, currentLevel);
            yield return null;
        }
        blackOut.color = new Color(0, 0, 0, 1);
    }
}
