using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ButterFly : MonoBehaviour
{
    const int ONE_HAND = 1;
    const int TWO_HANDS = 2;
    int clapState;

    static float moveSpeed = 2f;

    Rigidbody rb;
    GameObject ancher;

    RandomPlay butterFlySound;
    GameObject canvas;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ancher = GameObject.Find("Player");
        butterFlySound = GameObject.Find("ButterFlySound").GetComponent<RandomPlay>();
        canvas = GameObject.Find("Canvas");
    }

    // Update is called once per frame
    private void FixedUpdate() {
        move();    
    }

    void move(){
        // Randomly move the fly

       
        Vector3 randomDirection = new Vector3(Random.Range(-moveSpeed, moveSpeed), Random.Range(-moveSpeed, moveSpeed), Random.Range(-moveSpeed, moveSpeed));

        // Reset velocity to balance the force
        if(rb.velocity.magnitude > 1f || rb.angularVelocity.magnitude > 1f){
            rb.velocity = randomDirection;
            rb.angularVelocity = randomDirection;
        }

        Vector3 direction = randomDirection;
        // Resitrict the fly to the constraint area
        if(Vector3.Distance( transform.position, ancher.transform.position) > 2.5f){
            direction = Vector3.Normalize(ancher.transform.position - transform.position + randomDirection*0.1f)*Mathf.Abs(randomDirection.magnitude);
        }

        rb.AddForce(direction);
    }

    public void hoverEnter(){
        clapState++;

        if(clapState == ONE_HAND){
            Debug.Log("First Hand Detected");
        }
        else if(clapState == TWO_HANDS){
            Debug.Log("Second Hand Detected");

           
            butterFlySound.play(1f);
            StartCoroutine(showTimeLost());
            transform.GetChild(0).gameObject.SetActive(false);
            GetComponent<Collider>().enabled = false;

            Debug.Log("handTimer Object:" + Timer.timeRemaining.ToString());

            Timer.timeRemaining -= 5f;

            
        }
    }


    public void hoverExit(){
        clapState--;
        Debug.Log("Hand Left");
    }

    IEnumerator showTimeLost(){
        canvas.transform.GetChild(1).gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        canvas.transform.GetChild(1).gameObject.SetActive(false);
    }


}
