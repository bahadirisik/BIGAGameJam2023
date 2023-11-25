using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireMageAttacks : MonoBehaviour, IMageAttack
{
	[Header("Attack1")]
	[SerializeField] private GameObject fireBlast;
	[SerializeField] private float fireBlastSpeed = 10f;

	[Header("Direction Arrow")]
	[SerializeField] private Transform directionArrow;
	[SerializeField] private float rotateSpeed;

	private void Update()
	{
		RotateDirectionArrow();
	}

	public void MageAttack1()
	{
		GameObject fireBlastGO = Instantiate(fireBlast, directionArrow.position, directionArrow.rotation);
		fireBlastGO.GetComponent<Rigidbody2D>().AddForce(directionArrow.right * fireBlastSpeed,ForceMode2D.Impulse);
		Destroy(fireBlastGO,5f);
	}

	public void MageAttack2()
	{
		Debug.Log("Fire Attack2");
	}

	public void MageAttack3()
	{
		Debug.Log("Fire Attack3");
	}

	void RotateDirectionArrow()
	{
		directionArrow.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);
	}

}
