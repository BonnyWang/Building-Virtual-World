using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckDrown : MonoBehaviour
{
    Animator anim;
    Rigidbody rb;

    [SerializeField] bool drownOnStart = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        if(drownOnStart){
            anim.SetBool("IsDrown", true);
        }

        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bubble") // Replace 'Bubble' with the actual tag you use for bubbles
        {
            anim.SetBool("IsDrown", true);
        }
    }

    void freeDuck(){
        rb.constraints = RigidbodyConstraints.None;
        rb.useGravity = true;
        transform.parent = GameObject.Find("GamePlay").transform;
    }
    void Update()
    {
        
    }
}
