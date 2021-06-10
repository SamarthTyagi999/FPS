using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public GameObject objPrefab;                  //object to be instantiated
    public int createOnStart;                     //number of the objects it has to be made


    private List<GameObject> pooledObjs = new List<GameObject>();    //list don't need a initial number to be initialized, they are dynamic than arrays

    private void Start()         // Create the default numbers needed at start
    {
        for (int x = 0; x < createOnStart; x++)
        {
            CreateNewObject();
        }
    }

    GameObject CreateNewObject()
    {
        GameObject obj = Instantiate(objPrefab);
        obj.SetActive(false);
        pooledObjs.Add(obj);

        return obj;
    }

    public GameObject GetObject()       //returns the available prefab in the list if null then create a new prefab
    {
        GameObject obj = pooledObjs.Find(x => x.activeInHierarchy == false);

        if (obj == null)
        {
            obj = CreateNewObject();
        }

        obj.SetActive(true);

        return obj;
    }
}
