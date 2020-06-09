using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="AI/Actions/Stationary Patrol")]
public class StationaryPatrolAction : Action
{
    float timer = 0.0f;
    public override void Act(StateController controller)
    {
        
        controller.agent.destination = controller.waypoints[controller.nextWaypoint].position;
        controller.agent.isStopped = false;

        if (controller.agent.remainingDistance <= controller.agent.stoppingDistance && !controller.agent.pathPending)
        {
            timer += Time.deltaTime;

            if (timer >= controller.stats.waitAtWaypointTimer)
            {
                timer = 0.0f;
                //Stops the next way point from going beyond the waypoints array
                controller.nextWaypoint = (controller.nextWaypoint + 1) % controller.waypoints.Count;
            }
            else
            {
                controller.agent.isStopped = true;
            }
        }
    }
}
