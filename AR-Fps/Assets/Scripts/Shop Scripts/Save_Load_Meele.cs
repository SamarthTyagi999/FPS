using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save_Load_Meele : MonoBehaviour
{
    public static Save_Load_Meele instance;

    public string currentMeele;
    public int x = 10;

    private void Start()
    {
        instance = this;

        //if (!PlayerPrefs.HasKey("EquippedMeele"))
            //PlayerPrefs.SetString("Equipped", "Pistol");
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

    public void SaveEquippedMeele(string meele)
    {
        currentMeele = meele;
        PlayerPrefs.SetString("EquippedMeele", currentMeele);
        //PlayerPrefs.Save();
    }
    public string LoadEquippedGun()
    {
        currentMeele = PlayerPrefs.GetString("EquippedMeele");
        return (currentMeele);
    }

    public void SaveMeeleTraits(string name, float value)
    {
        PlayerPrefs.SetFloat(name, value);
        //PlayerPrefs.Save();
    }
    public float LoadMeeleTraits(string name)
    {
        float trait = PlayerPrefs.GetFloat(name);
        return trait;
    }

}
