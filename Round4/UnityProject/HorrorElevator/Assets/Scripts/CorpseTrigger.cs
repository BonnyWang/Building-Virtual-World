using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorpseTrigger : MonoBehaviour
{

    [SerializeField] private GameObject corpse;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = corpse.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Tirgger Corpse Movement");
            rb.isKinematic = false;

            //TODO: Need Adjustment
            SoundManager.instance.PlayJumpScareDropSound();
        }

    }

}
