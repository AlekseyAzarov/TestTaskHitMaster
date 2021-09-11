using UnityEngine;

public class Pistol : MonoBehaviour
{
    [SerializeField] private int _shotsPerMinute;

    private float _delayBetweenShots;
    private bool _canShoot;

    private void Start()
    {
        _delayBetweenShots = 60 / (float)_shotsPerMinute;
        _canShoot = true;
    }

    private void Update()
    {
        _delayBetweenShots -= Time.deltaTime;

        if (_delayBetweenShots < 0)
        {
            _delayBetweenShots = 60 / (float)_shotsPerMinute;
            _canShoot = true;
        }
    }

    public void Shoot(Vector3 originPosition, Vector3 destination)
    {
        if (_canShoot)
        {
            GameObject bullet = BulletObjectsPool.Instance.SpawnBullet();
            bullet.transform.position = originPosition;
            bullet.transform.LookAt(destination);
            _canShoot = false;
        }
    }
}
