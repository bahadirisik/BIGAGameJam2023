using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float meteorSpeed = 10f;
    [SerializeField] private GameObject meteorShadow;
    [SerializeField] private float impactForce;
    [SerializeField] private float impactTime;

    private Vector3 startPos;
    private float endPosY;
    bool isExploded = false;
    List<DamageableBase> targets;
    /*void Start()
    {
        endPos = Random.Range(3f, 13f);
        meteorShadow.transform.position = new Vector3(transform.position.x, endPos, 0f);
        targets = FindObjectsOfType<DamageableBase>().ToList();
        isExploded = false;
        startPos = transform.position;
        gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.down * meteorSpeed, ForceMode2D.Impulse);
    }*/

	private void OnEnable()
	{
        transform.position = transform.parent.position;
        startPos = new Vector3(transform.position.x, 8f, 0f);
        endPosY = Random.Range(3f, 13f);
        meteorShadow.transform.position = new Vector3(transform.position.x, transform.position.y -  endPosY, 0f);
        targets = FindObjectsOfType<DamageableBase>().ToList();
        isExploded = false;
        gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.down * meteorSpeed, ForceMode2D.Impulse);
    }

	void Update()
    {
        if (transform.position.y <= (startPos.y - endPosY) && !isExploded)
        {
            Explode();
        }
    }

    private void Explode()
    {
        isExploded = true;

        GameObject fireImpactGO = ObjectPoolManager.SpawnObject(GameAssets.ins.fireImpactEffect, transform.position,
                Quaternion.Euler(-90f, 0f, 0f), ObjectPoolManager.PoolType.ParticleSystem);

		foreach (var target in targets)
		{
            if (Vector3.Distance(transform.position, target.transform.position) <= 3f)
			{
                target.DecreaseHealth(damage);
                target.TryGetComponent(out PlayerMovement playerMovement);
                playerMovement.StartHitEffect((target.transform.position - transform.position), impactForce, impactTime);
            }
        }

        ObjectPoolManager.ReturnObjectToPool(gameObject.transform.parent.gameObject);
    }
}
