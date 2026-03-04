using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public Player_Combat combat;


    private void OnEnable()
    {
        SkillsSlot.OnAbilityPointSpent += HandleAbilityPointSpent;
    }

    private void OnDisable()
    {
        SkillsSlot.OnAbilityPointSpent -= HandleAbilityPointSpent;
    }


    private void HandleAbilityPointSpent(SkillsSlot slot)
    {
        string skillName = slot.skillSo.skillName;

        switch(skillName)
        {
            case "MaxHeathBoost":
                StatsManager.Instance.UpdateMaxHeath(1);
                break;

            case "Sword Slash":
                combat.enabled = true;
                break;

            default:
                Debug.LogWarning("Unknown skill:" + skillName);
                    break;
        }
    }
}
