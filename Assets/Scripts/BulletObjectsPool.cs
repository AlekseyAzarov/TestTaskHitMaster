using System.Collections.Generic;
using UnityEngine;

public class BulletObjectsPool : MonoBehaviour
{
    public static BulletObjectsPool Instance;

    [SerializeField] private GameObject _bullet;
    [SerializeField] private int _poolSize;

    private Queue<GameObject> _bulletQueue = new Queue<GameObject>();

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private void Start()
    {
        for (int i = 0; i < _poolSize; i++)
        {
            GameObject bullet = Instantiate(_bullet, transform);
            _bulletQueue.Enqueue(bullet);
            bullet.SetActive(false);
        }
    }

    public GameObject SpawnBullet()
    {
        GameObject bullet = _bulletQueue.Dequeue();
        bullet.SetActive(true);
        _bulletQueue.Enqueue(bullet);
        return bullet;
    }

    public void ReturnBullet(GameObject bullet)
    {
        bullet.transform.position = Vector3.zero;
        bullet.SetActive(false);
    }
}
