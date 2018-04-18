using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour {

    public Transform Player;

    public Vector2 Margin;
    public Vector2 Smoothing;

    public BoxCollider2D Bounds;

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
        var cameraHalfWidth = ortho * ((float)Screen.width / Screen.height);

        x = Mathf.Clamp(x, min.x + cameraHalfWidth, max.x - cameraHalfWidth);
        y = Mathf.Clamp(y, min.y + ortho, max.y - ortho);

        transform.position = new Vector3(x, y, transform.position.z);
	}

}
