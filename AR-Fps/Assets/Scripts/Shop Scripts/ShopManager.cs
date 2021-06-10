using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ShopManager : MonoBehaviour
{
    public GameObject shopChoiceScreen;
    public int shopselected=0;

    ARTapToPlace arTapToPlace;
    ARPlaneManager arPlaneManager;
    ShopInstructions shopInstructions;

    public static ShopManager instance;

    private void Awake()
    {
        instance = this;
        arTapToPlace = GetComponent<ARTapToPlace>();
        arPlaneManager = GetComponent<ARPlaneManager>();
        shopInstructions = GetComponent<ShopInstructions>();

        arTapToPlace.enabled = false;
        arPlaneManager.enabled = false;
        shopInstructions.enabled = false;
        shopChoiceScreen.SetActive(true);
    }
    public void OnShopSelectButton(int shopNumber) 
    {
        shopselected = shopNumber;                      //selected shop number
        arTapToPlace.enabled = true;                     //enable scanning the surface
        shopChoiceScreen.SetActive(false);           //Disable the shop selection menu
    }


}
