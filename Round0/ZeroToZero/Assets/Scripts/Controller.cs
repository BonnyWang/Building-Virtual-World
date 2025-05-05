using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    // Start is called before the first frame update
    public float moveSpeed = 5f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();  
    }

    void Move(){

        float inputH = Input.GetAxis("Horizontal");
        float inputV = Input.GetAxis("Vertical");

        Vector3 moveDir = new Vector3(inputH, inputV, 0);
        Vector3 movement = moveDir * moveSpeed * Time.deltaTime;

        if( (Mathf.Abs(inputH) > 0) || (Mathf.Abs(inputV) > 0) ){
            transform.Translate(movement);

            GetComponentInChildren<Animator>().SetBool("Move", true);
            if(GameObject.Find("MoveSound") != null){
                if(GameObject.Find("MoveSound").GetComponent<AudioSource>().isPlaying == false){
                    GameObject.Find("MoveSound").GetComponent<AudioSource>().Play();
                }
            }
        }else{
            GetComponentInChildren<Animator>().SetBool("Move", false);

            if(GameObject.Find("MoveSound") != null){
                GameObject.Find("MoveSound").GetComponent<AudioSource>().Stop();
            }
        }
    }


}
