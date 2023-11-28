using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HealerMageAttacks : MonoBehaviour, IMageAttack
{
	[Header("Attack1")]
	[SerializeField] private int healthAmount;
	private float healRate = 10f;
	private float healCurrentCooldown;

	[Header("Attack2")]
	[SerializeField] private GameObject healingTouch;
	private float healingTouchRate = 2f;
	private float healingTouchCurrentCooldown;

	[Header("Attack3")]
	[SerializeField] private GameObject healingFloor;
	private float healingFloorRate = 5f;
	private float healingFloorCurrentCooldown;

	[Header("Direction Arrow")]
	[SerializeField] private Transform directionArrow;
	[SerializeField] private float rotateSpeed;

	private void Start()
	{
		healCurrentCooldown = 0f;
		healingTouchCurrentCooldown = 0f;
		healingFloorCurrentCooldown = 0f;
	}

	private void Update()
	{
		RotateDirectionArrow();

		healCurrentCooldown -= Time.deltaTime;
		healingTouchCurrentCooldown -= Time.deltaTime;
		healingFloorCurrentCooldown -= Time.deltaTime;
	}
	public void MageAttack1()
	{
		if (healCurrentCooldown > 0f)
		{
			return;
		}

		healCurrentCooldown = healRate;
		gameObject.TryGetComponent(out DamageableBase damageableBase);
		damageableBase.IncreaseHealth(healthAmount);
	}

	public void MageAttack2()
	{
		if (healingTouchCurrentCooldown > 0f)
		{
			return;
		}

		healingTouchCurrentCooldown = healingTouchRate;
		GameObject healingTouchGO = Instantiate(healingTouch, directionArrow.position, directionArrow.rotation);
		healingTouchGO.GetComponent<HealingTouch>().SetThrownBy(transform);
	}

	public void MageAttack3()
	{
		if (healingFloorCurrentCooldown > 0f)
		{
			return;
		}

		healingFloorCurrentCooldown = healingFloorRate;

		List<GameObject> playersGO = GameObject.FindGameObjectsWithTag("Player").ToList();
		GameObject thrownByGameObj = playersGO.Find(thrownByGO => thrownByGO.transform == transform);
		playersGO.Remove(thrownByGameObj);

		if(playersGO.Count == 0)
		{
			return;
		}

		Vector3 healingFloorPos = playersGO.FirstOrDefault().transform.position;

		GameObject healingFloorGO = Instantiate(healingFloor, healingFloorPos, Quaternion.identity);
		healingFloorGO.GetComponent<HealingFloor>().SetThrownBy(transform);
	}

	void RotateDirectionArrow()
	{
		directionArrow.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);
	}

}
