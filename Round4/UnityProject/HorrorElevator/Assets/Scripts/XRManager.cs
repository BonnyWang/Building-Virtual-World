using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using System;
// using Unity.VisualScripting;
// using UnityEditor.XR.Interaction.Toolkit;

public class XRManager : MonoBehaviour
{
    public static XRManager instance;
    [SerializeField] private ElevatorController elevator;

    #region Object Reference

    [Header("Controller Model Reference")]
    [SerializeField] private XRBaseController leftController;
    [SerializeField] GameObject leftControllerCandle;
    /*
     Interactable Objects:
      - Letter
      - Key

     Sockets:
      - Key Socket: Open Last Door

     Scenes:
      - Lobby: (1st Floor) Render in Start
      - Basement: (-1st Floor) Render after letter pickup and elevator went down
      - Auditorium: (2nd Floor) Render after key pickedup and elevator went up
    */
    [Header("Interactables - Candle")]
    [SerializeField] private GameObject candle;

    [Header("Interactables - Letter")]
    [SerializeField] private GameObject letter;
    private LetterController letterController;

    [Header("Interactables - Radio")]
    [SerializeField] private GameObject radio;
    [SerializeField] Rigidbody CDRb;
    private RadioController radioController;


    [Header("Sockets")]
    [SerializeField] private XRSocketInteractor doorLock;

    [Header("Spline")]
    public SplineController splineController;

    [Header("Ending")]
    [SerializeField] private GameObject endingPlane;
    [SerializeField] Material goodLuck;
    [SerializeField] Material credit;

    bool letterPicked;
    bool secondPressed;

    #endregion

    public void OnCandlePickedUp()
    {
        //leftController.model = candle.transform;
        //leftController.modelPrefab = candle.transform;

        candle.GetComponent<FollowPosition>().enabled = true;
        candle.GetComponent<XRGrabInteractable>().enabled = false;
        candle.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        candle.GetComponent<Rigidbody>().useGravity = false;

        splineController.jumptToNextSpline();
        // Next Spline
        Debug.Log("Next Spline");

        elevator.doorClose();
    }

    public void OnLetterPickedUp()
    {
        if(letterPicked){
            return;
        }
        letterPicked = true;
        GameManager.Instance.letterPicked = true;

        StartCoroutine(letterController.LetterReadThrough());

        // Elevator Open (Guide)
        // Animation
        //elevator.doorOpen();

        // Next Spline
        Debug.Log("Next Spline");
        splineController.jumptToNextSpline();

        elevator.doorOpen();
        
    }

    public void OnLetterDropped()
    {

    }

    public void OnSecondFloorBtnPushed()
    {
        if(secondPressed){
            return;
        }
        secondPressed = true;
        elevator.doorClose();
        elevator.elevatorWork(2);
        /*
        if (finishSignal == true)
        {
            Debug.Log(finishSignal);
            Debug.Log("Next Spline");
            splineController.jumptToNextSpline();
        }
        */

    }
    public void OnCDPickedUp()
    {

        if(GameManager.Instance.CDPicked == true){
            return;
        }
        
        GameManager.Instance.CDPicked = true;

        CDRb.constraints  = RigidbodyConstraints.FreezeAll;
        CDRb.useGravity  = false;
        if (GameManager.Instance.diskSocketed == false)
        {
            splineController.jumptToNextSpline();
            // OnDiskSocketed();
        }



        // Lighting?

        // Elevator Open (Guide)
        // Animation

    }

    public void OnDiskSocketed()
    {
        GameManager.Instance.diskSocketed = true;
        //SoundManager.instance.PlayDiskSocketedSound();
        Debug.Log("Disk Socketed");
        radioController.radioWork();



        

        // Lighting?

        // Elevator Open (Guide)
        // Animation


    }

    public void OnDiskPickedUp()
    {
        GameManager.Instance.diskSocketed = false;
        SoundManager.instance.StopDiskSocketedSound();


        Debug.Log("Disk UnSocketed");
        // Lighting?

        // Elevator Open (Guide)
        // Animation


    }

    public void OnHandlePulled()
    {
        if (GameManager.Instance.success == true)
        {
            endingPlane.GetComponent<MeshRenderer>().material = credit;
        }

        else
        {
            endingPlane.GetComponent<MeshRenderer>().material = goodLuck;
        }
    }





    #region Unity Routines

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        letterController = letter.GetComponent<LetterController>();
        radioController = radio.GetComponent<RadioController>();

        letterPicked = false;
        secondPressed = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #endregion 

}
