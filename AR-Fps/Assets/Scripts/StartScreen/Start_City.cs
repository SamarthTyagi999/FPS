using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Start_City : MonoBehaviour
{
    public GameObject arCamera;
    int layer_mask;
    int leanID;

    [Header("City Signs")]
    private GameObject startSign, shopSign, tutorialSign;
    PlaceCity placeCity;
    Vector3 originalSignsTransform;

    void OnEnable()
    {
        layer_mask = LayerMask.GetMask("CitySigns");
        leanID = -9999;

        placeCity = GetComponent<PlaceCity>();
        startSign = placeCity.spawnedObject.transform.GetChild(1).gameObject;
        shopSign = placeCity.spawnedObject.transform.GetChild(2).gameObject;
        tutorialSign = placeCity.spawnedObject.transform.GetChild(3).gameObject;

        originalSignsTransform = new Vector3(0.18f, 0.18f, 0.18f);
    }

    // Update is called once per frame
    void Update()
    {
        HighlightChoice();
    }

    void HighlightChoice()
    {
        startSign.transform.gameObject.GetComponent<TextMesh>().color = Color.white;
        shopSign.transform.gameObject.GetComponent<TextMesh>().color = Color.white;
        tutorialSign.transform.gameObject.GetComponent<TextMesh>().color = Color.white;


        RaycastHit hit;
        if (Physics.Raycast(arCamera.transform.position, arCamera.transform.forward, out hit, layer_mask))
        {
            if (hit.transform.gameObject.name == "StartGameSign")
            {
                hit.transform.gameObject.GetComponent<TextMesh>().color = Color.green;
                if (leanID == -9999)
                    leanID = hit.transform.LeanScale(new Vector3(0.24f, 0.24f, 0.24f), .6f).setLoopPingPong().id;
            }
            else if (hit.transform.gameObject.name == "ShopsSign")
            {
                hit.transform.gameObject.GetComponent<TextMesh>().color = Color.yellow;
                if (leanID == -9999)
                    leanID = hit.transform.LeanScale(new Vector3(0.24f, 0.24f, 0.24f), .6f).setLoopPingPong().id;
            }
            else if (hit.transform.gameObject.name == "Tutorial Sign")
            {
                hit.transform.gameObject.GetComponent<TextMesh>().color = Color.red;
                if (leanID == -9999)
                    leanID = hit.transform.LeanScale(new Vector3(0.24f, 0.24f, 0.24f), .6f).setLoopPingPong().id;
            }
            else
            {
                LeanTween.cancel(leanID);
                leanID = -9999;
                startSign.transform.localScale = shopSign.transform.localScale = tutorialSign.transform.localScale = originalSignsTransform;
            }
        }
    }


    public void OnSelect()
    {
        RaycastHit hit;
        if (Physics.Raycast(arCamera.transform.position, arCamera.transform.forward, out hit))
        {
            if (hit.transform.gameObject.name == "StartGameSign")
            {
                SceneManager.LoadScene("GameScene");
            }
            else if (hit.transform.gameObject.name == "ShopsSign")
            {
                SceneManager.LoadScene("AmmunitionShop");
            }
            else if (hit.transform.gameObject.name == "Tutorial Sign")
            {
                SceneManager.LoadScene("Tutorial");
            }
        }
    }

}
