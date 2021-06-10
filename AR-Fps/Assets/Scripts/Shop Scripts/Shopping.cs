using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Shopping : MonoBehaviour
{
    public GameObject arCamera;
    GunsDisplay gunDisplay;
    MeeleDisplay meeleDisplay;


    [Header("Guns Lights")]
    public GameObject ak47Light;
    public GameObject pistolLight;
    public GameObject weabon10Light;
    public GameObject uziLight;
    public GameObject rpg7Light;
    public GameObject M249Light;

    [Header("Meele Lights")]
    public GameObject khandaLight;


    private void OnEnable()
    {

        if (ShopManager.instance.shopselected==0)
        {
            gunDisplay = GetComponent<GunsDisplay>();

            pistolLight = GameObject.FindGameObjectWithTag("PistolLight");
            ak47Light = GameObject.Find("AK47_Light");
            weabon10Light = GameObject.FindGameObjectWithTag("Weabon10Light");
            uziLight = GameObject.FindGameObjectWithTag("UZILight");
            rpg7Light = GameObject.FindGameObjectWithTag("RPG7Light");
            M249Light = GameObject.FindGameObjectWithTag("M249Light");
        }

        else if(ShopManager.instance.shopselected == 1){
            meeleDisplay = GetComponent<MeeleDisplay>();
            khandaLight = GameObject.Find("khanda_Light");

        }
        
        
    }

    private void LateUpdate()
    {
        if (ShopManager.instance.shopselected == 0)
            HighlightedGuns01();
        else if (ShopManager.instance.shopselected == 1)
            HighlightedMeele01();
    }

    public void SelectItem()
    {
        RaycastHit hit;
        if (Physics.Raycast(arCamera.transform.position, arCamera.transform.forward, out hit))
        {
            Debug.Log(hit.transform.name);

            if(ShopManager.instance.shopselected == 0)
            {
                if (hit.transform.gameObject.tag == "Pistol")
                    gunDisplay.DisplayGun(0);
                else if (hit.transform.gameObject.tag == "AK47")
                    gunDisplay.DisplayGun(1);
                //else if(hit.transform.gameObject.tag == "RPG7")
                    // gunDisplay.DisplayGun(4);
                //else if (hit.transform.gameObject.tag == "UZI")
                    //gunDisplay.DisplayGun(3);
                else if (hit.transform.gameObject.tag == "M249")
                    gunDisplay.DisplayGun(2);
                //if (hit.transform.gameObject.tag == "Weabon10")
                    //gunDisplay.DisplayGun(5);
            }

            else if(ShopManager.instance.shopselected == 1)
            {
                if (hit.transform.gameObject.name == "SM_Khanda")
                    meeleDisplay.DIsplayMeeleWeapon(0);
            }
        }
    }

    void HighlightedGuns01()
    {

        pistolLight.SetActive(false);
        ak47Light.SetActive(false);
        rpg7Light.SetActive(false);
        uziLight.SetActive(false);
        M249Light.SetActive(false);
        weabon10Light.SetActive(false); 


        RaycastHit hit;
        if (Physics.Raycast(arCamera.transform.position, arCamera.transform.forward, out hit))
        {
            if (hit.transform.gameObject.tag == "Pistol")
                pistolLight.SetActive(true);

            else if (hit.transform.gameObject.tag == "AK47")
                ak47Light.SetActive(true);

            else if (hit.transform.gameObject.tag == "RPG7")
                rpg7Light.SetActive(true);

            else if (hit.transform.gameObject.tag == "UZI")
                uziLight.SetActive(true);

            else if (hit.transform.gameObject.tag == "M249")
                M249Light.SetActive(true);

            else if (hit.transform.gameObject.tag == "Weabon10")
                weabon10Light.SetActive(true);
        }
    }

    void HighlightedMeele01()
    {

        khandaLight.SetActive(false);


        RaycastHit hit;
        if (Physics.Raycast(arCamera.transform.position, arCamera.transform.forward, out hit))
        {
            if (hit.transform.gameObject.name == "SM_Khanda")
                khandaLight.SetActive(true);
            
        }
    }

    public void OnExit()
    {
        SceneManager.LoadScene("GameScene");
    }

}
