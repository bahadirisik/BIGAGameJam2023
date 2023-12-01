using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DamageableBase : MonoBehaviour
{
	public event Action<int> OnHealthChange;

	private int startingHealth = 100;
	private int currentHealth;
	private int startingShield = 0;
	private int currentShield;

	bool isDead = false;
	void Start()
	{
		isDead = false;
		currentHealth = startingHealth;
		currentShield = startingShield;

		OnHealthChange?.Invoke(currentHealth);
	}

	void Update()
	{
		if (currentHealth <= 0 && !isDead)
		{
			isDead = true;
			Death();
		}
	}

	public void Death()
	{
		GameManager.instance.SetGameState(GameManager.GameState.GameEnded);

		gameObject.SetActive(false);
	}

	public void DecreaseHealth(int damage)
	{
		damage -= currentShield;

		CameraShake.Instance.ShakeCamera(3f, 0.3f);

		if (damage < 0)
		{
			damage = 0;
		}

		currentHealth -= damage;

		OnHealthChange?.Invoke(currentHealth);

		if (currentHealth <= 0)
			currentHealth = 0;
	}

	public void IncreaseHealth(int health)
	{
		currentHealth += health;

		if (currentHealth >= startingHealth)
			currentHealth = startingHealth;

		OnHealthChange?.Invoke(currentHealth);
	}

	public void SetPlayerShield(int shield, float effectTimer)
	{
		StartCoroutine(SetShield(shield, effectTimer));
	}

	private IEnumerator SetShield(int shield, float effectTimer)
	{
		currentShield = shield;
		yield return new WaitForSeconds(effectTimer);
		currentShield = startingShield;
	}
}
