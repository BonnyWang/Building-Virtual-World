using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleLookatCamera : MonoBehaviour
{
    Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        transform.forward = cam.transform.position - transform.position;
    }
}
