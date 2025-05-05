using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TopDown : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float downSpeed;
    static public bool startMovig;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startMovig = false;
    }

    // Update is called once per frame
    private void FixedUpdate() {
        if(startMovig){
            rb.velocity = new Vector3(0,-downSpeed,0);

            if(transform.childCount == 0){
                SceneManager.LoadScene("Win");
            }
        }
    }
}
