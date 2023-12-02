using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DesertDog : MonoBehaviour
{
    [Header("Dog Attack")]
    [SerializeField] private float maxXPos;
    Vector3 randomPosition;
    private float dogSpeed = 3f;
    [SerializeField] private int dogDamage;
    private float startAttackTimer = 1f;
    private float attackTimer;
    
    private bool canAttack = false;
    private Transform target;
    Rigidbody2D rb;
    void Start()
    {
        attackTimer = startAttackTimer;
        rb = GetComponent<Rigidbody2D>();
        canAttack = false;
        FindNewRandomPosition();
    }

	private void Update()
	{
        attackTimer -= Time.deltaTime;

        if (!canAttack)
        {
            RandomWalk();
        }

        if (target == null)
		{
            return;
		}

		if(Vector3.Distance(transform.position , target.position) <= 0.2f && attackTimer <= 0f)
		{
            DamageToPlayer();
		}

        if (canAttack)
        {
            FollowPlayer();
        }
	}


    public void SetCanAttack(bool value, Transform _target)
	{
        canAttack = value;
        target = _target;
	}

    private void FollowPlayer()
	{
        rb.velocity = (target.position - transform.position).normalized * dogSpeed;
	}

    private void DamageToPlayer()
	{
        attackTimer = startAttackTimer;
        target.TryGetComponent(out DamageableBase damageableBase);
        damageableBase.DecreaseHealth(dogDamage);
	}

    private void RandomWalk()
	{
        if(Vector3.Distance(transform.position,randomPosition) <= 0.2f)
		{
            FindNewRandomPosition();
		}

        rb.velocity = (randomPosition - transform.position).normalized * (dogSpeed / 2);
    }

	private void FindNewRandomPosition()
	{
        randomPosition = new Vector3(Random.Range(maxXPos - 2, maxXPos),
            Random.Range(-3f, 3f), 0f);
    }
}
