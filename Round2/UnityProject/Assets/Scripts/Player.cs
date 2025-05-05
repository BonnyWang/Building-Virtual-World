using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    AudioSource drawnAudio;
    AudioSource BGM;

    static public int level;

    [SerializeField] AudioSource NormalBGM;
    [SerializeField] AudioSource IntroBGM;
    [SerializeField] AudioSource DrawnMain;
    [SerializeField] AudioSource DrawnLevel0;

    public static Player instance;


    private void Start() {
        level = 0;
    }

    private void OnTriggerEnter(Collider other) {
        if(level == 0){
            drawnAudio = DrawnLevel0;
            BGM = IntroBGM;

        }else{
            drawnAudio = DrawnMain;
            BGM = NormalBGM;
        }

        if(other.gameObject.tag == "Bubble"){

            
            Bubble otherBubble = other.gameObject.GetComponent<Bubble>();
            if((otherBubble != null) && (!otherBubble.fromPlayer)){
                drawnPlayer();
            //    StartCoroutine(loadFail());
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.gameObject.tag == "Bubble"){
            Bubble otherBubble = other.gameObject.GetComponent<Bubble>();
            if((otherBubble != null) && (!otherBubble.fromPlayer)){
                unDrawnPlayer();
            }
        }
        
    }

    public void drawnPlayer(){
        drawnAudio.volume = 0.8f;
        BGM.volume = 0f;
        transform.GetChild(0).gameObject.SetActive(true);
    }

    public void unDrawnPlayer(){
        drawnAudio.volume = 0f;
        BGM.volume = 0.8f;
        transform.GetChild(0).gameObject.SetActive(false);
    }


    IEnumerator loadFail(){
        transform.GetChild(0).gameObject.SetActive(true);
        drawnAudio.Play();
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("Fail");
    }
}
