using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SkillTreeManager : MonoBehaviour
{
    public SkillsSlot[] skillSlots;
    public TMP_Text pointsText;

    public int availablePoints;


    private void OnEnable()
    {
        SkillsSlot.OnAbilityPointSpent += HandleAbilityPointsSpent;
        SkillsSlot.OnSkillMaxed += HandleSkillMaxed;
        ExpManager.OnLevelUp += UpdateAbilityPoints;
    }

    private void OnDisable()
    {
        SkillsSlot.OnAbilityPointSpent -= HandleAbilityPointsSpent;
        SkillsSlot.OnSkillMaxed -= HandleSkillMaxed;
        ExpManager.OnLevelUp -= UpdateAbilityPoints;
    }


    private void Start()
    {
        foreach (SkillsSlot slot in skillSlots)
        {
            slot.skillButton.onClick.AddListener(() => CheckAvailablePoints(slot));
        }

        UpdateAbilityPoints(0);

    }

    private void CheckAvailablePoints(SkillsSlot slot)
    {
        Debug.Log($"=== CheckAvailablePoints 开始 ===");
        Debug.Log($"可用点数: {availablePoints}");
        Debug.Log($"点击的技能: {slot.skillSo?.skillName}");

        if (availablePoints > 0)
        {
            Debug.Log("点数充足，尝试升级");
            slot.TryUpgradeSkill();
        }
        else
        {
            Debug.Log("点数不足，无法升级");
        }
        Debug.Log($"=== CheckAvailablePoints 结束 ===");
    }
    /*private void CheckAvailablePoints(SkillsSlot slot)
    {
        if(availablePoints > 0)
        {
            slot.TryUpgradeSkill();
        }
    }*/


    private void HandleAbilityPointsSpent(SkillsSlot skillsSlot)
    {
        if(availablePoints >0)
        {
            UpdateAbilityPoints(-1);
        }
    }

    private void HandleSkillMaxed(SkillsSlot skillsSlot)
    {
        foreach (SkillsSlot slot in skillSlots)
        {
            if (!slot.isUnlocked && slot.CanUnlockSkill())
            {
                slot.Unlock();
            }
        }
    }


    public void UpdateAbilityPoints( int amount)
    {
        availablePoints += amount;
        pointsText.text = "Points :" + availablePoints;
    }

}
