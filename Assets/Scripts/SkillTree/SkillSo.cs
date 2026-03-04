using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="NewSkill" , menuName = "SkillTree/Skill" )]
public class SkillSo : ScriptableObject //让组件可以添加到游戏对象上
{

    public string skillName;
    public int maxLevel;
    public Sprite skillIcon;


}
