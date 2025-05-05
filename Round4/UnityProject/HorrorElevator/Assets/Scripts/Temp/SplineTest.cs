using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.XR.OpenXR.Input;

public class SplineTest : MonoBehaviour
{
    [SerializeField] InputActionReference inputActionReference;
    [SerializeField] UnityEvent clickEvent;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(inputActionReference.action.triggered){
            clickEvent.Invoke();
        }
    }
}
