using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceFloor : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float iceBlockSpeed = 10f;
    [SerializeField] private GameObject iceFloorPrefab;

    private Transform thrownByTransform;
    void Start()
    {
        gameObject.GetComponent<Rigidbody2D>().AddForce(transform.right * iceBlockSpeed, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out DamageableBase damageable))
        {
            if (damageable.transform != thrownByTransform)
            {
                damageable.TryGetComponent(out PlayerMovement playerMovement);
                playerMovement.SetPlayerSpeed(-1f, 4f);
                damageable.DecreaseHealth(damage);

                Instantiate(iceFloorPrefab, transform.position, Quaternion.identity).
                    GetComponent<FreezingFloor>().SetThrownBy(thrownByTransform);

                Destroy(gameObject);
            }
        }
    }

    public void SetThrownBy(Transform _thrownByTransform)
    {
        thrownByTransform = _thrownByTransform;
    }
}
