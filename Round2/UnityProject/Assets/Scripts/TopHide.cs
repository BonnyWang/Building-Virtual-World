using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopHide : MonoBehaviour
{
    // Start is called before the first frame update

    GameObject meshRendere;
    Vector3 showHight;

    bool isShow;

    Vector3 originalPos;
    bool beginingAnimation;
    void Start()
    {
        meshRendere = transform.GetChild(1).gameObject;
        showHight = transform.parent.parent.position;

        isShow = false;
        beginingAnimation = true;

        if(transform.position.y <= showHight.y){
            isShow = true;
        }else{
            transform.localScale = 1f*Vector3.one;
            meshRendere.SetActive(false);
        }

        originalPos =  transform.position;

        if(transform.childCount > 2){
            // This group include the duck
            transform.position = new Vector3(transform.position.x,transform.position.y-10f,transform.position.z);
        }else{
            // show from the top
            transform.position = new Vector3(transform.position.x,transform.position.y+10f,transform.position.z);

        }

        Player.instance.unDrawnPlayer();
        
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if(((transform.position - originalPos).magnitude > 0.01f) && beginingAnimation){
            transform.position = Vector3.Lerp(transform.position,originalPos,0.01f);
            // Debug.Log("Begining Animation");
            foreach(Collider collider in GetComponentsInChildren<Collider>()){
                collider.enabled = false;
            }
        }else{
            beginingAnimation = false;
            TopDown.startMovig = true;

            foreach(Collider collider in GetComponentsInChildren<Collider>()){
                collider.enabled = true;
            }
        }

        if(transform.position.y < showHight.y && !beginingAnimation){
            transform.localScale = Vector3.Lerp(transform.localScale,Vector3.one*15f,0.05f);
            if(!isShow){
                isShow = true;
                meshRendere.SetActive(true);
            }
        }
    }
}
