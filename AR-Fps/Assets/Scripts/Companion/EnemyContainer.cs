using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyContainer : MonoBehaviour
{
    //public static EnemyContainer instance;
    public List<Transform> _enemies = new List<Transform>(0);

    void Start()
    {
       //instance = this;
    }
    public void AddEnemy(Transform enemy)
    {
        _enemies.Add(enemy);
    }
    public void RemoveEnemy(Transform enemy)
    {
        _enemies.Remove(enemy);
    }


}
