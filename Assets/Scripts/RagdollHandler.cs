using UnityEngine;

[RequireComponent(typeof(Animator))]
public class RagdollHandler : MonoBehaviour
{
    [SerializeField] private Rigidbody[] _rigidbodies;
    [SerializeField] private Collider[] _colliders;

    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        DisableRagdoll();
    }

    public void EnableeRagdoll()
    {
        _animator.enabled = false;

        for (int i = 0; i < _rigidbodies.Length; i++)
        {
            _rigidbodies[i].isKinematic = false;
            _colliders[i].enabled = true;
        }
    }

    public void DisableRagdoll()
    {
        _animator.enabled = true;

        for (int i = 0; i < _rigidbodies.Length; i++)
        {
            _rigidbodies[i].isKinematic = true;
            _colliders[i].enabled = false;
        }
    }


}
