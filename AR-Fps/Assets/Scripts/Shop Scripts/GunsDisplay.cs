using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunsDisplay : MonoBehaviour
{
    public GunsTemplate[] guns;
    int gunNumber;

    [Header("Display")]
    public GameObject selectionScreen;
    public GameObject gunsScreen;
    public Text money, gunName, damage, fireRate, level, description;
    public Image[] gunImages;

    [Header("Buttons")]
    public GameObject buyButton;
    public GameObject equipButton;
    public GameObject equippedButton;
  

    private void Start()
    {
        //selectionScreen.SetActive(true);
        gunsScreen.SetActive(false);
        gunsScreen.transform.localScale = Vector2.zero; //LeenTween Animation

        LoadMoney();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            ClearPrefs();
    }

    public void DisplayGun(int index)
    {
        selectionScreen.SetActive(false);
        gunsScreen.SetActive(true);
        
        gunsScreen.transform.LeanScale(Vector2.one, 0.7f); //LeenTween Animation

        gunNumber = index;                                       //to access the selected gun traits

        Debug.Log("Hello" + guns[index].gun_name);
        gunName.text = guns[index].gun_name;
        description.text = guns[index].description;

        EnableGunImage();                                        //function to diasble all images and then enable the current gun image
        LoadMoney();
        UpdateTemplate();                                        //To change the template once a game is played then closed and then opened again

        if (!PlayerPrefs.HasKey(guns[index].gun_name + "Damage"))
        {
            Debug.Log("exsist"+PlayerPrefs.HasKey(guns[index].gun_name + "Damage"));
            damage.text = "Damage : " + guns[index].damage.ToString();
            fireRate.text = "Firerate : " + guns[index].firerate.ToString();
            level.text = "Level - " + guns[index].level.ToString();
        }
        else
        {
            UpdateTraitValues();
        }
        
    }

    public void OnExit()
    {
        gunsScreen.transform.LeanScale(Vector2.zero, 0.4f); //LeenTween Animation
        Invoke("SetGunScreenOff", 0.5f);
    }
    void SetGunScreenOff() //for leenTween animation
    {
        selectionScreen.SetActive(true);
        gunsScreen.SetActive(false);
    }

    public void OnUpgrade()
    {
        //change value in template also the update the ui
        string tempGunName = guns[gunNumber].gun_name;

        Save_Load_Guns.instance.SaveGunsTraits(tempGunName + "Damage", guns[gunNumber].damage+1);
        guns[gunNumber].damage += 1;
        Save_Load_Guns.instance.SaveGunsTraits(tempGunName + "firerate", guns[gunNumber].firerate - 0.1f);
        guns[gunNumber].firerate -= 0.1f;
        Save_Load_Guns.instance.SaveGunsTraits(tempGunName + "Level", guns[gunNumber].level +1);
        guns[gunNumber].level += 1;

        UpdateTraitValues();
    }

    public void OnEquip()
    {
        Save_Load_Guns.instance.SaveEquippedGun(guns[gunNumber].gun_name);
        UpdateTemplate();
        PlayerPrefs.SetString("EquippedWeapon", "Gun");
    }

    public void OnBuy()
    {
        //enable equip button and subtract money
        buyButton.SetActive(false);
        equipButton.SetActive(true);

        PlayerPrefs.SetString(guns[gunNumber].gun_name + "Unlocked", "Unlocked");
    }

    void UpdateTraitValues()
    {
        damage.text = "Damage : " + Save_Load_Guns.instance.LoadGunsTraits(guns[gunNumber].gun_name + "Damage").ToString();
        fireRate.text = "Firerate : " + Save_Load_Guns.instance.LoadGunsTraits(guns[gunNumber].gun_name + "firerate").ToString();
        level.text = "Level - " + Save_Load_Guns.instance.LoadGunsTraits(guns[gunNumber].gun_name + "Level").ToString();
    }

    void EnableGunImage()
    {
        for(int i = 0; i < gunImages.Length; i++)
        {
            gunImages[i].gameObject.SetActive(false);
        }
        gunImages[gunNumber].gameObject.SetActive(true);
    }

    void LoadMoney()
    {
        if (PlayerPrefs.HasKey("Money"))
            money.text = PlayerPrefs.GetInt("Money").ToString();
        else
            money.text = "0";

    }

    void UpdateTemplate()
    {
        string tempGunName = guns[gunNumber].gun_name;

        if (PlayerPrefs.HasKey(tempGunName + "Damage"))
        {
            guns[gunNumber].damage =Save_Load_Guns.instance.LoadGunsTraits(tempGunName+"Damage");
            guns[gunNumber].firerate =Save_Load_Guns.instance.LoadGunsTraits(tempGunName+"firerate");
            guns[gunNumber].level =(int) Save_Load_Guns.instance.LoadGunsTraits(tempGunName + "Level");
        }

        if (PlayerPrefs.GetString("EquippedGun") == guns[gunNumber].gun_name)
        {
            buyButton.SetActive(false);
            equipButton.SetActive(false);
            equippedButton.SetActive(true);
            //replace equip with equipped
        }
        else
        {
            if (PlayerPrefs.HasKey(guns[gunNumber].gun_name + "Unlocked"))
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

    void ClearPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}
