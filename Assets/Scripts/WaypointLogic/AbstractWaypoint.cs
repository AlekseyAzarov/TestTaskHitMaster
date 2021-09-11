using UnityEngine;

public abstract class AbstractWaypoint : MonoBehaviour
{
    public Transform TransformForWaypointUser { get { return _positionForWaypointUser; } }
    public AbstractWaypoint NextWaypoint { get { return _nextWaypoint; } }
    public EnemiesAtWaypointInfo EnemiesAtWaypointInfo { get { return _enemiesAtWaypointInfo; } }

    [SerializeField] private AbstractWaypoint _nextWaypoint;
    [SerializeField] private Transform _positionForWaypointUser;
    [SerializeField] private EnemiesAtWaypointInfo _enemiesAtWaypointInfo;

    protected abstract void OnTriggerEnter(Collider other);
}
