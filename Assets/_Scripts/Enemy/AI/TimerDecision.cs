using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="AI/Decision/Waypoint Timer")]
public class TimerDecision : Decision
{
    public float duration;
    public override bool Decide(StateController controller)
    {
        return controller.CheckIfCountdownElapsed(duration);
    }


}
