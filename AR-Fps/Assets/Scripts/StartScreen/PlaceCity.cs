using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;


public class PlaceCity : MonoBehaviour
{
    public GameObject gameObjectToInstantiate;
    public GameObject spawnedObject;
    public GameObject selectionScreeen;

    private ARRaycastManager _arRaycastManager;
    private Vector2 touchPosition;

    static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private ARPlaneManager _arPlaneManager;

    private Start_City start_City;

    private void Awake()
    {
        _arRaycastManager = GetComponent<ARRaycastManager>();
        _arPlaneManager = GetComponent<ARPlaneManager>();
        start_City = GetComponent<Start_City>();
        start_City.enabled = false;
    }

    bool TryGetTouchPosition(out Vector2 touchPosition)
    {
        if (Input.touchCount > 0)
        {
            touchPosition = Input.GetTouch(index: 0).position;
            return true;
        }
        touchPosition = default;
        return false;
    }

    void Update()
    {

        if (!TryGetTouchPosition(out Vector2 touchPosition))
            return;

        if (_arRaycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon))
        {
            var hitPose = hits[0].pose;
            if (spawnedObject == null)
            {
                spawnedObject = Instantiate(gameObjectToInstantiate, new Vector3(hitPose.position.x, hitPose.position.y + 0f, hitPose.position.z), gameObjectToInstantiate.transform.rotation);
                //spawnedObject= Instantiate(gameObjectToInstantiate, hitPose.position,hitPose.rotation);

                _arPlaneManager.enabled = false;
                start_City.enabled = true;
                selectionScreeen.SetActive(true);

                _arPlaneManager.SetTrackablesActive(false);

            }
        }
    }
}
