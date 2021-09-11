using System;
using UnityEngine;

public class WaypointInteractionProvider : MonoBehaviour
{
    public event Action<AbstractWaypoint, Transform, EnemiesAtWaypointInfo> IntermediateWaypointReached;
    public event Action FinalWaypointReached;

    public void UserReachedWaypoint(AbstractWaypoint reachedWaypoint)
    {
        if (reachedWaypoint is IntermediateWaypoint)
        {
            IntermediateWaypointReached?.Invoke(reachedWaypoint.NextWaypoint,
                reachedWaypoint.TransformForWaypointUser,
                reachedWaypoint.EnemiesAtWaypointInfo);
        }
        else if (reachedWaypoint is FinalWaypoint)
        {
            FinalWaypointReached?.Invoke();
        }
    }
}
