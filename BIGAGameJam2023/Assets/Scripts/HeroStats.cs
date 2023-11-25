using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class HeroStats : ScriptableObject
{
    public int heroID;
    public string heroName;
    public GameObject heroPrefab;
    public Sprite heroSprite;
}
