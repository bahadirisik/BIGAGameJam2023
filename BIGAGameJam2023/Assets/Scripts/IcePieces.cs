using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcePieces : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float iceBlockSpeed = 10f;
    [SerializeField] private float impactForce = 0.1f;
    [SerializeField] private float impactTime = 1f;

    private Transform thrownByTransform;
    void Start()
    {
        gameObject.GetComponent<Rigidbody2D>().AddForce(transform.right * iceBlockSpeed, ForceMode2D.Impulse);
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.TryGetComponent(out DamageableBase damageable))
		{
			if (damageable.transform != thrownByTransform)
			{
				damageable.TryGetComponent(out PlayerMovement playerMovement);
				playerMovement.StartHitEffect(transform.right, impactForce, impactTime);
				playerMovement.SetPlayerSpeed(-2f,4f);

				damageable.DecreaseHealth(damage);
				Destroy(gameObject);
			}
		}
	}

	public void SetThrownBy(Transform _thrownByTransform)
	{
		thrownByTransform = _thrownByTransform;
	}
}
