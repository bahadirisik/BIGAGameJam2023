using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    [SerializeField] private int damage;

    private float startDamageTimer = 1f;
    private float damageTimer = 1f;

    private void Start()
    {
        damageTimer = startDamageTimer;
    }

    private void Update()
    {
        damageTimer -= Time.deltaTime;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out DamageableBase damageable))
        {
            if (damageTimer > 0f)
            {
                return;
            }
            damageTimer = startDamageTimer;

            damageable.DecreaseHealth(damage);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out DamageableBase damageable))
        {
            if (damageTimer > 0f)
            {
                return;
            }
            damageTimer = startDamageTimer;

            damageable.DecreaseHealth(damage);
        }
    }

}
