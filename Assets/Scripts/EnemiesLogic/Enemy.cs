using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(RagdollHandler))]
public class Enemy : MonoBehaviour, IDamageable
{
    public event Action<Enemy> Killed;

    [SerializeField] private TextMeshPro _healthPointsText;

    private RagdollHandler _ragdoll;
    private BoxCollider _collider;
    private int _healthPoints;

    private void Start()
    {
        _healthPoints = UnityEngine.Random.RandomRange(2, 6);
        _healthPointsText.text = _healthPoints.ToString();

        _ragdoll = GetComponent<RagdollHandler>();
        _collider = GetComponent<BoxCollider>();
    }

    public void TakeDamage()
    {
        _healthPoints -= 1;
        _healthPointsText.text = _healthPoints.ToString();

        if (_healthPoints == 0)
        {
            Killed?.Invoke(this);
            _collider.enabled = false;
            _ragdoll.EnableeRagdoll();
            _healthPointsText.enabled = false;
        }
    }
}
