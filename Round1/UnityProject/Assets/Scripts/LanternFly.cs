using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;


public class LanternFly : MonoBehaviour
{
    const int ONE_HAND = 1;
    const int TWO_HANDS = 2;
    public static float moveSpeed = 0.5f;

    
    int clapState;
    // Clap State 0 no hands detected
    // Clap State 1 first hand detected
    // Clap State 2 second hand detected

    public static int destroyedFlyNumber = 0;

    public static int destroyGoal = 10;
    Rigidbody rb;

    public GameObject ancher;
    public bool isDemo;

    LevelManager levelManager;

    GameObject canvasForScore;

    RandomPlay clapSound;
    
    void Start()
    {
        clapState = 0;
        rb = GetComponent<Rigidbody>();
        moveSpeed = 0.05f;
        
        if(isDemo){
            moveSpeed = 0.01f;
        }

        
        transform.rotation = new Quaternion(0, Random.Range(-1, 1), 0, 0);

        ancher = GameObject.Find("Player");
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        canvasForScore = GameObject.Find("Canvas");
        clapSound = GameObject.Find("ClapSound").GetComponent<RandomPlay>();

        if(levelManager.currentLevel == 2){
            moveSpeed = 0.1f;

            destroyGoal = 5;
        }
    }

    // Update is called once per frame
    private void FixedUpdate() {
        move();

    }

    void move(){
        // Randomly move the fly

        //Debug.Log(moveSpeed);
       
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

            // Change depend on the state of the lantern fly

            if(!isDemo){
                destroyedFlyNumber++;
                StartCoroutine(showScoreBoard());
                if(destroyedFlyNumber >= destroyGoal){

                    switch (levelManager.currentLevel)
                    {
                        case 1:
                            destroyedFlyNumber = 0;
                            levelManager.level1To2();
                            canvasForScore.transform.GetChild(0).gameObject.SetActive(false);

                            break;
                        case 2:
                        
                            StartCoroutine(toBossScene());
                            break;
                        default:
                            break;
                    }
                }
            }else{
                // This is for the start scene
                StartCoroutine(startMainGame());
            }

           
            clapSound.play(1f);
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(true);
            GetComponent<Collider>().enabled = false;
            
        }
    }


    public void hoverExit(){
        clapState--;
        Debug.Log("Hand Left");
    }

    IEnumerator startMainGame(){
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("Main");
    }

    IEnumerator showScoreBoard(){
        canvasForScore.transform.GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        canvasForScore.transform.GetChild(0).gameObject.SetActive(false);
    }

    IEnumerator toBossScene(){
        GameObject.Find("ThankYou").GetComponent<AudioSource>().Play();

        Transform NPCs = GameObject.Find("NPCs").transform;
        NPCs.position = new Vector3(0f,1f,25.95f);
        NPCs.GetChild(0).GetComponent<Animator>().SetTrigger("Cheer");
        NPCs.GetChild(1).GetComponent<Animator>().SetTrigger("Cheer");
        yield return new WaitForSeconds(7f);
        
        Timer.timeRemaining = 60f;
        SceneManager.LoadScene("Boss");
    }


}
