using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


// 角色状态枚举（包含减速状态）
public enum CharacterState
{
    Normal,
    Slowed,
    Stunned
    // 可扩展其他状态：沉默、眩晕等
}

// 角色基类（所有游戏角色的通用属性）
public class Character : MonoBehaviour
{
    // 基础属性
    public string characterName;
    public float maxHealth = 1000f;
    public float currentHealth;
    public float baseMoveSpeed = 350f; // 基础移动速度
    public float currentMoveSpeed;     // 当前移动速度
    public bool isEnemy;               // 是否为敌方角色

    // 状态管理
    public CharacterState currentState;
    private float slowDuration;        // 减速剩余持续时间
    private float slowPercent;         // 减速百分比（0-1，0.3=减速30%）
    private Coroutine slowCoroutine;   // 减速协程引用

    private void Awake()
    {
        currentHealth = maxHealth;
        currentMoveSpeed = baseMoveSpeed;
        currentState = CharacterState.Normal;
    }

    // 应用减速效果
    public void ApplySlow(float slowPercent, float duration)
    {
        // 防止减速百分比超出合理范围
        slowPercent = Mathf.Clamp01(slowPercent);

        // 如果已有减速效果，可选择刷新持续时间或叠加（这里选择刷新）
        if (slowCoroutine != null)
        {
            StopCoroutine(slowCoroutine);
        }

        this.slowPercent = slowPercent;
        slowDuration = duration;
        currentState = CharacterState.Slowed;

        // 计算减速后的速度
        currentMoveSpeed = baseMoveSpeed * (1 - slowPercent);

        // 启动减速倒计时协程
        slowCoroutine = StartCoroutine(SlowCountdown());
    }

    // 减速倒计时协程
    private IEnumerator SlowCountdown()
    {
        while (slowDuration > 0)
        {
            slowDuration -= Time.deltaTime;
            yield return null;
        }

        // 减速结束，恢复原始速度
        currentMoveSpeed = baseMoveSpeed;
        currentState = CharacterState.Normal;
        slowCoroutine = null;
    }

    // 受到攻击的方法（由攻击方调用）
    public void TakeDamage(float damage, float slowPercent = 0, float slowDuration = 0)
    {
        currentHealth = Mathf.Max(0, currentHealth - damage);
        Debug.Log($"{characterName} 受到 {damage} 点伤害，剩余血量: {currentHealth}");

        // 如果配置了减速参数，触发减速
        if (slowPercent > 0 && slowDuration > 0)
        {
            ApplySlow(slowPercent, slowDuration);
            Debug.Log($"{characterName} 被减速 {slowPercent * 100}%，持续 {slowDuration} 秒");
        }

        // 血量为0时触发死亡逻辑
        if (currentHealth <= 0)
        {
            OnDeath();
        }
    }

    // 死亡逻辑
    private void OnDeath()
    {
        Debug.Log($"{characterName} 已死亡");
        // 可扩展：播放死亡动画、移除角色、触发计分等
    }
}

// 攻击系统（挂载到攻击角色上）
public class AttackSystem : MonoBehaviour
{
    public Character attacker;          // 攻击方角色
    public float attackDamage = 100f;   // 基础攻击力
    public float attackRange = 2f;      // 攻击范围
    public LayerMask enemyLayer;        // 敌方图层（用于检测）

    // 攻击配置（减速相关）
    public bool isAttackSlow = true;    // 是否开启攻击减速
    public float slowPercent = 0.3f;    // 减速30%
    public float slowDuration = 2f;     // 减速持续2秒

    // 发起攻击
    public void Attack(Character target)
    {
        // 校验攻击合法性：目标存在、是敌方、在攻击范围内
        if (target == null || !target.isEnemy || !IsInAttackRange(target.transform))
        {
            Debug.Log("攻击失败：目标不合法或超出范围");
            return;
        }

        // 调用目标的受击方法，传递伤害和减速参数
        target.TakeDamage(attackDamage,
                          isAttackSlow ? slowPercent : 0,
                          isAttackSlow ? slowDuration : 0);

        // 可扩展：播放攻击动画、播放音效、生成攻击特效等
        PlayAttackEffect();
    }

    // 检测目标是否在攻击范围内
    private bool IsInAttackRange(Transform targetTransform)
    {
        float distance = Vector3.Distance(transform.position, targetTransform.position);
        return distance <= attackRange;
    }

    // 播放攻击特效（示例）
    private void PlayAttackEffect()
    {
        Debug.Log($"{attacker.characterName} 发起攻击！");
        // 可扩展：实例化攻击粒子特效、播放攻击音效等
    }

    // （可选）自动检测范围内敌人并攻击
    public void AutoAttackNearestEnemy()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRange, enemyLayer);
        if (hitColliders.Length > 0)
        {
            // 取第一个检测到的敌人进行攻击
            Character target = hitColliders[0].GetComponent<Character>();
            if (target != null)
            {
                Attack(target);
            }
        }
    }
}
