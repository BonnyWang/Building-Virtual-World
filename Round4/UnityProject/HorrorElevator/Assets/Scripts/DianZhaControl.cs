using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class DianZhaControl : MonoBehaviour
{
    [SerializeField] Animator baseAnimator;
    [SerializeField] SplineController splineController;
    [SerializeField] GameObject Monster;
    [SerializeField] bool isClose = false;

    PlayableDirector director;

    private void Start()
    {
        director = GetComponentInChildren<PlayableDirector>();
    }


    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "DianZha"){
            if(!isClose){
                GhostAnimControl.catchedPlayer = true;
                Debug.Log("Door Open");
                baseAnimator.SetTrigger("Open");
                Monster.GetComponentInChildren<FollowPosition>().enabled = false;
                // Monster.transform.position = new Vector3(-0.2588f,0,-0.0427000001f); 
                splineController.jumptToNextSpline();
            }else{
                Debug.Log("Door Close");
                baseAnimator.SetTrigger("Close");
                director.Play();
                // splineController.jumptToNextSpline();
                StartCoroutine(toGoodLuck());
                // TODO: show the 
            }
            
        }
    }


    IEnumerator toGoodLuck(){
        yield return new WaitForSeconds(6f);
        DBHandler.instance.setPlayerShake(0);
        SceneManager.LoadScene("GoodLuck");
    }
}
