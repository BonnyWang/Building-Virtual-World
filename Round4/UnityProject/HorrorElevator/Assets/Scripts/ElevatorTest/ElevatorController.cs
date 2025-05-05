using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorController : MonoBehaviour
{

    // Buttons?
    // Animations: door, pointer
    // Sound: dooropen/close, start, running, stop

    // Logic: animdoor(boolean check)-> goInClose(auto?/button?)-> sound & pointerAnim (Change scene or disable gameobj)-> animdoor

    // Start is called before the first frame update

    [Header("SCENES")]
    [SerializeField] private GameObject lobbyScene;
    [SerializeField] private GameObject basementScene;
    [SerializeField] private GameObject auditoriumScene;

    [SerializeField] private Animator doorAnimator;
    [SerializeField] private Animator pointerAnimator;
    [SerializeField] SplineController Floor2SplineController;
    [SerializeField] SplineController Floor3SplineController;
    [SerializeField] Transform wheelChair;

    private int doorCloseLength = 4;

    bool Operating;
    [SerializeField] bool testing;
    bool transitBase;
    void Start()
    {
        if(!testing){
        
            Operating = false;
            lobbyScene.SetActive(true);
            auditoriumScene.SetActive(false);
            basementScene.SetActive(false);

            Floor2SplineController.gameObject.SetActive(false);
            Floor3SplineController.gameObject.SetActive(false);
        }
        
        transitBase = false;
        doorOpen();
    }

    // Update is called once per frame
    void Update()
    {
        //if(GameManager.Instance.letterPicked) { doorOpen(); }
    }

    public void doorOpen()
    {
        //Animation
        //doorAnimator.SetTrigger("DoorOpen");
        doorAnimator.SetBool("DoorOpen", true);
        Debug.Log("Door Opened");
        //door.SetActive(false);

        //Sound
        SoundManager.instance.PlayElevatorDoorOpenSound();

    }

    public void doorClose()
    {
        // if player inside elevator/push button? -> close door

        //Animation
        //doorAnimator.SetTrigger("DoorClose");
        doorAnimator.SetBool("DoorOpen", false);
        Debug.Log("Door Closed");
        //door.SetActive(true);

        //Sound
        SoundManager.instance.PlayElevatorDoorCloseSound();

    }

    public void elevatorWork(int level)
    {
        StartCoroutine(ElevatorOperating(level, OnOperateComplete));

    }

    IEnumerator ElevatorOperating(int levelSelected, System.Action<int,bool> callbackOnFinish)
    {
        if(Operating == true){
           yield break;
        }
        Operating = true;
        yield return new WaitForSeconds(doorCloseLength);
        
        // Play Elevator Operating Sound
        SoundManager.instance.PlayElevatorOperatingSound();
        DBHandler.instance.setPlayerShake(20);

        Debug.Log("ElevatorOperating");

        yield return new WaitForSeconds(10);

        if ((GameManager.Instance.letterPicked == true) && (levelSelected == 2))
        {
            lobbyScene.SetActive(false);
            auditoriumScene.SetActive(true);
        }

        if(levelSelected == 3){
            auditoriumScene.SetActive(false);
            basementScene.SetActive(true);
        }

        Debug.Log("ElevatorOperatingDone");
        Debug.Log("SceneChanged");
        yield return new WaitForSeconds(8);

        SoundManager.instance.StopElevatorOperatingSound();
        DBHandler.instance.setPlayerShake(0);
        doorOpen();

        yield return new WaitForSeconds(3);

        // Set finish bool
        Operating = false;
        callbackOnFinish(levelSelected,true);

    }

    public void OnOperateComplete( int level,bool completed)
    {
         Debug.Log("SceneChanged Completed?: " + completed);

        if(level == 2){
            Floor2SplineController.gameObject.SetActive(true);
            XRManager.instance.splineController = Floor2SplineController;
            Camera.main.transform.parent.GetComponent<FollowPosition>().target= Floor2SplineController.transform;
            wheelChair.GetComponent<FollowPosition>().target= Floor2SplineController.transform;
            SoundManager.instance.PlayBgm();
        }

        if(level ==3){
            Floor2SplineController.gameObject.SetActive(false);
            Floor3SplineController.gameObject.SetActive(true);
            XRManager.instance.splineController = Floor3SplineController;
            Camera.main.transform.parent.GetComponent<FollowPosition>().target= Floor3SplineController.transform;
            wheelChair.GetComponent<FollowPosition>().target= Floor3SplineController.transform;

            DBHandler.instance.setPlayerShake(20);
            SoundManager.instance.playBaseBgm();
        }
        
    }

    public void ToBasement(){
        RenderSettings.fogDensity = 0.03f;
        if(transitBase){
            return;
        }
        transitBase = true;
        doorClose();
        // SoundManager.instance.playBaseBgm();
        elevatorWork(3);
        StartCoroutine(toBaseShake());
        
    }

    IEnumerator toBaseShake(){
        yield return new WaitForSeconds(5f);
        SplineController.useSplineRotation = false;
        yield return new WaitForSeconds(1f);
        DBHandler.instance.setPlayerRotation(380f,170f,340.623f);
        yield return new WaitForSeconds(0.5f);
        DBHandler.instance.setPlayerRotation(340,170,359.623f);
        DBHandler.instance.setPlayerRotation(380,170,340.623f);
        yield return new WaitForSeconds(1f);
        DBHandler.instance.setPlayerRotation(340,170,380.623f);
        yield return new WaitForSeconds(0.5f);
        DBHandler.instance.setPlayerRotation(356.1199f,170,359.623f);
        yield return new WaitForSeconds(0.2f);
        SplineController.useSplineRotation = true;
    }


}
