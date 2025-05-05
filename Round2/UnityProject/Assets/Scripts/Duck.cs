using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Duck : MonoBehaviour
{
    Animator anim;
    Rigidbody rb;

    [SerializeField] bool drownOnStart = false;
    [SerializeField] bool tutorialDuck = false;
    [SerializeField] bool bottleDuck = false;
    static AudioSource duckTalk;
    static SoundPlayer duckduck;
    bool inwater;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        if(drownOnStart){
            anim.SetBool("IsDrown", true);
        }

        rb = GetComponent<Rigidbody>();
        inwater = false;

        duckTalk = GameObject.Find("DuckHelp").GetComponent<AudioSource>();
        duckduck = GameObject.Find("Duckduck").GetComponent<SoundPlayer>();
    }

    private void FixedUpdate() {
        if(rb != null){
            if(rb.velocity.magnitude > 1.5f){
                rb.velocity = rb.velocity.normalized*1.5f;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        // Constraint the duck movement to be good
        if(collision.gameObject.tag =="Water" && (!tutorialDuck)){
            if(rb != null){
                if(!inwater){
                    rb.velocity = Vector3.zero;
                    rb.angularVelocity = Vector3.zero;
                    inwater = true;
                    rb.constraints = RigidbodyConstraints.FreezeRotationX;
                    rb.constraints = RigidbodyConstraints.FreezeRotationZ;
                    // rb.constraints = RigidbodyConstraints.FreezeAll;
                    // StartCoroutine(reactivateDuck());
                }
            }
        }
        if (collision.gameObject.tag == "Bubble") // Replace 'Bubble' with the actual tag you use for bubbles
        {   
            if(anim != null){
                anim.SetBool("IsDrown", true);
            }
        }

        if(collision.gameObject.tag == "LeftHand" || collision.gameObject.tag == "RightHand"){
            if(bottleDuck){
                Destroy(GetComponent<UpDownMovement>());
                rb.constraints = RigidbodyConstraints.None;
            }

            if(tutorialDuck){
                transform.GetChild(2).gameObject.SetActive(true);
                anim.SetBool("IsTalk", true);
                duckTalk.Play();
                StartCoroutine(latehideText());
            }else{
                duckduck.play_random();
            }
        }
    }

    IEnumerator reactivateDuck(){
        yield return new WaitForSeconds(2f);
        if(rb != null){
            rb.constraints = RigidbodyConstraints.None;
            // rb.useGravity = true;
        }
    }

    IEnumerator latehideText(){
        yield return new WaitForSeconds(2f);
        anim.SetBool("IsTalk", false);
        transform.GetChild(2).gameObject.SetActive(false);
    }
    public void freeDuck(){
        if(anim != null){
            anim.SetBool("IsDrown", false);
        }
        if(rb != null){
            rb.constraints = RigidbodyConstraints.None;
            rb.useGravity = true;
        }

        if((transform.parent != null) && GameObject.Find("GamePlay") != null){
            transform.parent = GameObject.Find("GamePlay").transform;
        }
    }
}
