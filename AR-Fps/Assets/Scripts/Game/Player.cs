using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("Stats")]
    public int curHp;
    public int maxHp;

    [SerializeField]
    private Weapon weapon;
    [SerializeField]
    private MeeleWeapon meeleWeapon;
    ///public Weapon weapon;

    [Header("Weapons")]
    public Weapon[] guns;
    public MeeleWeapon[] meeleWeapons;

    public bool holdToAttack;

    private void Start()
    {
        GameUI.instance.UpdateHealthbar(curHp, maxHp);

        Debug.Log(GameManager.instance.weaponEquippedIs);
        if (GameManager.instance.weaponEquippedIs=="Gun")
            EnableActiveGun(); // to enable the selected gun and to assign its weapon script to the player
        else if(GameManager.instance.weaponEquippedIs == "Meele")
            EnableActiveMeele(); // to enable the selected meele Weapon and to assign its script to the player
    }

    private void Update()
    {
        if (holdToAttack)
        {
            if (GameManager.instance.weaponEquippedIs=="Gun")
                Fire();
            else if(GameManager.instance.weaponEquippedIs == "Meele")
                MeeleAttack();
        }         
    }

    public void Fire()
    {
        if (weapon.CanShoot())
            weapon.Shoot();
    }

    private void MeeleAttack()
    {
        if (meeleWeapon.CanAttack())
            meeleWeapon.Attack();
    }

    public void OnPointerDown()
    {
        holdToAttack = true;
    }
    public void OnPointerUp()
    {
        holdToAttack = false;
    }

    public IEnumerator TakeDamage(int damage,float delay)
    {
        Debug.Log("Damage taken");
        //yield return new WaitForSeconds(1.6f);
        yield return new WaitForSeconds(delay);
        curHp -= damage;

        GameUI.instance.UpdateDamageEffect(curHp);
        GameUI.instance.UpdateHealthbar(curHp, maxHp);

        if (curHp <= 0)
            Die();
        yield break;
    }

    public void Reload()
    {
        weapon.ReloadGun();
    }

    void Die()
    {
        Debug.Log("Dead");
        GameManager.instance.LooseGame();
    }

    void EnableActiveGun()
    {
        int temp=0;
        for(int i = 0; i < guns.Length; i++)
        {
            guns[i].gameObject.SetActive(false);
            if (PlayerPrefs.GetString("EquippedGun") == guns[i].gunTemplate.gun_name)
            {
                Debug.Log(guns[i].gunTemplate.gun_name);
                temp = i;
            }   
        }
        Debug.Log("Gun to be used" + temp);
        guns[temp].gameObject.SetActive(true);
        weapon = guns[temp];

        GameUI.instance.gunImage.sprite = guns[temp].gunTemplate.gun_Image;
    }

    void EnableActiveMeele()
    {
        int temp = 0;
        for (int i = 0; i < meeleWeapons.Length; i++)
        {
            meeleWeapons[i].gameObject.SetActive(false);
            if (PlayerPrefs.GetString("EquippedMeele") == meeleWeapons[i].meeleTemplate.Weapon_name)
            {
                Debug.Log(meeleWeapons[i].meeleTemplate.Weapon_name);
                temp = i;
            }
        }
        Debug.Log("Meele to be used" + temp);
        meeleWeapons[temp].gameObject.SetActive(true);
        meeleWeapon = meeleWeapons[temp];

        //GameUI.instance.gunImage.sprite = guns[temp].gunTemplate.gun_Image;
    }

}
