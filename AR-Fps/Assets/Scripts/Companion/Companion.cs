using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Companion : MonoBehaviour
{
    private Player player;

    private GameObject spawner;
    private GameObject enemyToLook;

    private Animator anim;
    public float distanceFromPlayer;
    public float moveSpeed;

    private EnemyContainer enemyContainer;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        anim = GetComponent<Animator>();

        spawner = GameManager.instance.spawner;
        enemyContainer = spawner.GetComponent<EnemyContainer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) >= distanceFromPlayer)
            Move();       
        else
            Attack();
    }

    void Move()
    { 
        anim.SetBool("Walking", true);

        Vector3 movement = new Vector3(player.transform.position.x - transform.position.x, 0f, player.transform.position.z - transform.position.z);
        transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement.normalized), 0.2f);

    }

    void Attack()
    {
        anim.SetBool("Walking", false);

        if (GameManager.instance.enabled)
        {
            if (enemyToLook == null)
            {
                anim.SetBool("Shooting", false);
                if (enemyContainer._enemies.Count>0)
                    enemyToLook = enemyContainer._enemies[0].gameObject;
            }
            else
            {
                Vector3 dir = (enemyToLook.transform.position - transform.position).normalized;
                float angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;

                transform.eulerAngles = Vector3.up * angle;
                anim.SetBool("Shooting", true);
            }
        }
       
    }

}
