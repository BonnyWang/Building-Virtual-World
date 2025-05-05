using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskControll : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit(Collider other) {

        // Debug.Log("Exit" + other.gameObject.name);
        if(other.gameObject.tag == "Player"){
            other.transform.GetChild(1).gameObject.SetActive(true);            
        }    
    }

    private void OnTriggerEnter(Collider other) {

        // Debug.Log("Exit" + other.gameObject.name);
        if(other.gameObject.tag == "Player"){
            other.transform.GetChild(1).gameObject.SetActive(false);            
        }    
    }

}
