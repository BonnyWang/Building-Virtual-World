using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(transform.GetChild(0).childCount);
        if(transform.GetChild(0).childCount == 0){
            StartCoroutine(LoadScene());
        }
    }

    IEnumerator LoadScene(){
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("Main");
    }
}
