using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.OpenXR.Input;

public class PlayerBoss : MonoBehaviour
{
    public GameObject HurtEffect;

    public int record_Strength;

    public InputActionProperty Left_HandPosition_Ref;
    public InputActionProperty Right_HandPosition_Ref;

    Vector3 LeftHand_Position;
    Vector3 RightHand_Position;
    Vector3 LeftHand_PrevPosition;
    Vector3 RightHand_PrevPosition;

    float[] left_DistanceMoved_Cache;
    float[] right_DistanceMoved_Cache;
    int cachePointer;

    public float left_Velocity;
    public float right_Velocity;

    Transform leftHandVisualizer;
    Transform rightHandVisualizer;

    bool canHurt;
    
    void Start()
    {
        LeftHand_Position = Vector3.zero;
        RightHand_Position = Vector3.zero;
        LeftHand_PrevPosition = Vector3.zero;
        RightHand_PrevPosition = Vector3.zero;
        left_DistanceMoved_Cache = new float[5];
        right_DistanceMoved_Cache = new float[5];
        cachePointer = 0;

        left_Velocity = 0;
        right_Velocity = 0;

        canHurt = true;

        
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(Left_HandPosition_Ref.action.ReadValue<Vector3>().ToString() + " " + Right_HandPosition_Ref.action.ReadValue<Vector3>().ToString());
        LeftHand_Position = Left_HandPosition_Ref.action.ReadValue<Vector3>();
        RightHand_Position = Right_HandPosition_Ref.action.ReadValue<Vector3>();

        left_DistanceMoved_Cache[cachePointer] = Vector3.Distance(LeftHand_Position, LeftHand_PrevPosition);
        right_DistanceMoved_Cache[cachePointer] = Vector3.Distance(RightHand_Position, RightHand_PrevPosition);
        // Debug.Log(left_DistanceMoved_Cache);

        addCachePointer();

        left_Velocity = left_DistanceMoved_Cache.Sum();
        right_Velocity = right_DistanceMoved_Cache.Sum();

        
        
        if(GameObject.Find("Left Velocity Visualizer") != null){
            leftHandVisualizer = GameObject.Find("Left Velocity Visualizer").transform;
            leftHandVisualizer.localScale = new Vector3(left_Velocity, left_Velocity, left_Velocity)*0.5f;
        }
        if(GameObject.Find("Right Velocity Visualizer") != null){
            rightHandVisualizer = GameObject.Find("Right Velocity Visualizer").transform;
            rightHandVisualizer.localScale = new Vector3(right_Velocity, right_Velocity, right_Velocity)*0.5f;
        }

        // Debug.Log("Left Velocity: " + left_Velocity.ToString() + " Right Velocity: " + right_Velocity.ToString());

        LeftHand_PrevPosition = LeftHand_Position;
        RightHand_PrevPosition = RightHand_Position;

        
    }

    void addCachePointer(){
        cachePointer++;
        if(cachePointer >= left_DistanceMoved_Cache.Length){
            cachePointer = 0;
        }
    }

    private void OnTriggerEnter(Collider other) {
            
        if (other.gameObject.tag == "Boss" && canHurt){
            Debug.Log("Player Hurt by the Boss");
            Timer.timeRemaining -= 5f;
            StartCoroutine(showHurtEffect());

            canHurt = false;
            StartCoroutine(reactivateHurt());

            // SceneManager.LoadScene("Boss");
        }
    }

    IEnumerator reactivateHurt(){
        yield return new WaitForSeconds(5f);
        canHurt = true;
    }

    IEnumerator showHurtEffect() {
        HurtEffect.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        HurtEffect.SetActive(false);
    }
}
