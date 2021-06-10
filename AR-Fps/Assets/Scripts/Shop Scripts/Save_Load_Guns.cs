using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save_Load_Guns : MonoBehaviour
{
    public static Save_Load_Guns instance;

    public string currentGun;
    public int x = 10;

    private void Start()
    {
        instance = this;

        if (!PlayerPrefs.HasKey("EquippedGun"))
            PlayerPrefs.SetString("EquippedGun", "Pistol");
    }

    public void SaveMoney(int money)
    {
        PlayerPrefs.SetInt("Money", money);
        //PlayerPrefs.Save();

    }
    public int GetMoney()
    {
        return (PlayerPrefs.GetInt("Money"));
    }

    public void SaveEquippedGun(string gun) 
    {
        currentGun = gun;
        PlayerPrefs.SetString("EquippedGun", currentGun);
        //PlayerPrefs.Save();
    }
    public string LoadEquippedGun()
    {
        currentGun = PlayerPrefs.GetString("EquippedGun");
        return (currentGun);
    }

    public void SaveGunsTraits(string name,float value)
    {
        PlayerPrefs.SetFloat(name, value);
        //PlayerPrefs.Save();
    }
    public float LoadGunsTraits(string name)
    {
        float trait = PlayerPrefs.GetFloat(name);
        return trait;
    }

}
