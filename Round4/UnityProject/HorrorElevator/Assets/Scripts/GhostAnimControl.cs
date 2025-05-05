using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GhostAnimControl : MonoBehaviour
{
    Animator animator;
    [SerializeField] SplineController splineController;
    [SerializeField] AudioClip MonsterSFX;
    [SerializeField] AudioClip MonsterTALK;
    AudioSource audioSource;
    FollowPosition followPosition;
    int attacked = 0;

    public static bool catchedPlayer;

    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        followPosition = GetComponent<FollowPosition>();

        catchedPlayer = false;
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player"){
            animator.SetTrigger("Start");
        }
    }

    void splineNext(){
        if(attacked < 3){
            splineController.jumptToNextSpline();
            attacked += 1;
        }

    }

    void activeLight(){
        transform.parent.GetChild(2).gameObject.SetActive(true);
    }

    void moveZ(){
        transform.position += new Vector3(0, 0, 7f);
    }

    void startFollow(){
        GetComponent<FollowPosition>().enabled = true;
    }

    void playSFX(){
        audioSource.PlayOneShot(MonsterSFX);
    }

    public void catchPlayer(){
        followPosition.offset = new Vector3(-0.9f, -2f, 0);
        animator.SetTrigger("Catch");
        StartCoroutine(catchDuration());

    }

    public void playTalkSound(){
        int randomNumber = Random.Range(0, 3);
        if(randomNumber > 1){
            audioSource.PlayOneShot(MonsterTALK);
        }
    }

    IEnumerator catchDuration(){
        yield return new WaitForSeconds(3f);
        // yield return new WaitForSeconds(5f);
        // if(catchedPlayer == false){
            // SceneManager.LoadScene("YouDie");
        // }
    }
}
