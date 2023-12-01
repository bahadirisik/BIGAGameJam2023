using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FireBall : MonoBehaviour
{
	[SerializeField] private int damage;
	[SerializeField] private float fireBallSpeed = 10f;
	[SerializeField] private float impactForce = 8f;
	[SerializeField] private float impactTime = 0.7f;

	private Transform thrownByTransform;

	private Rigidbody2D rb;

	Transform targetTransform;
	private bool isTargetFound = false;
	private void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		SetTargetTransform();
		Destroy(gameObject, 5f);
	}

	private void FixedUpdate()
	{
		if (!isTargetFound)
			return;

		rb.velocity = (targetTransform.position - transform.position).normalized * fireBallSpeed;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.TryGetComponent(out DamageableBase damageable))
		{
			if (damageable.transform != thrownByTransform)
			{
				damageable.TryGetComponent(out PlayerMovement playerMovement);
				playerMovement.StartHitEffect((targetTransform.position - transform.position), impactForce, impactTime);

				GameObject fireImpactGO = Instantiate(GameAssets.ins.fireImpactEffect, transform.position,
					Quaternion.Euler(-90f, 0f, 0f));

				Destroy(fireImpactGO, 2f);

				damageable.DecreaseHealth(damage);
				Destroy(gameObject);
			}
		}
	}

	public void SetThrownBy(Transform _thrownByTransform)
    {
        thrownByTransform = _thrownByTransform;
    }

	private void SetTargetTransform()
	{
		List<GameObject> playersGO = GameObject.FindGameObjectsWithTag("Player").ToList();

		GameObject thrownByGameObj = playersGO.Find(thrownByGO => thrownByGO.transform == thrownByTransform);
		playersGO.Remove(thrownByGameObj);

		if(playersGO.Count <= 0)
		{
			return;
		}

		targetTransform = playersGO.FirstOrDefault().transform;
		isTargetFound = true;
	}
}
