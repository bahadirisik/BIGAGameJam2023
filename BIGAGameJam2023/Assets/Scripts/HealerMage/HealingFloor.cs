using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingFloor : MonoBehaviour
{
    [SerializeField] private int healthAmount;
    [SerializeField] private int damage;

    private float startHealingTimer = 0.5f;
    private float healingTimer = 0.5f;

    private Transform thrownByTransform;

    private void Start()
    {
        healingTimer = startHealingTimer;
        Destroy(gameObject, 10f);
    }

	private void Update()
	{
        healingTimer -= Time.deltaTime;
	}

	private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out DamageableBase damageable))
        {
            if (damageable.transform != thrownByTransform)
            {
                if(healingTimer > 0f)
				{
                    return;
				}
                healingTimer = startHealingTimer;

                damageable.DecreaseHealth(damage);

                thrownByTransform.TryGetComponent(out DamageableBase healerMage);
                healerMage.IncreaseHealth(healthAmount);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out DamageableBase damageable))
        {
            if (damageable.transform != thrownByTransform)
            {
                if (healingTimer > 0f)
                {
                    return;
                }
                healingTimer = startHealingTimer;

                damageable.DecreaseHealth(damage);

                thrownByTransform.TryGetComponent(out DamageableBase healerMage);
                healerMage.IncreaseHealth(healthAmount);
            }
        }
    }

    public void SetThrownBy(Transform _thrownByTransform)
    {
        thrownByTransform = _thrownByTransform;
    }
}
