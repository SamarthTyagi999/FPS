using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Gun", menuName = "Gun")]
public class GunsTemplate : ScriptableObject
{
    public int index;
    public string gun_name;
    public string description;

    public Sprite gun_Image;

    public float damage;
    public float firerate;
    public int level;

    public int upgradeCost;
    public int unlockCost;

    public bool isUnlocked;
    //public bool isEquipped;
}
