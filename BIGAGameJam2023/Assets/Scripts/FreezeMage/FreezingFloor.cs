using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezingFloor : MonoBehaviour
{
    [SerializeField] private float slowAmount;

    private Transform thrownByTransform;
    private PlayerMovement targetPlayer;

	private void Start()
	{
        Destroy(gameObject, 7f);
	}

	private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out DamageableBase damageable))
        {
            if (damageable.transform != thrownByTransform)
            {
                damageable.TryGetComponent(out PlayerMovement playerMovement);
                targetPlayer = playerMovement;
                playerMovement.SlowPlayerSpeed(slowAmount);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out DamageableBase damageable))
        {
            if (damageable.transform != thrownByTransform)
            {
                damageable.TryGetComponent(out PlayerMovement playerMovement);
                playerMovement.DefaultPlayerSpeed();
            }
        }
    }

    public void SetThrownBy(Transform _thrownByTransform)
    {
        thrownByTransform = _thrownByTransform;
    }

	private void OnDestroy()
	{
        targetPlayer.DefaultPlayerSpeed();
	}

}
