using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Actions/Patrol")]
public class PatrolAction : Action
{
    public override void Act(StateController controller)
    {
        controller.agent.destination = controller.waypoints[controller.nextWaypoint].position;
        controller.agent.isStopped = false;

        if(controller.agent.remainingDistance <= controller.agent.stoppingDistance && !controller.agent.pathPending)
        {
            //Stops the next way point from going beyond the waypoints array
            controller.nextWaypoint = (controller.nextWaypoint + 1) % controller.waypoints.Count;
        }
    }
}
