using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bubble : MonoBehaviour
{
    public bool fromPlayer;
    Rigidbody rb;
    [SerializeField] float moveSpeed = 2f;
    public int health = 1;
    GameObject rightHandRef;
    GameObject leftHandRef;

    [SerializeField] GameObject bubblePopEffect;


    public int color;
    // 0 Red
    // 1 Blue
    // 2 Green
    

    public bool interacted;
    bool snaped;

    [SerializeField] Material[] bubbleMaterials;

    static SoundPlayer bubblePopSound;
    static AudioSource bubbleUnbreak;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        rightHandRef = null;
        leftHandRef = null;

        interacted = false;
        snaped = false;

        setColor(color, true);

        if(bubblePopSound == null){
            bubblePopSound = GameObject.Find("BubblePop").GetComponent<SoundPlayer>();
            bubbleUnbreak = GameObject.Find("UnBreak").GetComponent<AudioSource>();
        }

    }

    
    private void FixedUpdate() {
        
        // Velocity Constraint
        foreach(Rigidbody rigidibody in GetComponentsInChildren<Rigidbody>()){
            if((rigidibody.velocity.magnitude > 1.5f) && (!snaped)){
                rigidibody.velocity = rigidibody.velocity.normalized*1.5f;
                Destroy(GetComponent<HingeJoint>());
            }
        }

        if((rb.velocity.magnitude > 1.5f) && (!snaped)){
            rb.velocity = rb.velocity.normalized*1.5f;
        }
        
        
        // Release the bubble
        if((GetComponent<HingeJoint>()!=null) && (rb.velocity.magnitude > 0.25f)){
            Destroy(GetComponent<HingeJoint>());
            snaped = false;
        }
    }


    public void setColor(int color, bool checkGroup = false){
        if(checkGroup){
            if(transform.parent.parent !=null){
                if(transform.parent.parent.GetComponent<GroupColor>() != null){
                    this.color = transform.parent.parent.GetComponent<GroupColor>().color;

                }
            }
        }else{
            this.color = color;
        }

        // Debug.Log(color);

        transform.parent.GetChild(1).GetComponent<SkinnedMeshRenderer>().material = bubbleMaterials[color];
    }

    

    private void OnCollisionEnter(Collision other) {
        // Snap to hand
        // Debug.Log("Collided with " + other.gameObject.name);
        Bubble otherBubble = other.gameObject.GetComponent<Bubble>();

        if(other.gameObject.tag == "Water"){
            if(!fromPlayer){
                SceneManager.LoadScene("Fail");
            }
        }

        // Check if it is bubble
        if(otherBubble != null){
            if(otherBubble.fromPlayer && (!fromPlayer)){
                if(otherBubble.color == color){

                    // Start destroying bubbles
                    GetComponent<SphereNearByDetect>().destroyNearBy(transform.position, 0.2f);
                }
                // Destroy the players bubble
                Destroy(other.transform.parent.gameObject);
            }
        }

        if(other.gameObject.tag == "RightHand"){
            if(fromPlayer){
                interacted = true;

                if(rightHandRef == null){
                    rightHandRef = GameObject.Find("Right Hand Interaction Visual");
                }
                VelocityDetector velocityDetector = rightHandRef.GetComponent<VelocityDetector>();

                // Debug.Log(velocityDetector.velocity);
                if(velocityDetector.velocity > 0.25f){
                    // TODO: Set the trigger of the Animator to the burst effect
                    rb.constraints = RigidbodyConstraints.FreezePosition;
                Destroy(transform.parent.gameObject);
                }

                if((velocityDetector.velocity < 0.03f) && (!snaped)){
                    snapToObject(other.gameObject);
                }

                rb.velocity = velocityDetector.direction*moveSpeed;
            }else{
                bubbleUnbreak.Play();
            }
        }

        if(other.gameObject.tag == "LeftHand"){
            if(fromPlayer){
                interacted = true;

                if(leftHandRef == null){
                    leftHandRef = GameObject.Find("Left Hand Interaction Visual");
                }
                VelocityDetector velocityDetector = leftHandRef.GetComponent<VelocityDetector>();

                Debug.Log(velocityDetector.velocity);
                if(velocityDetector.velocity > 0.25f){
                    rb.constraints = RigidbodyConstraints.FreezePosition;
                Destroy(transform.parent.gameObject);
                }

                if((velocityDetector.velocity < 0.03f) && (!snaped)){
                    snapToObject(other.gameObject);
                }

                rb.velocity = velocityDetector.direction*moveSpeed;
            }else{
                bubbleUnbreak.Play();
            }
        }
    }

    

    // public IEnumerator destroyBubbleEffect(GameObject bubble, GameObject parent = null){
    //     // if(health >1){
    //     //     health -= 1;
    //     // }else{            
    //         bubble.transform.GetChild(2).position = bubble.transform.GetChild(0).position;
    //         bubble.transform.GetChild(1).gameObject.SetActive(false);
    //         bubble.transform.GetChild(2).gameObject.SetActive(true);
    //         playPopSound();
    //         yield return new WaitForSeconds(1.5f);
    //         if(parent != null){ 
    //             Destroy(parent);
    //         }
    //         Destroy(bubble);
    //     // }
    // }

    static public void playPopSound() {
        bubblePopSound.play_random();
    }

    void snapToObject(GameObject anchor){
        HingeJoint hj = transform.AddComponent<HingeJoint>();
        hj.connectedBody = anchor.GetComponent<Rigidbody>();
        snaped = true;

    }


    // For Editing the group
    private void OnValidate() {
        setColor(color, true);
    }

    private void OnDestroy() {
        playPopSound();
        GameObject popEffect = Instantiate(bubblePopEffect, transform.position, Quaternion.identity);
        popEffect.transform.localScale = 0.08f*Vector3.one;
        Destroy(popEffect, 1f);
        
        if(transform.parent.GetComponentInChildren<Duck>()){
            transform.parent.GetComponentInChildren<Duck>().freeDuck();
        }
    }
}
