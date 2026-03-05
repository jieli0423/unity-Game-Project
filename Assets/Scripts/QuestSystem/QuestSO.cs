using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestSO" , menuName = "QuestSO")]
public class QuestSO : ScriptableObject
{

    public string questName;
    [TextArea] public string questDescription;
    public int questLevel;

    public List<QuestObjective> objectives;
}

[System.Serializable]
public class QuestObjective
{
    public string description;

    [SerializeField] private Object target;
    public ActorSO targetNPC => target as ActorSO;


    public int requiredAmount;
    public int currentAmount;
}
