using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{

    private Player player;
    private bool isDead;

    [Header("Stats/Info")]
    public string zombieName;
    public float curHp;
    public float maxHp;
    public int moneyToGive;
    public float timeToDestroy=3f;

    [Header("Movement")]
    public float moveSpeed;
    public float attackRange;
    public float yPathOffset;

    [Header("Attack")]
    public float attackTimer;
    public int attackDamage;
    public float attackDelay;
    public float attackDistance = 3f;

    [Header("Display")]
    public GameObject healthBar;
    public Image healthBarFill;
    private Animator anim;
    //public GameObject floatingDamagePrefab;

    [Header("Sounds")]
    public AudioClip attackSound;
    private AudioSource audioSource;



    private void Awake()
    {
        player = FindObjectOfType<Player>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        healthBarFill.enabled = true;
        healthBar.SetActive(true);
        UpdateHealthbar(curHp, maxHp);
    }

    private void Update()
    {
        if (!isDead)
        {
            if (Vector3.Distance(transform.position, player.transform.position) >= attackDistance && !isDead)
                Move();
            else
                Attack();
        }
        
    }

    void Move()
    {
        anim.SetBool("Walking", true);
        //transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.transform.position.x, -1.5f, player.transform.position.y), moveSpeed * Time.deltaTime);
        //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(-transform.position), 0.15F);
        //transform.eulerAngles = new Vector3(0, transform.eulerAngles.y , 0);

        Vector3 movement = new Vector3(player.transform.position.x - transform.position.x, 0f, player.transform.position.z - transform.position.z);
        transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement.normalized), 0.2f);
    }

    void Attack()
    {
        Debug.Log("zombie Attacked");
        anim.SetBool("Walking", false);
        attackTimer -= Time.deltaTime;

        if (attackTimer <= 0)
        {
            anim.SetTrigger("Punch");

            StartCoroutine(player.TakeDamage(attackDamage,attackDelay));
            attackTimer = 3.6f;
            audioSource.PlayOneShot(attackSound);
            
        }

        //attackTimer -= Time.deltaTime;
    }

    public void TakeDamage(float damage)
    {
        curHp -= damage;
        UpdateHealthbar(curHp, maxHp);

        anim.SetTrigger("Hit");

        //if(floatingDamagePrefab && curHp>0) //if it is assigned then show
            //ShowFloatingDamage();

        if (curHp <= 0)
        {
            healthBar.SetActive(false);
            Die();
        }
            
    }

    public void UpdateHealthbar(float curHp, float maxHp)
    {
        healthBar.SetActive(true);
        healthBarFill.fillAmount = (float)curHp / (float)maxHp;
        Invoke("DisableHealthBar",2f);
    }

    void DisableHealthBar()
    {
        healthBar.SetActive(false);
    }

    /*
    void ShowFloatingDamage()
    {
        var go= Instantiate(floatingDamagePrefab, transform.position,Quaternion.identity);

        if (transform.position.z < 0) //text was coming reversed
            go.transform.rotation = Quaternion.Euler(0, 210f,0);
         
        go.GetComponent<TextMesh>().text = curHp.ToString();
    }
    */

    public void Die()
    {
        isDead = true;
        anim.SetTrigger("Die");

        GameManager.instance.AddMoney(moneyToGive);
        GameUI.instance.UpdateWaveSlider(1);

        if (zombieName == "SmallZombie")
        {
            StartCoroutine(GetComponent<ZombieDeath>().DeathEffect());
        }


        if (gameObject != null)
        {
            Destroy(gameObject, timeToDestroy);
            GameManager.instance.spawner.GetComponent<EnemyContainer>().RemoveEnemy(gameObject.transform);
        }

    }
    

    
}
