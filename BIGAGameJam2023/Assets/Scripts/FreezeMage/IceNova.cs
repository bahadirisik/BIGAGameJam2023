using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceNova : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float iceBlockSpeed = 10f;

    private Vector3 startPos;
    bool isExploded = false;
    Transform target;
    void Start()
    {
        isExploded = false;
        startPos = transform.position;
        gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.down * iceBlockSpeed, ForceMode2D.Impulse);
    }

    void Update()
    {
        if(transform.position.y <= (startPos.y - 8f)  && !isExploded)
		{
            Explode();
		}
    }

    private void Explode()
	{
        isExploded = true;

        GameObject iceImpactGO = Instantiate(GameAssets.ins.iceImpactEffect, transform.position,
                    Quaternion.Euler(-90f, 0f, 0f));

        Destroy(iceImpactGO, 2f);

        if (Vector3.Distance(transform.position,target.position) <= 3f)
		{
            target.TryGetComponent(out DamageableBase damageableBase);
            damageableBase.DecreaseHealth(damage);
		}
        Destroy(gameObject);
	}

    public void SetTarget(Transform targetTransform)
	{
        target = targetTransform;
	}

	private void OnDrawGizmos()
	{
        Gizmos.DrawWireSphere(Vector3.zero,4f);
	}
}
