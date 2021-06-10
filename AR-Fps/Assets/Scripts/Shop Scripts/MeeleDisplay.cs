using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MeeleDisplay : MonoBehaviour
{
    public MeeleTemplate[] meeleWeapons;
    int meeleNumber;

    [Header("Display")]
    public GameObject selectionScreen;
    public GameObject meeleScreen;
    public Text money, meeleName, damage, Speed, level, description;
    public Image[] WeaponImages;

    [Header("Buttons")]
    public GameObject buyButton;
    public GameObject equipButton;
    public GameObject equippedButton;

    void Start()
    {
        //selectionScreen.SetActive(true);
        meeleScreen.SetActive(false);
        meeleScreen.transform.localScale = Vector2.zero; //LeenTween Animation

        LoadMoney();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LoadMoney()
    {
        if (PlayerPrefs.HasKey("Money"))
            money.text = PlayerPrefs.GetInt("Money").ToString();
        else
            money.text = "0";
    }

    public void DIsplayMeeleWeapon(int index)
    {
        selectionScreen.SetActive(false);
        meeleScreen.SetActive(true);
        meeleNumber = index;

        meeleScreen.transform.LeanScale(Vector2.one, 0.7f); //LeenTween Animation

        meeleName.text = meeleWeapons[index].Weapon_name;
        description.text = meeleWeapons[index].description;

        EnableMeeleImage();                                        //function to diasble all images and then enable the current gun image
        LoadMoney();
        UpdateTemplate();                                           //To change the template once a game is played then closed and then opened again

        if (!PlayerPrefs.HasKey(meeleWeapons[index].Weapon_name + "Damage"))
        {
            damage.text = "Damage : " + meeleWeapons[index].damage.ToString();
            Speed.text = "Firerate : " + meeleWeapons[index].attackSpeed.ToString();
            level.text = "Level - " + meeleWeapons[index].level.ToString();
        }
        else
        {
            UpdateTraitValues();
        }

        
    }

    void UpdateTemplate()
    {
        string tempMeeleName = meeleWeapons[meeleNumber].Weapon_name;

        if (PlayerPrefs.HasKey(tempMeeleName + "Damage"))
        {
            meeleWeapons[meeleNumber].damage = Save_Load_Meele.instance.LoadMeeleTraits(tempMeeleName + "Damage");
            meeleWeapons[meeleNumber].attackSpeed = Save_Load_Meele.instance.LoadMeeleTraits(tempMeeleName + "Speed");
            meeleWeapons[meeleNumber].level = (int)Save_Load_Meele.instance.LoadMeeleTraits(tempMeeleName + "Level");
        }

        if (PlayerPrefs.GetString("EquippedMeele") == meeleWeapons[meeleNumber].Weapon_name)
        {
            buyButton.SetActive(false);
            equipButton.SetActive(false);
            equippedButton.SetActive(true);
            //replace equip with equipped
        }
        else
        {
            if (PlayerPrefs.HasKey(meeleWeapons[meeleNumber].Weapon_name + "Unlocked"))
            {
                buyButton.SetActive(false);
                equippedButton.SetActive(false);
                equipButton.SetActive(true);
            }
            else
            {
                equippedButton.SetActive(false);
                buyButton.SetActive(true);
                equipButton.SetActive(false);
            }
        }
    }

    void EnableMeeleImage()
    {
        for (int i = 0; i < WeaponImages.Length; i++)
        {
            WeaponImages[i].gameObject.SetActive(false);
        }
        WeaponImages[meeleNumber].gameObject.SetActive(true);
    }

    void UpdateTraitValues()
    {
        damage.text = "Damage : " + Save_Load_Meele.instance.LoadMeeleTraits(meeleWeapons[meeleNumber].Weapon_name + "Damage").ToString();
        Speed.text = "Speed : " + Save_Load_Meele.instance.LoadMeeleTraits(meeleWeapons[meeleNumber].Weapon_name + "firerate").ToString();
        level.text = "Level - " + Save_Load_Meele.instance.LoadMeeleTraits(meeleWeapons[meeleNumber].Weapon_name + "Level").ToString();
    }

    public void OnEquip()
    {
        Save_Load_Meele.instance.SaveEquippedMeele(meeleWeapons[meeleNumber].Weapon_name);
        UpdateTemplate();
        PlayerPrefs.SetString("EquippedWeapon", "Meele");
    }

    public void OnBuy()
    {
        //enable equip button and subtract money
        buyButton.SetActive(false);
        equipButton.SetActive(true);

        PlayerPrefs.SetString(meeleWeapons[meeleNumber].Weapon_name + "Unlocked", "Unlocked");
    }

    public void OnExit()
    {
        meeleScreen.transform.LeanScale(Vector2.zero, 0.3f); //LeenTween Animation
        Invoke("SetMeeleScreenOff", 0.4f);
    }
    void SetMeeleScreenOff() //for leenTween animation
    {
        selectionScreen.SetActive(true);
        meeleScreen.SetActive(false);
    }


}
