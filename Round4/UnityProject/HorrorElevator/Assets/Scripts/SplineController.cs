using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Splines;
using UnityEngine.UIElements;
using static UnityEngine.Splines.SplineComponent;
public class SplineController : MonoBehaviour
{
    SplineAnimate splineAnimate;
    [SerializeField] SplineContainer[] splines;
    [SerializeField] float[] splineSpeeds;
    [SerializeField] UnityEvent[] nextEvents;
    [SerializeField] AlignAxis[] alignAxes;
    int currentSplineIndex;

    public static bool useSplineRotation = true;
    void Start()
    {
        splineAnimate = GetComponent<SplineAnimate>();
        currentSplineIndex = 0;
    }

    // Update is called once per frame
    
    private void FixedUpdate() {
        if(useSplineRotation){
            DBHandler.instance.setPlayerRotation(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        }

        // If the current spline is finished and autoNext is true, jump to the next spline
        if((!splineAnimate.IsPlaying) && (currentSplineIndex < splines.Length)){
            nextEvents[currentSplineIndex].Invoke();
        }
    }


    public void jumptToNextSpline(){
        currentSplineIndex++;
        if(currentSplineIndex < splines.Length){
            splineAnimate.Container  = splines[currentSplineIndex];
            splineAnimate.MaxSpeed = splineSpeeds[currentSplineIndex];
            splineAnimate.ObjectForwardAxis = alignAxes[currentSplineIndex];
            //splineAnimate.MaxSpeed = splineSpeeds[currentSplineIndex];
            splineAnimate.Restart(true);
        }

    }
}
