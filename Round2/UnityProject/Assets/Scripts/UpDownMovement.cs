using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDownMovement : MonoBehaviour
{
    Vector3 downPosition;
    Vector3 upPosition;
    [SerializeField] bool direction;
    [SerializeField] float distance = 1f;
    [SerializeField] float speed = 1f;
    void Start()
    {
        direction = true;
        // true for up and false for down

        downPosition = transform.position + distance*Vector3.down;
        upPosition = transform.position + distance*Vector3.up;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(direction){
            transform.position = Vector3.MoveTowards(transform.position, upPosition, speed);
            if(transform.position.y >= (upPosition.y - distance*0.1f)){
                direction = false;
            }
        }else{
            transform.position = Vector3.MoveTowards(transform.position, downPosition, speed);
            if(transform.position.y <= (downPosition.y + distance*0.1f)){

                direction = true;
            }
        }
    }
}
