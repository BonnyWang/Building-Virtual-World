using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    Rigidbody playerRB;
    bool lockPosition = false;
    void Start(){
        playerRB = GameObject.Find("Player").GetComponentInChildren<Rigidbody>();
    }
    private void Update() {
        // Fixed the position because of z-axis different
        playerRB.AddForce(Vector3.Normalize(new Vector3(0,15,0) - playerRB.transform.position)*0.001f, ForceMode.Impulse);
        if(lockPosition){
            playerRB.transform.position = transform.position;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player"){
            Debug.Log("Portal");
            other.gameObject.GetComponentInChildren<Animator>().SetBool("Twist", true);

            lockPosition = true;
            StartCoroutine(enterBlackHole());
            // other.gameObject.transform.position = new Vector3(0, 0, 0);
        }
    }

    IEnumerator enterBlackHole(){
        yield return new WaitForSeconds(2.5f);

        SceneManager.LoadScene("Asteroid");
    }
}
