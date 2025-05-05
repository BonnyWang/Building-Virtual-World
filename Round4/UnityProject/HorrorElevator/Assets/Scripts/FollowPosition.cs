using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPosition : MonoBehaviour
{
    // This is to fix rotation is only controlled by the egg chair
    [SerializeField] public Transform target;
    [SerializeField] public Vector3 offset;
    [SerializeField] Vector3 rotationOffset;
    public bool followRotation = false;
    void FixedUpdate()
    {
        if(followRotation){
            transform.rotation = target.rotation * Quaternion.Euler(rotationOffset);
        }
        transform.position = target.position + offset;
    }
}
