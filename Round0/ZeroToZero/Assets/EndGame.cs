using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    public GameObject EndUI;
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Onion"){
            GetComponent<Animator>().SetBool("Destroy", true);
            GameObject.Find("Player").SetActive(false);
            GameObject.Find("Destroy").GetComponent<AudioSource>().Play();
            StartCoroutine(Destroy());
        }
    }

    IEnumerator Destroy(){
        yield return new WaitForSeconds(4f);
        EndUI.SetActive(true);
        
    }
}
