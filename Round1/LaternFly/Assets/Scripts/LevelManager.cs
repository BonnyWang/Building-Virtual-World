using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject level1;
    public GameObject level2;
    GameObject XROrigin;

    public int currentLevel;
    public static bool winning;
    void Start()
    {
        XROrigin = GameObject.Find("XR Origin (XR Rig)");
        level1.SetActive(true);
        level2.SetActive(false);

        winning = false;

        currentLevel = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void level1To2() {
        level1.SetActive(false);
        level2.SetActive(true);
        StartCoroutine(moveToLevel2());
        currentLevel = 2;
        
        GameObject.Find("Gameplay").transform.position = new Vector3(0f,0.62f,0f);
        GameObject.Find("World").transform.position = new Vector3(0f,-0.5f,-20.3999996f);
    }

    IEnumerator moveToLevel2(){
        while(XROrigin.transform.position != new Vector3(0, 0, 18)){
            XROrigin.transform.position = Vector3.Lerp(XROrigin.transform.position, new Vector3(0, 0, 18), 0.1f);
            yield return null;
        }

        
    }
}
