using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBlastProjectile : MonoBehaviour, IProjectile
{
    [SerializeField] private int damage;
	[SerializeField] private float fireBlastSpeed = 10f;
	[SerializeField] private float impactForce = 0.1f;
	[SerializeField] private float impactTime = 1f;

	private Transform thrownByTransform;

	private void Start()
	{
		gameObject.GetComponent<Rigidbody2D>().AddForce(transform.right * fireBlastSpeed, ForceMode2D.Impulse);
		Destroy(gameObject, 7f);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.TryGetComponent(out DamageableBase damageable))
		{
			if (damageable.transform != thrownByTransform)
			{
				damageable.TryGetComponent(out PlayerMovement playerMovement);
				playerMovement.StartHitEffect(transform.right, impactForce, impactTime);

				damageable.DecreaseHealth(damage);

				GameObject fireImpactGO = Instantiate(GameAssets.ins.fireImpactEffect, transform.position,
					Quaternion.Euler(-90f, 0f, 0f));

				Destroy(fireImpactGO, 2f);

				Destroy(gameObject);
			}
		}
	}

	public void SetThrownBy(Transform _thrownByTransform)
	{
		thrownByTransform = _thrownByTransform;
	}
}
