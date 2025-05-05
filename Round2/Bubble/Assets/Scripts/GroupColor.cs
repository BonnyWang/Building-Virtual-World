using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupColor : MonoBehaviour
{
    public int color;

    private void FixedUpdate() {

        // Debug.Log(transform.childCount);
        if(transform.childCount == 0){
            Destroy(gameObject);
        }
    }
}
