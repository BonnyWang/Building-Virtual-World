using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class VelocityDetector : MonoBehaviour
{
    public InputActionProperty Input_Ref;
    public int recordLength;
    public GameObject velocityVisualizer;
    Vector3 currentPosition;
    Vector3 prevPosition;

    float[] distanceCache;
    int cachePointer;

    public float velocity;
    public Vector3 direction;
    public Vector3 velocityVector;
    
    void Start()
    {
        currentPosition = Vector3.zero;
        prevPosition = Vector3.zero;
        distanceCache = new float[recordLength];
        cachePointer = 0;

        velocity = 0f;
        direction = Vector3.zero;
        velocityVector = Vector3.zero;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Input_Ref.action != null){
            currentPosition =  Input_Ref.action.ReadValue<Vector3>();
        }else{
            currentPosition = transform.position;
        }

        distanceCache[cachePointer] = Vector3.Distance(currentPosition, prevPosition);

        addCachePointer();

        velocity = distanceCache.Sum();
        direction = (currentPosition - prevPosition).normalized;
        velocityVector = direction * velocity;

        
        
        if(velocityVisualizer != null){
           velocityVisualizer.transform.localScale = Vector3.one*velocity*0.5f;
        }

        prevPosition = currentPosition;
    }

    void addCachePointer(){
        cachePointer++;
        if(cachePointer >= distanceCache.Length){
            cachePointer = 0;
        }
    }
}
