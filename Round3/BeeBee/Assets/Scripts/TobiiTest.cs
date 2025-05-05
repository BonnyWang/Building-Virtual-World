using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.Gaming;

public class TobiiTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Camera.main.ScreenToWorldPoint(TobiiAPI.GetGazePoint().Screen);
        transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
    }
}
