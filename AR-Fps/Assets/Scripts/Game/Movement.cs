using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Player player;
    public float moveSpeed = 1f;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }
    void Update()
    {
        ///transform.Translate(Vector3.up * Time.deltaTime * 0.2f);
        
        if(Vector3.Distance(transform.position,player.transform.position)>=2f)
            Move();
    }

    void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
        
       
    }
}
