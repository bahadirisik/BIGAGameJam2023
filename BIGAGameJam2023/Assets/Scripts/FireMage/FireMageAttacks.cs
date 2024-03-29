using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireMageAttacks : MonoBehaviour, IMageAttack
{
	HealthBarUI heroInfoPanel;

	[Header("Attack1")]
	[SerializeField] private GameObject fireBlast;
	private float fireBlastRate = 2f;
	private float fireBlastCurrentCooldown;

	[Header("Attack2")]
	[SerializeField] private GameObject fireWall;
	private float fireWallRate = 4f;
	private float fireWallCurrentCooldown;

	[Header("Attack3")]
	[SerializeField] private GameObject fireBall;
	private float fireBallRate = 6f;
	private float fireBallCurrentCooldown;

	[Header("Direction Arrow")]
	[SerializeField] private Transform directionArrow;
	[SerializeField] private float rotateSpeed;


	private void Start()
	{
		fireBlastCurrentCooldown = 0f;
		fireWallCurrentCooldown = 0f;
		fireBallCurrentCooldown = 0f;

		heroInfoPanel = GetComponentInChildren<PlayerInputHandler>().GetHeroInfoPanel().GetComponent<HealthBarUI>();
	}

	private void Update()
	{
		RotateDirectionArrow();
		
		fireBlastCurrentCooldown -= Time.deltaTime;
		fireWallCurrentCooldown -= Time.deltaTime;
		fireBallCurrentCooldown -= Time.deltaTime;
	}

	public void MageAttack1()
	{
		if(fireBlastCurrentCooldown > 0f)
		{
			return;
		}

		fireBlastCurrentCooldown = fireBlastRate;
		//GameObject fireBlastGO = Instantiate(fireBlast, directionArrow.position, directionArrow.rotation);

		GameObject fireBlastGO = ObjectPoolManager.SpawnObject(fireBlast, directionArrow.position,
				directionArrow.rotation, ObjectPoolManager.PoolType.GameObjectSystem);

		fireBlastGO.GetComponent<FireBlastProjectile>().SetThrownBy(transform);

		heroInfoPanel.SetSkillOneImage(fireBlastRate);
	}

	public void MageAttack2()
	{
		if (fireWallCurrentCooldown > 0f)
		{
			return;
		}

		fireWallCurrentCooldown = fireWallRate;
		//GameObject fireWallGO = Instantiate(fireWall, directionArrow.position, directionArrow.rotation);

		GameObject fireWallGO = ObjectPoolManager.SpawnObject(fireWall, directionArrow.position,
				directionArrow.rotation, ObjectPoolManager.PoolType.GameObjectSystem);

		fireWallGO.GetComponentInChildren<FireWall>().SetThrownBy(transform);

		heroInfoPanel.SetSkillTwoImage(fireWallRate);
	}

	public void MageAttack3()
	{
		if (fireBallCurrentCooldown > 0f)
		{
			return;
		}

		fireBallCurrentCooldown = fireBallRate;
		//GameObject fireBallGO = Instantiate(fireBall, transform.position, Quaternion.identity);

		GameObject fireBallGO = ObjectPoolManager.SpawnObject(fireBall, directionArrow.position,
				Quaternion.identity, ObjectPoolManager.PoolType.GameObjectSystem);

		fireBallGO.GetComponent<FireBall>().SetThrownBy(transform);

		heroInfoPanel.SetSkillThreeImage(fireBallRate);
	}

	void RotateDirectionArrow()
	{
		directionArrow.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);
	}

}
