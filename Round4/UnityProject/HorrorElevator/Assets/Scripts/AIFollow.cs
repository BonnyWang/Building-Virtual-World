using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AIFollow : MonoBehaviour
{
    NavMeshAgent agent;
    public Transform target;
    void Start()
    {   
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(target.position, transform.position) > 2f){
            agent.SetDestination(target.position);
            DBHandler.instance.GhostRef.Child("Position").Child("x").SetValueAsync(transform.position.x);
        }
        
    }
}
