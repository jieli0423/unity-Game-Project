using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using System;

public class SkillsSlot : MonoBehaviour
{
    public List<SkillsSlot> prerequisiteSkillSlots;
    public SkillSo skillSo;


    public int currentLevel;
    public bool isUnlocked;


    public Image skillIcon;
    public Button skillButton;
    public TMP_Text skillLevelText;

    public static event Action<SkillsSlot> OnAbilityPointSpent;
    public static event Action<SkillsSlot> OnSkillMaxed;





    private void OnValidate() //这个模块可以在你改变脚本文件时运行,更新UI
    {
        if(skillSo != null && skillLevelText != null)
        {
            UpdateUI();
        }

    }

    private void Start()
    {
        // 在 SkillsSlot.Start() 中添加
       

            Debug.Log($"技能 {skillSo?.skillName} 初始化状态:");
            Debug.Log($"- 按钮可交互: {skillButton?.interactable}");
            Debug.Log($"- 已解锁: {isUnlocked}");
            Debug.Log($"- 当前等级: {currentLevel}");
            Debug.Log($"- 最大等级: {skillSo?.maxLevel}");

        Debug.Log($"技能槽 {gameObject.name} 初始化开始");

        if (skillButton != null)
        {
            Debug.Log($"技能 {skillSo?.skillName} 的按钮存在，当前交互状态: {skillButton.interactable}");
        }
        else
        {
            Debug.LogError($"技能槽 {gameObject.name} 的按钮未赋值！");
        }

        if (skillIcon != null)
        {
            Debug.Log($"技能图标存在");
        }
        else
        {
            Debug.LogError($"技能槽 {gameObject.name} 的图标未赋值！");
        }

        if (skillLevelText != null)
        {
            Debug.Log($"技能文本存在");
        }
        else
        {
            Debug.LogError($"技能槽 {gameObject.name} 的文本未赋值！");
        }
    }

    public void UltraSimpleTest()
    {
        Debug.Log($" 按钮被点击了！技能: {skillSo?.skillName}");

        // 不管什么条件，直接强制升级
        if (currentLevel < skillSo.maxLevel)
        {
            currentLevel++;
            UpdateUI();
            Debug.Log($"强制升级到: {currentLevel}");
        }
    }
    /* public void TryUpgradeSkill()
     {
         Debug.Log($"=== TryUpgradeSkill 开始 ===");
         Debug.Log($"技能名称: {skillSo?.skillName}");
         Debug.Log($"当前解锁状态: {isUnlocked}");
         Debug.Log($"当前等级: {currentLevel}/{skillSo?.maxLevel}");

         // 检查前置条件
         bool canUnlock = CanUnlockSkill();
         Debug.Log($"是否可以解锁: {canUnlock}");

         if (!isUnlocked && canUnlock)
         {
             Debug.Log("条件满足，尝试解锁技能");
             Unlock();
         }

         if (isUnlocked && currentLevel < skillSo.maxLevel)
         {
             Debug.Log("技能已解锁且未满级，执行升级");
             currentLevel++;
             Debug.Log($"升级后等级: {currentLevel}");

             OnAbilityPointSpent?.Invoke(this);
             Debug.Log("已触发 OnAbilityPointSpent 事件");

             if (currentLevel >= skillSo.maxLevel)
             {
                 Debug.Log("技能已达最大等级，触发 OnSkillMaxed 事件");
                 OnSkillMaxed?.Invoke(this);
             }

             UpdateUI();
             Debug.Log("UI已更新");
         }
         else
         {
             if (!isUnlocked)
                 Debug.Log("升级失败：技能未解锁");
             else if (currentLevel >= skillSo.maxLevel)
                 Debug.Log("升级失败：技能已达最大等级");
         }

         Debug.Log($"=== TryUpgradeSkill 结束 ===");
     }*/
    public void TryUpgradeSkill()
    {
        if(isUnlocked && currentLevel < skillSo.maxLevel)
        {
            if (!isUnlocked && CanUnlockSkill())
            {
                Unlock();
            }
            currentLevel++;
            OnAbilityPointSpent?.Invoke(this);

            if(currentLevel >= skillSo.maxLevel)
            {
                OnSkillMaxed?.Invoke(this);
            }

            UpdateUI();
        }
    } 

    public bool CanUnlockSkill()
    {
        foreach(SkillsSlot slot in prerequisiteSkillSlots)
        {
            if(!slot.isUnlocked || slot.currentLevel < slot.skillSo.maxLevel)
            {
                return false;
            }
        }
        return true;
    }



    public void Unlock()
    {
        isUnlocked = true;
        UpdateUI();
    }



    public void UpdateUI()
    {
        skillIcon.sprite = skillSo.skillIcon;

        if(isUnlocked)
        {
            skillButton.interactable = true;
            skillLevelText.text = currentLevel.ToString() + "/" + skillSo.maxLevel.ToString();
            skillIcon.color = Color.white;
        }
        else
        {
            skillButton.interactable = false;
            skillLevelText.text = "Locked";
            skillIcon.color = Color.grey;
        }
    }


}
