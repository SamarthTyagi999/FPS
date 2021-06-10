using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    [Header("Values to Assign")]
    public GameObject arCamera;
    public Camera Camera;
    public GunsTemplate gunTemplate;
    private ReloadGun ammoScript;
    public ObjectPool bulletPool;
    private Enemy enemy;
    private EnemyTutorial enemytutorial;
    public Transform muzzle;                   //spawn position for the bullet

    [Header("Attributes")]
    public int curAmmo;                        //current ammo available
    public int maxAmmo;                        // maximum ammo
    public bool infiniteAmmo;
    public float bulletSpeed;
    public float gunDamage;
    public float shootRate;                    // intervals between 2 shots
    private float lastShootTime;
    private bool isPlayer;                     // are we the players weapon


    [Header("Visual Effects")]
    public AudioClip shootSfx;
    private AudioSource audioSource;
    public GameObject hitParticle;
    public ParticleSystem muzzleFlash;
    public Animator anim;

    public bool isTutorial;
    
    private void Awake()
    {
        //are we attached to the player
        //if (GetComponent<Player>())
            //isPlayer = true;

        audioSource = GetComponent<AudioSource>();
        ammoScript = GetComponent<ReloadGun>();

        SetGunTraits();
    }

    //can we shoot a bullet
    public bool CanShoot()
    {
        if (Time.time - lastShootTime >= shootRate)
        {

            if(ammoScript.currentAmmo>0 || infiniteAmmo == true)
            {
                return (true);
            }
        }
        return false;
    }

    public void Shoot()
    {
        lastShootTime = Time.time;
        ammoScript.currentAmmo--;

        muzzleFlash.Play();
        anim.SetTrigger("Shoot");
        audioSource.PlayOneShot(shootSfx);

        
        //getting bullet from the objectPool i.e from the list of bullet prefabs that are currently not active
        GameObject bullet = bulletPool.GetObject();

        ShootBulletAtCenter(bullet);//move the path of bullet to the center
        
        RaycastHit hit;

        if (Physics.Raycast(arCamera.transform.position, arCamera.transform.forward, out hit))
        {
            Debug.Log(hit.transform.root.tag);           
            if (hit.transform.root.gameObject.tag == "Target")
            {
                if(hit.transform.root.gameObject.GetComponent<Enemy>().zombieName != "SmallZombie")
                {
                    GameObject obj = Instantiate(hitParticle, hit.point, Quaternion.identity,hit.transform.root.transform);
                    Destroy(obj, 0.5f);
                }
                

                //inflict damage on enemy
                if (!isTutorial) //Not Tutorial
                {
                    enemy = hit.transform.root.gameObject.GetComponent<Enemy>();
                    if (hit.transform.gameObject.tag == "Head")
                    {
                        enemy.TakeDamage(gunDamage*1.5f);
                        GameUI.instance.HeadShot();
                    }
                    else
                    {
                        enemy.TakeDamage(gunDamage);
                    }
                    
                }
                else   
                {
                    enemytutorial = hit.transform.root.gameObject.GetComponent<EnemyTutorial>();
                    enemytutorial.TakeDamage(gunDamage);
                }
                
                bullet.SetActive(false);
            }
            else
            {
                //bullet Holes
            }
        }
    }


    public void ReloadGun()
    {
        ammoScript.OnReload();
    }

    void ShootBulletAtCenter(GameObject bullet)
    {
        Ray ray = Camera.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        RaycastHit hit;
        // Check whether your are pointing to something so as to adjust the direction
        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit))
            targetPoint = hit.point;
        else
            targetPoint = ray.GetPoint(1000);

        bullet.transform.position = muzzle.transform.position;
        bullet.transform.rotation = muzzle.transform.rotation;

        bullet.GetComponent<Rigidbody>().velocity = (targetPoint - muzzle.transform.position).normalized * bulletSpeed;
    }


    void SetGunTraits()
    {
        if(PlayerPrefs.HasKey(gunTemplate.gun_name + "Damage"))
        {
            gunDamage = PlayerPrefs.GetFloat(gunTemplate.gun_name + "Damage");
            shootRate = PlayerPrefs.GetFloat(gunTemplate.gun_name + "firerate");
        }
       
    }
}
