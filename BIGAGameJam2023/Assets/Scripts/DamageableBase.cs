using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DamageableBase : MonoBehaviour
{
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
		gameObject.SetActive(false);
	}

	public void DecreaseHealth(int damage)
	{
		damage -= currentShield;

		if(damage < 0)
		{
			damage = 0;
		}

		currentHealth -= damage;

		if (currentHealth <= 0)
			currentHealth = 0;

		Debug.Log(gameObject.name + " " + currentHealth);
	}

	public void IncreaseHealth(int health)
	{
		currentHealth += health;

		if (currentHealth >= startingHealth)
			currentHealth = startingHealth;

		Debug.Log(gameObject.name + " " + currentHealth);
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
