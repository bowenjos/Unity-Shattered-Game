using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MonoBehaviour
{
    public float RotateSpeed = 5f;
    public float Radius = 0.1f;

    public Vector2 _centre;
    public float _angle;

    private void Start()
    {
        //_centre = transform.position;
    }

    private void Update()
    {

        _angle += RotateSpeed * Time.deltaTime;

        var offset = new Vector2(Mathf.Sin(_angle), Mathf.Cos(_angle)) * Radius;
        transform.position = _centre + offset;
    }
}
