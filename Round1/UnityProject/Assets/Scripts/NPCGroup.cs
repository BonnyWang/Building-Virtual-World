using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCGroup : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject lanternFlies;
    public GameObject lanternFliesPlay;
    bool activated;

    public GameObject handTimer;
    void Start()
    {
        activated = false;
        lanternFliesPlay.SetActive(false);
        StartCoroutine(GameObject.Find("StartDialogue").GetComponent<PlaySequence>().play());
        activated = true;


        handTimer.SetActive(false);
        StartCoroutine(showLanternFlies());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // private void OnTriggerEnter(Collider other) {
        
    //     if ((other.gameObject.CompareTag("Player")) && (!activated)) {
            
    //     }
    // }

    IEnumerator showLanternFlies() {
        yield return new WaitForSeconds(7f);
        lanternFlies.transform.position = new Vector3(0f,0.5f,0.209f);
        LanternFly.moveSpeed = 0.4f;
        lanternFliesPlay.SetActive(true);  
        handTimer.SetActive(true);

    }
}
