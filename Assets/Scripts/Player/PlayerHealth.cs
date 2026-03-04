using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public TMP_Text heathText;
    public Animator healthTextAnim;
    private void Start()
    {
        // 1. 检查StatsManager单例是否存在
        if (StatsManager.Instance == null)
        {
            Debug.LogError("PlayerHealth: 未找到StatsManager单例，请检查场景中是否存在StatsManager对象！");
            return;
        }

        // 2. 检查UI组件是否赋值
        if (heathText == null)
        {
            Debug.LogError("PlayerHealth: heathText未在Inspector中赋值！");
            return;
        }
        heathText.text = "Hp:" + StatsManager.Instance.currentHeath + "/" + StatsManager.Instance.maxHeath;
    }

    public void ChangeHeath(int amount)
    {
        // 同样添加空引用检查
        if (StatsManager.Instance == null || heathText == null)
        {
            Debug.LogError("PlayerHealth: 核心组件未初始化，无法执行ChangeHeath操作！");
            return;
        }

        StatsManager.Instance.currentHeath += amount;


        healthTextAnim.Play("TextUpdate");
        heathText.text = "Hp:" + StatsManager.Instance.currentHeath + " / " + StatsManager.Instance.maxHeath;

        if (StatsManager.Instance.currentHeath <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
