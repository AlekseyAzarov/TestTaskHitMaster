using UnityEngine;

public class FinalWaypoint : AbstractWaypoint
{
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out WaypointInteractionProvider provider))
        {
            provider.UserReachedWaypoint(this);
        }
    }
}
