using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldCompanion : CompanionBase
{
	[SerializeField] private int shieldAmount = 15;
	[SerializeField] private float shieldEffectTime = 3f;
	[SerializeField] private float shieldStartTimer = 15f;
	private float shieldTimer;

	private void Start()
	{
		shieldTimer = shieldStartTimer;
	}

	private void Update()
	{
		if (shieldTimer <= 0f)
		{
			CompanionSkill();
		}

		shieldTimer -= Time.deltaTime;
		FollowHero();
	}

    public override void CompanionSkill()
    {
		shieldTimer = Random.Range(shieldStartTimer, shieldStartTimer + 5f);
		GetThrownBy().TryGetComponent(out DamageableBase damageable);
		damageable.SetPlayerShield(shieldAmount,shieldEffectTime);
	}

	public override void FollowHero()
	{
		base.FollowHero();
	}
}
