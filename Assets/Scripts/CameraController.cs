using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float _cameraSpeed;
    [SerializeField] private Player _player;

    private float _currentPathPosition = 0;
    private CinemachineTrackedDolly _dolly;

    private void Start()
    {
        _dolly = GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineTrackedDolly>();
        _player.MovingToNextPoint += OnPlayerMovingToNextPoint;
    }

    private void OnPlayerMovingToNextPoint()
    {
        _currentPathPosition += 1;
    }

    private void LateUpdate()
    {
        if (_dolly.m_PathPosition < _currentPathPosition)
        {
            _dolly.m_PathPosition += _cameraSpeed * Time.deltaTime;
        }
    }

    private void OnDestroy()
    {
        _player.MovingToNextPoint -= OnPlayerMovingToNextPoint;
    }
}
