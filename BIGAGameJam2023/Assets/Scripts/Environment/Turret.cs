using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float startingFireRate;
    private float fireRate;
    void Start()
    {
        fireRate = startingFireRate;
    }

    // Update is called once per frame
    void Update()
    {
        if(fireRate <= 0f)
		{
            TurretShoot();
		}

        fireRate -= Time.deltaTime;
    }

    private void TurretShoot()
	{
        fireRate = startingFireRate;
        GameObject bulletGameObject = ObjectPoolManager.SpawnObject(bulletPrefab, transform.position,
                Quaternion.identity, ObjectPoolManager.PoolType.GameObjectSystem);
    }
}
