using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialGroup : MonoBehaviour
{
    [SerializeField] GameObject mainLevel;
    bool transitioning;
    void Start()
    {
        transitioning = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(checkGroupIsEmpty()){
            mainLevel.SetActive(true);
            transform.parent.GetChild(3).localScale += 0.01f*Vector3.one;
            if(!transitioning){
                StartCoroutine(toMainLevel());
            }
        }
    }

    bool checkGroupIsEmpty(){
        foreach(Transform child in transform){
            if(child.childCount !=0){
                return false;
            }
        }

        return true;
    }
    IEnumerator toMainLevel(){
        Player.level = 1;
        transform.parent.GetChild(4).gameObject.SetActive(false);
        yield return new WaitForSeconds(10f);
        transform.parent.gameObject.SetActive(false);
    }
}
