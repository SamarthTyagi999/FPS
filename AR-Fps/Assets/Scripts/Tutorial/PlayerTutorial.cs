using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTutorial : MonoBehaviour
{
    [Header("Stats")]
    public int curHp;
    public int maxHp;


    public Weapon weapon;


    public bool holdToFire;

    private void Start()
    {
        GameUI.instance.UpdateHealthbar(curHp, maxHp);

    }

    private void Update()
    {
        if (holdToFire)
            Fire();
    }

    public void Fire()
    {
        if (weapon.CanShoot())
            weapon.Shoot();
    }

    public void OnPointerDown()
    {
        holdToFire = true;
    }
    public void OnPointerUp()
    {
        holdToFire = false;
    }

    public IEnumerator TakeDamage(int damage)
    {
        yield return new WaitForSeconds(1.6f);
        curHp -= damage;

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

    
}


