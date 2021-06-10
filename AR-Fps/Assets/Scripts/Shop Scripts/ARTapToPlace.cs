using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class ARTapToPlace : MonoBehaviour
{
    [Header("SHOP")]
    public GameObject[] ShopsList;
    private GameObject gameObjectToInstantiate;
    public Shopping shoppingScript;
    public bool isShopPlaced;


    private ShopInstructions shopInstructions;
    private GameObject spawnedObject;
    private ARRaycastManager _arRaycastManager;
    private Vector2 touchPosition;

    static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private ARPlaneManager _arPlaneManager;

    private void OnEnable()
    {
        _arRaycastManager = GetComponent<ARRaycastManager>();
        _arPlaneManager = GetComponent<ARPlaneManager>();
        shopInstructions = GetComponent<ShopInstructions>();

        shoppingScript.enabled = false;
        _arPlaneManager.enabled = true;
        shopInstructions.enabled = true;
        isShopPlaced = false;

        ShopToBePlaced(); //to set the selected shop ast the game obj to instantiate
    }

    private void ShopToBePlaced() //setting the gameobjectToInstntiate to the shop selected;
    {
        Debug.Log(ShopManager.instance.shopselected);
        gameObjectToInstantiate = ShopsList[ShopManager.instance.shopselected];
    }

    bool TryGetTouchPosition(out Vector2 touchPosition)
    {
        if (Input.touchCount > 0)
        {
            touchPosition = Input.GetTouch(index:0).position;
            return true;
        }
        touchPosition = default;
        return false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            ShopPlaced();

        if (!TryGetTouchPosition(out Vector2 touchPosition))
            return;

        if (_arRaycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon))
        {
            var hitPose = hits[0].pose;
            if (spawnedObject == null)
            {
                //rotate it 180 degrees and also make it a little forward
                spawnedObject = Instantiate(gameObjectToInstantiate, new Vector3(hitPose.position.x, hitPose.position.y + 1.351202f, hitPose.position.z), gameObjectToInstantiate.transform.rotation);
                //spawnedObject= Instantiate(gameObjectToInstantiate, hitPose.position,hitPose.rotation);

                isShopPlaced = true;

                _arPlaneManager.enabled = false;
                RemoveInstructions();
                shoppingScript.enabled = true;
            }
        }
    }
    public void OnRespawn()
    {
        spawnedObject = null;
        shoppingScript.enabled = false;
        _arPlaneManager.enabled = true;
    }

    //Remove Later
    void ShopPlaced()
    {
        Instantiate(gameObjectToInstantiate, new Vector3(0,0,0), gameObjectToInstantiate.transform.rotation);
        RemoveInstructions();
        isShopPlaced = true;
        shoppingScript.enabled = true;
    }

    void RemoveInstructions()
    {
        if (!PlayerPrefs.HasKey("ShopInstruction"))
        {
            shopInstructions.pointer.SetActive(false);
            shopInstructions.instruction2Text.SetActive(false);
            shopInstructions.selectionScreen.SetActive(true);

            StartCoroutine(shopInstructions.Instructions2());
        }
        else
        {
            shopInstructions.pointer.SetActive(false);
            shopInstructions.instruction2Text.SetActive(false);
            shopInstructions.selectionScreen.SetActive(true);
        }

        PlayerPrefs.SetString("ShopInstruction", "Done");
    }

    
}
