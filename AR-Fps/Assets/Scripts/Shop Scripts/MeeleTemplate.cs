using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Meele Weapon", menuName = "MeeleWeapon")]
public class MeeleTemplate : ScriptableObject
{
    public int index;
    public string Weapon_name;
    public string description;

    public Sprite Weapon_Image;

    public float damage;
    public float attackSpeed;
    public int level;

    public int upgradeCost;
    public int unlockCost;

    public bool isUnlocked;
    public bool isEquipped;

}
