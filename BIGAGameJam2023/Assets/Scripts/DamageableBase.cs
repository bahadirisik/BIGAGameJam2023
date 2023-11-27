using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DamageableBase : MonoBehaviour
{
	private int startingHealth = 100;
	int currentHealth;
	bool isDead = false;
	void Start()
	{
		isDead = false;
		currentHealth = startingHealth;
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
		currentHealth -= damage;
	}

	public void IncreaseHealth(int health)
	{
		currentHealth += health;
	}
}
