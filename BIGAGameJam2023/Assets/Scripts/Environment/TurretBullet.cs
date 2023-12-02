using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBullet : MonoBehaviour
{
	[SerializeField] private int damage;
	[SerializeField] private float bulletSpeed = 10f;
	[SerializeField] private float impactForce = 8f;
	[SerializeField] private float impactTime = 0.7f;
	private float deathTime = 4f;
	private float startingDeathTime = 4f;

	/*private void Start()
	{
		transform.right = new Vector3(Random.Range(-360f, 360f), Random.Range(-360f, 360f), 0f);

		gameObject.GetComponent<Rigidbody2D>().AddForce(transform.right * bulletSpeed, ForceMode2D.Impulse);
		//Destroy(gameObject, 7f);
	}*/

	private void OnEnable()
	{
		transform.right = new Vector3(Random.Range(-360f, 360f), Random.Range(-360f, 360f), 0f);

		gameObject.GetComponent<Rigidbody2D>().AddForce(transform.right * bulletSpeed, ForceMode2D.Impulse);
	}

	private void Update()
	{
		deathTime -= Time.deltaTime;

		if (deathTime <= 0f)
		{
			deathTime = startingDeathTime;
			ObjectPoolManager.ReturnObjectToPool(gameObject);
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.TryGetComponent(out DamageableBase damageable))
		{
			damageable.TryGetComponent(out PlayerMovement playerMovement);
			playerMovement.StartHitEffect(transform.right, impactForce, impactTime);

			damageable.DecreaseHealth(damage);

			GameObject bulletImpactGO = ObjectPoolManager.SpawnObject(GameAssets.ins.fireImpactEffect, transform.position,
				Quaternion.Euler(-90f, 0f, 0f), ObjectPoolManager.PoolType.ParticleSystem);

			//Destroy(bulletImpactGO, 2f);

			//Destroy(gameObject);
			ObjectPoolManager.ReturnObjectToPool(gameObject);
		}
	}
}
