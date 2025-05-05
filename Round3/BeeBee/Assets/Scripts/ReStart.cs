using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReStart : MonoBehaviour
{
    public void restartGame(){
        Global.BeeWin = false;
        GlobalData.playerRole = -100;

        SceneManager.LoadScene("Start");
        HealthBar.beeCollected = 0;
    }
}
