using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
public class AICharacterController : MonoBehaviour {


    [HideInInspector] public NavMeshAgent navMeshAgent;


    private void Awake()
    {
        navMeshAgent = this.transform.GetComponent<NavMeshAgent>();
    }

    public void setFixedDestination(Vector3 destination)
    {
        navMeshAgent.destination = destination;
    }

    public void setMovingDestination(GameObject gameobject)
    {
        setMovingDestination(gameobject.transform.position);
    }

    public void setMovingDestination(Transform transform)
    {
        setMovingDestination(transform.position);
    }

    public void setMovingDestination(Vector3 destination)
    {

    }
}
