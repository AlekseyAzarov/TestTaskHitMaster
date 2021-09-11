using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _maxLifeTime;

    private void Update()
    {
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);

        _maxLifeTime -= Time.deltaTime;

        if (_maxLifeTime < 0)
        {
            BulletObjectsPool.Instance.ReturnBullet(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDamageable obj))
        {
            obj.TakeDamage();
        }

        BulletObjectsPool.Instance.ReturnBullet(gameObject);
    }
}