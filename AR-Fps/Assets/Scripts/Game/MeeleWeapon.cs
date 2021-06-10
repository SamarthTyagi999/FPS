using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeeleWeapon : MonoBehaviour
{
    [Header("Values to Assign")]
    public GameObject arCamera;
    public MeeleTemplate meeleTemplate;
    private Enemy enemy;
    private ComboAttack combo;


    [Header("Attributes")]
    public float meeleDamage;
    public float AttackRate;                    // intervals between 2 Attack
    private float lastAttackTime;


    //[Header("Visual Effects")]
    //public AudioClip attackSfx;
    //private AudioSource audioSource;
    //public GameObject hitParticle;
    //public Animator anim;


    private void Awake()
    {
        //audioSource = GetComponent<AudioSource>();

        //SetGunTraits();

        combo = GetComponent<ComboAttack>();
    }

    //can we shoot a bullet
    public bool CanAttack()
    {
        if (Time.time - lastAttackTime >= AttackRate)
        {
            return (true);
        }
        return false;
    }

    public void Attack()
    {
        lastAttackTime = Time.time;
        combo.PerformAttack();
        //anim.SetTrigger("Shoot");
        //audioSource.PlayOneShot(shootSfx);


        RaycastHit hit;

        if (Physics.Raycast(arCamera.transform.position, arCamera.transform.forward, out hit, 4.5f))
        {
            Debug.Log("Meele Attack"+ hit.transform.root.tag);

            if (hit.transform.root.gameObject.tag == "Target")
            {
                if (hit.transform.root.gameObject.GetComponent<Enemy>().zombieName != "SmallZombie")
                {
                    //GameObject obj = Instantiate(hitParticle, hit.point, Quaternion.identity);
                    //Destroy(obj, 0.5f);
                }

                enemy = hit.transform.root.gameObject.GetComponent<Enemy>();

                if (hit.transform.gameObject.tag == "Head")
                {
                    enemy.TakeDamage(meeleDamage * 1.5f);
                    GameUI.instance.HeadShot();
                }
                else
                {
                    enemy.TakeDamage(meeleDamage);
                }
            }
        }
    }
}
