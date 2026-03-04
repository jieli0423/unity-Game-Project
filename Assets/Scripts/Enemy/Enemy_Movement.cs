using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public class Enemy_Movement : MonoBehaviour
{

    public float speed;
    public float attackingRange = 2;
    public float attackCooldown = 2;
    public float playerDetectRange = 5;
    public Transform detectionPoint;
    public LayerMask playerLayer;

    private float attackCooldownTimer;
    private int facingDriction = -1;
    private EnemyStart enemyState; 
    
    private Rigidbody2D rb;
    private Transform player;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        ChangeState(EnemyStart.Idle);
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyState != EnemyStart.Knockback)
        {

            CheckForPlayer();

            if (attackCooldown > 0)
            {
                attackCooldownTimer -= Time.deltaTime;
            }
            if (enemyState == EnemyStart.Chasing)
            {
                Chase();
            }
            else if (enemyState == EnemyStart.Attacking)
            {
                //攻击逻辑
                rb.velocity = Vector2.zero;
            }
        }
    }

    void Chase()
    {
        if(Vector2.Distance(transform.position,player.transform.position) <= attackingRange && attackCooldownTimer <= 0)
        {
            attackCooldownTimer = attackCooldown;
            ChangeState(EnemyStart.Attacking);
        }
        
        else if (player.position.x > transform.position.x && facingDriction == -1 ||
            player.position.x < transform.position.x && facingDriction == 1)
        {
            Flip();
        }
        Vector2 direction = (player.position - transform.position).normalized;
        rb.velocity = direction * speed;
    }
    void Flip()
    {
        facingDriction *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }
    private void CheckForPlayer()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(detectionPoint.position, playerDetectRange, playerLayer);
        if (hits.Length > 0)
        {
            player = hits[0].transform;
            // 检测玩家是否在攻击范围内并且敌人的攻击冷却是否好了
            if (Vector2.Distance(transform.position, player.position) <= attackingRange && attackCooldownTimer <= 0)
            {
                attackCooldownTimer = attackCooldown;
                ChangeState(EnemyStart.Attacking);
            }

            else if (Vector2.Distance(transform.position, player.position) > attackingRange && enemyState != EnemyStart.Attacking)
            {
                ChangeState(EnemyStart.Chasing);
            }
        }
        else
        {
            rb.velocity = Vector2.zero;
            ChangeState(EnemyStart.Idle);
        }

    }


    public void ChangeState(EnemyStart newState)
    {
        //退出当前动画
        if (enemyState == EnemyStart.Idle)
            anim.SetBool("isIdle", false);
        else if (enemyState == EnemyStart.Chasing)
            anim.SetBool("isChasing", false);
        else if (enemyState == EnemyStart.Attacking)
            anim.SetBool("isAttacking", false);

        //更新当前状态
        enemyState = newState;
        
        //更新新的动画
        if (enemyState == EnemyStart.Idle)
            anim.SetBool("isIdle",true);
        else if (enemyState == EnemyStart.Chasing)
            anim.SetBool("isChasing", true);
        else if (enemyState == EnemyStart.Attacking)
            anim.SetBool("isAttacking", true);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(detectionPoint.position, playerDetectRange);
    }
}
public enum EnemyStart
{
    Idle,
    Chasing,
    Attacking,
    Knockback,
}
