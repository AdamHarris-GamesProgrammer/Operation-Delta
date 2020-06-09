using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;
using UnityEngine.AI;

public class StateController : MonoBehaviour
{
    public State currentState;
    public EnemyStats stats;
    public Transform eyes;
    public State remainState;

    //TODO: Incorporate game manager and enemy manager classes
    [HideInInspector] public NavMeshAgent agent;
    public List<Transform> waypoints;
    public int nextWaypoint;
    public Transform target;
    public float stateTimeElapsed = 0.0f;

    private bool isAiActive;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void OnDrawGizmos()
    {
        if(currentState != null && eyes !=null){
            Gizmos.color = currentState.sceneGizmoColor;
            Gizmos.DrawWireSphere(eyes.position, stats.lookSphereCastRadius);
        }
    }

    public void TransitionToState(State nextState)
    {
        if(nextState != remainState)
        {
            currentState = nextState;
            OnExitState();
        }
    }

    public bool CheckIfCountdownElapsed(float duration)
    {
        stateTimeElapsed += Time.deltaTime;
        return (stateTimeElapsed >= duration);
    }


    public void OnExitState()
    {
        stateTimeElapsed = 0.0f;
    }
}
