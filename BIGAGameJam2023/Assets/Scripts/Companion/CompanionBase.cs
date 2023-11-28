using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CompanionBase : MonoBehaviour
{
    private Transform heroTransform;

	public abstract void CompanionSkill();

    public void SetThrownBy(Transform _heroTransform)
    {
        heroTransform = _heroTransform;
    }

    public Transform GetThrownBy()
    {
        return heroTransform;
    }

    public virtual void FollowHero()
    {
        transform.position = heroTransform.position + new Vector3(2f, 2f, 0f);
    }
}
