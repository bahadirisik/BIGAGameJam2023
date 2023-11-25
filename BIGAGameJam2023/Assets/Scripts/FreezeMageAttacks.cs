using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeMageAttacks : MonoBehaviour, IMageAttack
{
	[Header("Direction Arrow")]
	[SerializeField] private Transform directionArrow;
	[SerializeField] private float rotateSpeed;

	private void Update()
	{
		RotateDirectionArrow();
	}
	public void MageAttack1()
	{
		Debug.Log("Freeze Attack1");
	}

	public void MageAttack2()
	{
		Debug.Log("Freeze Attack2");
	}

	public void MageAttack3()
	{
		Debug.Log("Freeze Attack3");
	}

	void RotateDirectionArrow()
	{
		directionArrow.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);
	}
}
