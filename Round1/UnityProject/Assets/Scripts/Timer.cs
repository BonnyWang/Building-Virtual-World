using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class Timer : MonoBehaviour
{
    public static float timeRemaining = 60;



    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            GetComponent<TextMeshPro>().text = timeRemaining.ToString("F0");

        } else if (timeRemaining <= 0 && LevelManager.winning == false)
        {   
            SceneManager.LoadScene("Fail");
        }
    }
}
