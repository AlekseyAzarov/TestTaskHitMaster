using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

[RequireComponent(typeof(WaypointInteractionProvider), typeof(NavMeshAgent))]
public class Player : MonoBehaviour
{
    public event Action MovingToNextPoint;
    public event Action<TipType> UpdateUI;

    [SerializeField] private AbstractWaypoint _startWaypoint;
    [SerializeField] private Transform _bulletOriginTransform;
    [SerializeField] private LayerMask _layersToIgnoreOnRaycast;

    private WaypointInteractionProvider _waypointProvider;
    private NavMeshAgent _navMeshAgent;
    private Transform _nextWaypointTransform;
    private Transform _currentWaypointTransform;
    private AbstractPlayerStates _currentState;
    private EnemiesAtWaypointInfo _enemiesInfo;
    private Animator _animator;
    private Pistol _pistol;
    private List<AbstractPlayerStates> _allStates;

    private void Start()
    {
        _waypointProvider = GetComponent<WaypointInteractionProvider>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _pistol = GetComponent<Pistol>();
        _animator = GetComponent<Animator>();
        _currentWaypointTransform = _startWaypoint.TransformForWaypointUser;
        transform.rotation = _currentWaypointTransform.rotation;

        _allStates = new List<AbstractPlayerStates>
        {
            new ReadyMoveNextPointState(),
            new ReadyToKillEnemiesState(),
            new LevelEndReachedState(),
        };
        _currentState = _allStates[0];

        _waypointProvider.IntermediateWaypointReached += OnIntermediateWaypointReached;
        _waypointProvider.FinalWaypointReached += OnFinalWaypointReached;
    }

    public void OnTapDetected(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (_currentState.ReadyToMove())
            {
                _navMeshAgent.destination = _nextWaypointTransform.position;
                _animator.SetInteger("AnimatorState", 1);
                MovingToNextPoint?.Invoke();
            }
            else if (_currentState.ReadyToKillEnemies())
            {
                _animator.SetTrigger("Shoot");
                Vector2 touchPosition = context.ReadValue<Vector2>();
                Ray ray = Camera.main.ScreenPointToRay(touchPosition);
                if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, ~_layersToIgnoreOnRaycast))
                {
                    _pistol.Shoot(_bulletOriginTransform.position, hit.point);
                    Quaternion rotationForPlayer = Quaternion.LookRotation(hit.point - transform.position, Vector3.up);
                    transform.rotation = Quaternion.Euler(0, rotationForPlayer.eulerAngles.y, 0);
                }
                _animator.SetInteger("AnimatorState", 0);
            }
            else if (_currentState.LevelEndReached())
            {
                SceneController.Instance.RestartScene();
            }
        }
    }

    private void OnIntermediateWaypointReached(AbstractWaypoint nextWaypoint,
        Transform currentWaypointTransform, EnemiesAtWaypointInfo enemiesInfo)
    {
        SwitchState<ReadyToKillEnemiesState>();
        _currentWaypointTransform = currentWaypointTransform;
        _enemiesInfo = enemiesInfo;
        _enemiesInfo.AllEnemiesAtWaypointKilled += OnAllEnemiesAtWaypointKilled;
        _nextWaypointTransform = nextWaypoint.TransformForWaypointUser;
        _animator.SetInteger("AnimatorState", 0);
        UpdateUI?.Invoke(TipType.Shoot);
    }

    private void OnFinalWaypointReached()
    {
        SwitchState<LevelEndReachedState>();
        _animator.SetInteger("AnimatorState", 3);
        UpdateUI?.Invoke(TipType.LevelEnd);
    }

    private void OnAllEnemiesAtWaypointKilled()
    {
        SwitchState<ReadyMoveNextPointState>();
        _enemiesInfo.AllEnemiesAtWaypointKilled -= OnAllEnemiesAtWaypointKilled;
        UpdateUI?.Invoke(TipType.Run);
    }

    private void SwitchState<T>() where T : AbstractPlayerStates
    {
        var state = _allStates.Find(s => s is T);
        _currentState = state;
    }

    private void OnDestroy()
    {
        _waypointProvider.IntermediateWaypointReached -= OnIntermediateWaypointReached;
        _waypointProvider.FinalWaypointReached -= OnFinalWaypointReached;
    }
}