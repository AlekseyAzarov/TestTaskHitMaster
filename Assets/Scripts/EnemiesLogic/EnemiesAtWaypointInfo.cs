using System;
using UnityEngine;

public class EnemiesAtWaypointInfo : MonoBehaviour
{
    public event Action AllEnemiesAtWaypointKilled;

    [SerializeField] private Enemy[] _enemies;

    private int _enemiesLeft;

    private void Start()
    {
        _enemiesLeft = _enemies.Length;

        foreach (Enemy enemy in _enemies)
        {
            enemy.Killed += OnEnemyKilled;
        }
    }

    private void OnEnemyKilled(Enemy killedEnemy)
    {
        killedEnemy.Killed -= OnEnemyKilled;

        _enemiesLeft--;

        if (_enemiesLeft == 0) AllEnemiesAtWaypointKilled?.Invoke();
    }
}
