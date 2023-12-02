using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MeteorSpawner : MonoBehaviour
{
    [SerializeField] private GameObject meteorPrefab;
    [SerializeField] private float maxX;
    [SerializeField] private float startTimer;
    private float currentTimer;

	private void Start()
	{
        currentTimer = startTimer;
	}

	// Update is called once per frame
	void Update()
    {
        if(currentTimer <= 0f)
		{
            SpawnMeteor();
		}

        currentTimer -= Time.deltaTime;
    }

	private void SpawnMeteor()
	{
        currentTimer = startTimer;

        Vector3 randomPos = new Vector3(Random.Range(-maxX,maxX), 8f ,0f);

        GameObject meteorPrefabGO = ObjectPoolManager.SpawnObject(meteorPrefab, randomPos,
                Quaternion.identity, ObjectPoolManager.PoolType.GameObjectSystem);
    }
}
