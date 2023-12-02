using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerZone : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		transform.GetChild(0).GetComponent<DesertDog>().SetCanAttack(true, collision.transform);
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		transform.GetChild(0).GetComponent<DesertDog>().SetCanAttack(false, null);
	}
}
