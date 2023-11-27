using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FreezeMageAttacks : MonoBehaviour, IMageAttack
{
	[Header("Attack1")]
	[SerializeField] private GameObject icePieces;
	private float icePiecesRate = 1.8f;
	private float icePiecesCurrentCooldown;

	[Header("Attack2")]
	[SerializeField] private GameObject iceNova;
	private float iceNovaRate = 8f;
	private float iceNovaCurrentCooldown;

	[Header("Direction Arrow")]
	[SerializeField] private Transform directionArrow;
	[SerializeField] private float rotateSpeed;

	private void Start()
	{
		icePiecesCurrentCooldown = 0f;
		iceNovaCurrentCooldown = 0f;
	}

	private void Update()
	{
		RotateDirectionArrow();

		icePiecesCurrentCooldown -= Time.deltaTime;
		iceNovaCurrentCooldown -= Time.deltaTime;
	}

	public void MageAttack1()
	{
		if (icePiecesCurrentCooldown > 0f)
		{
			return;
		}

		icePiecesCurrentCooldown = icePiecesRate;
		GameObject icePiecesGO = Instantiate(icePieces, directionArrow.position, directionArrow.rotation);
		Destroy(icePiecesGO, 7f);
		foreach (var item in icePiecesGO.GetComponentsInChildren<IcePieces>())
		{
			item.SetThrownBy(transform);
		}	
	}

	public void MageAttack2()
	{
		if (iceNovaCurrentCooldown > 0f)
		{
			return;
		}

		iceNovaCurrentCooldown = iceNovaRate;

		List<GameObject> playersGO = GameObject.FindGameObjectsWithTag("Player").ToList();
		GameObject thrownByGameObj = playersGO.Find(thrownByGO => thrownByGO.transform == transform);
		playersGO.Remove(thrownByGameObj);
		Vector3 novaPos = playersGO.FirstOrDefault().transform.position + new Vector3(0f,8f,0f);

		GameObject iceNovaGO = Instantiate(iceNova, novaPos, Quaternion.identity);
		iceNovaGO.GetComponent<IceNova>().SetTarget(playersGO.FirstOrDefault().transform);
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
