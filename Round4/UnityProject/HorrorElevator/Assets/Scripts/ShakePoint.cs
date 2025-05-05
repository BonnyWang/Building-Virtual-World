using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ShakePoint : MonoBehaviour
{
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
            
        if(other.gameObject.tag == "Player"){
            StartCoroutine(Camera.main.transform.parent.GetComponent<CameraShake>().Shake(0.2f, 0.2f));
        }
    }
}
