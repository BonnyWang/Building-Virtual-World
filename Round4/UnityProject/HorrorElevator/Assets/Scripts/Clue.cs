using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clue : MonoBehaviour
{
    public static Clue instance;
    Material material;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

        material = GetComponent<Renderer>().material;

        //dissolvePaper();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void removePS(){
        Destroy(transform.GetChild(0).gameObject);
    }


    // Use Clue.instance.dissolvePaper() to call this function
    public void dissolvePaper()
    {
        StartCoroutine(dissolve());
    }


    IEnumerator dissolve(){
        float dissolve = 0;
        while(dissolve < 1){
            Debug.Log(dissolve);
            dissolve += Time.deltaTime*material.GetFloat("_speed");
            material.SetFloat("_dissolve_ratio", dissolve);
            yield return new WaitForEndOfFrame();
        }

        // removePS();
    }

}
