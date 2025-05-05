using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeToOnion : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {

        if(other.gameObject.tag != "Onion"){
            other.transform.GetChild(0).gameObject.SetActive(false);
            other.transform.GetChild(2).gameObject.SetActive(true);
            other.transform.GetComponentInChildren<Controller>().moveSpeed = 2f;
            other.gameObject.tag = "Onion";

            GameObject.Find("IamOnion").GetComponent<AudioSource>().Play();

            this.gameObject.SetActive(false);

        }

    }
}
