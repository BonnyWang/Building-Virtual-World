using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioController : MonoBehaviour
{
    [SerializeField] private int waitLength;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void radioWork()
    {
        Debug.Log("Radio Start Work");
        StartCoroutine(RadioPlayThrough(OnRadioOperateComplete));

    }

    public IEnumerator RadioPlayThrough(System.Action<bool> callbackOnFinish)
    {
        SoundManager.instance.PlayDiskSocketedSound();

        Debug.Log("Start Radio Wait");
        yield return new WaitForSeconds(waitLength);

        Debug.Log("End Radio Wait");

        callbackOnFinish(true);

    }

    public void OnRadioOperateComplete(bool completed)
    {
        Debug.Log("SceneChanged Completed?: " + completed);
        if (completed == true)
        {
            XRManager.instance.splineController.jumptToNextSpline();
        }
        SoundManager.instance.StopDiskSocketedSound();
    }

}
