using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonForceTest : MonoBehaviour
{
    public Button testButton;

    void Start()
    {
        // 创建测试按钮
        CreateTestButton();
    }

    void CreateTestButton()
    {
        GameObject btnObj = new GameObject("ForceTestButton", typeof(RectTransform), typeof(Image), typeof(Button));
        btnObj.transform.SetParent(transform, false);

        RectTransform rect = btnObj.GetComponent<RectTransform>();
        rect.anchoredPosition = new Vector2(0, 200);
        rect.sizeDelta = new Vector2(200, 50);

        btnObj.GetComponent<Image>().color = Color.red;

        Button btn = btnObj.GetComponent<Button>();
        btn.onClick.AddListener(() => {
            Debug.Log("🔴🔴🔴 强制测试按钮点击成功！");
            btnObj.GetComponent<Image>().color = Color.green;
        });

        Debug.Log("强制测试按钮已创建，位置在屏幕上方");
    }
}
