using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyTutorial : MonoBehaviour
{

    private PlayerTutorial player;
    private bool isDead;

    [Header("Stats")]
    public float curHp;
    public float maxHp;
    public int scoreToGive;
    public int moneyToGive;

    [Header("Movement")]
    public float moveSpeed;
    public float attackRange;
    public float yPathOffset;

    [Header("Attack")]
    public float attackTimer;
    public int attackDamage;

    [Header("Display")]
    private Animator anim;
    public GameObject healthBar;
    public Image healthBarFill;


    private void Awake()
    {
        player = FindObjectOfType<PlayerTutorial>();
        anim = GetComponent<Animator>();
    }
    private void Start()
    {
        healthBarFill.enabled = true;
        healthBar.SetActive(true);
        UpdateHealthbar(curHp, maxHp);
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) >= 3f && !isDead)
            Move();
        else
            Attack();
    }

    void Move()
    {
        anim.SetBool("Walking", true);
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.transform.position.x, -1.5f, player.transform.position.y), moveSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(-transform.position), 0.15F);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
    }

    void Attack()
    {
        anim.SetBool("Walking", false);
        //attackTimer -= Time.deltaTime;

        if (attackTimer <= 0)
        {
            anim.SetTrigger("Punch");
            //player.Hurt(2, 1);
            //player.TakeDamage(attackDamage);
            StartCoroutine(player.TakeDamage(attackDamage));
            attackTimer = 3.6f;
        }

        attackTimer -= Time.deltaTime;
    }

    public void TakeDamage(float damage)
    {
        curHp -= damage;
        UpdateHealthbar(curHp, maxHp);

        if (curHp <= 0)
        {
            healthBar.SetActive(false);
            Die();
        }

    }

    public void UpdateHealthbar(float curHp, float maxHp)
    {
        healthBarFill.fillAmount = (float)curHp / (float)maxHp;
    }

    public void Die()
    {
        //GameManager.instance.AddScore(scoreToGive);
        isDead = true;
        anim.SetTrigger("Die");

        GameManager.instance.AddMoney(moneyToGive);
        GameUI.instance.UpdateWaveSlider(1);

        if (gameObject != null)
        {
            Destroy(gameObject, 3f);
        }
    }
}
