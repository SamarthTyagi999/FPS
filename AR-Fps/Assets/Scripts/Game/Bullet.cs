using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;              //damage dealt to the target
    public float lifeTime;          //how long untill the bullet despawns?
    private float shootTime;        //time the bullet was shot

    //public GameObject hitParticle;
    private void OnEnable()
    {
        shootTime = Time.time; //setting it to the current time
    }

    private void Update()
    {
        //disable the bullet after 'lifeTime' seconds
        if (Time.time - shootTime >= lifeTime)
            gameObject.SetActive(false);
    }

    
    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(collision.collider.name);
        gameObject.SetActive(false);
    }
   
}
