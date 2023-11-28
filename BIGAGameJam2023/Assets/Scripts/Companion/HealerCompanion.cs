using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealerCompanion : CompanionBase
{
	[SerializeField] private int healAmount = 15;
	[SerializeField] private float healStartTimer = 15f;
	private float healTimer;

	private void Start()
	{
		healTimer = healStartTimer;
	}

	private void Update()
	{
		if(healTimer <= 0f)
		{
			CompanionSkill();
		}

		healTimer -= Time.deltaTime;
		FollowHero();
	}
	public override void CompanionSkill()
	{
		healTimer = Random.Range(healStartTimer, healStartTimer + 5f);
        GetThrownBy().TryGetComponent(out DamageableBase damageable);
        damageable.IncreaseHealth(healAmount);
	}

	public override void FollowHero()
	{
		base.FollowHero();
	}
}
