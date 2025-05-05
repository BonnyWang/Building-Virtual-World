using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    public GameObject beeWinsImage;
    public GameObject beekeeperWinsImage;
    // Start is called before the first frame update
    void Start()
    {
        
        beeWinsImage.SetActive(Global.BeeWin);
        beekeeperWinsImage.SetActive(!Global.BeeWin);

        if (Global.BeeWin)
        {
            SoundManager.instance.PlayWinSound();
        }
        else
        {
            SoundManager.instance.PlayDieSound();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
