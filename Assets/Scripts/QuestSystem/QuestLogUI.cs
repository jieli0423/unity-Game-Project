using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestLogUI : MonoBehaviour
{


    public void HandleQuestClicked(QuestSO questSO)
    {
        Debug.Log($"Clicked Quest: {questSO.questName} ");

        foreach(var objective in questSO.objectives)
        {
            Debug.Log($"Objective:{objective.description}");
        }
    }


}
