using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedCompanion : CompanionBase
{
	[SerializeField] private float speedAmount = 4f;
	[SerializeField] private float speedEffectTime = 3f;
	[SerializeField] private float speedStartTimer = 15f;
	private float speedTimer;

	private void Start()
	{
		speedTimer = speedStartTimer;
	}

	private void Update()
	{
		if (speedTimer <= 0f)
		{
			CompanionSkill();
		}

		speedTimer -= Time.deltaTime;
		FollowHero();
	}

	public override void CompanionSkill()
	{
		speedTimer = Random.Range(speedStartTimer, speedStartTimer + 4f);
		GetThrownBy().TryGetComponent(out PlayerMovement movement);
		movement.SetPlayerSpeed(speedAmount, speedEffectTime);
	}

	public override void FollowHero()
	{
		base.FollowHero();
	}
}
