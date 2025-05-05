using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckMove : MonoBehaviour
{
    public Vector3 pointA;
    public Vector3 pointB;
    public float duckSpeed = 1.0f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float pingPong = Mathf.Sin(Time.time * duckSpeed + Mathf.PI / 2) * 0.5f + 0.5f;
        transform.position = Vector3.Lerp(pointA, pointB, pingPong);
    }
}
