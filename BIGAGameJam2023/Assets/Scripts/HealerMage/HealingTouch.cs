using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingTouch : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float healingTouchSpeed = 10f;
    [SerializeField] private int healingTouchHealthAmount = 10;
    [SerializeField] private float impactForce = 0.1f;
    [SerializeField] private float impactTime = 1f;

    private Transform thrownByTransform;

    private void Start()
    {
        gameObject.GetComponent<Rigidbody2D>().AddForce(transform.right * healingTouchSpeed, ForceMode2D.Impulse);
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

				thrownByTransform.TryGetComponent(out DamageableBase healerMageHealth);
				healerMageHealth.IncreaseHealth(healingTouchHealthAmount);

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
