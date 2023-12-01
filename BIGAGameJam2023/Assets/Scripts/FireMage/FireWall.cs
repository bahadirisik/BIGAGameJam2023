using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWall : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float impactForce = 0.1f;
    [SerializeField] private float impactTime = 1f;

    [SerializeField] private SpriteRenderer grpx;
    [SerializeField] private Transform objectCollider;

    [SerializeField] private float maxXScale;
    private float scaleAmount = 0.1f;
    private float scaleAmountGRPX = 0.067f;
    private float scaleSpeed = 0.005f;
    private float currentScaleSpeed;

    private float startDamageTimer = 0.5f;
    private float damageTimer;

    private Transform thrownByTransform;
    void Start()
    {
        damageTimer = startDamageTimer;
        currentScaleSpeed = scaleSpeed;
        Destroy(gameObject.transform.parent.gameObject, 10f);
    }

    // Update is called once per frame
    void Update()
    {
        if(currentScaleSpeed <= 0f && objectCollider.localScale.x < maxXScale)
		{
            currentScaleSpeed = scaleSpeed;
            objectCollider.localScale = new Vector3(objectCollider.localScale.x + scaleAmount, objectCollider.localScale.y, objectCollider.localScale.z);
            grpx.size = new Vector2(grpx.size.x + scaleAmountGRPX,10.8f);
		}

        currentScaleSpeed -= Time.deltaTime;
        damageTimer -= Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out DamageableBase damageable))
        {
            if(damageable.transform != thrownByTransform && damageTimer <= 0f)
			{
                damageable.TryGetComponent(out PlayerMovement playerMovement);
                Vector2 closestPos = gameObject.GetComponent<Collider2D>().ClosestPoint(collision.transform.position);

                Debug.Log(damageable.gameObject.name);

                playerMovement.StartHitEffect((Vector2)playerMovement.transform.position - closestPos, impactForce, impactTime);

                damageTimer = startDamageTimer;
                damageable.DecreaseHealth(damage);
            }
        }
    }

    public void SetThrownBy(Transform _thrownByTransform)
	{
        thrownByTransform = _thrownByTransform;
	}
}
