using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

public class BossAI : MonoBehaviour
{

    Transform bloodLine;
    PlayerBoss playerboss;

    Vector3 bloodLineScale;

    RandomPlay punchSound;

    public GameObject speezeEffect;
    GameObject squeezeInstance;

    Rigidbody rb;
    Animator animator;

    void Start()
    {
        bloodLine = transform.GetChild(1);
        playerboss = GameObject.Find("Player").GetComponent<PlayerBoss>();
        punchSound = GameObject.Find("PunchSound").GetComponent<RandomPlay>();

        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        StartCoroutine(dashtoPlayer());
    }

    // Update is called once per frame
    void Update()
    {
        //HoverEnterEventArgs.interactorObject;
    }

    private void OnTriggerEnter(Collider other) {
        bloodLineScale = bloodLine.localScale;
        if(other.gameObject.tag == "LeftHand" ){
            bloodLineScale.x -= playerboss.left_Velocity*0.1f;
            punchSound.play(playerboss.right_Velocity*3f);
            squeezeInstance = Instantiate(speezeEffect, other.transform.position, Quaternion.identity);
            changeSqueezeSize(squeezeInstance, playerboss.left_Velocity*0.5f);
            StartCoroutine(destorySqueeze(squeezeInstance));
        }
        if(other.gameObject.tag == "RightHand" ){
            bloodLineScale.x -= playerboss.right_Velocity*0.5f;
            punchSound.play(playerboss.right_Velocity*3f);
            squeezeInstance = Instantiate(speezeEffect, other.transform.position, Quaternion.identity);
            changeSqueezeSize(squeezeInstance, playerboss.right_Velocity*0.1f);
            StartCoroutine(destorySqueeze(squeezeInstance));
        }

        bloodLine.localScale = bloodLineScale;
        if(bloodLine.localScale.x <= 0){
            bloodLine.localScale = Vector3.zero;
            LevelManager.winning = true;
            StartCoroutine(toSuccessScene());
        }
    }

    void changeSqueezeSize(GameObject squeezeInstance,float size){
        ParticleSystem[] PSs = squeezeInstance.GetComponentsInChildren<ParticleSystem>();
        foreach(ParticleSystem PS in PSs){
            var PSMain = PS.main;
            PSMain.duration = size;
            PS.emissionRate = size*10000f;
        }
    }

    IEnumerator toSuccessScene(){
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Win");
        
    }

    IEnumerator destorySqueeze(GameObject speezeEffect){
        yield return new WaitForSeconds(2f);
        Destroy(speezeEffect);
    }


    IEnumerator dashtoPlayer(){
        yield return new WaitForSeconds(10f);
        animator.SetTrigger("Dash");
        rb.velocity = Vector3.Normalize(playerboss.transform.position - transform.position)*15f;
        yield return new WaitForSeconds(3f);
        rb.velocity = -rb.velocity;
        yield return new WaitForSeconds(3f);
        rb.velocity = Vector3.zero;
        
        // for loop 
        for(int i = 0; i < 3; i++){
             yield return new WaitForSeconds(5f);
            animator.SetTrigger("Dash");
            rb.velocity = Vector3.Normalize(playerboss.transform.position - transform.position)*20f;
            yield return new WaitForSeconds(3f);
            rb.velocity = -rb.velocity;
            yield return new WaitForSeconds(3f);
            rb.velocity = Vector3.zero;
        }
       
    }
    
}
