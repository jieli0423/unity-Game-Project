using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeBugText : MonoBehaviour
{
    void Start()
    {
        // 获取Canvas
        Canvas canvas = FindObjectOfType<Canvas>();
        if (canvas == null)
        {
            Debug.LogError("找不到Canvas！");
            return;
        }

        // 创建测试按钮
        GameObject testBtn = new GameObject("终极测试按钮", typeof(RectTransform), typeof(Image), typeof(Button));
        testBtn.transform.SetParent(canvas.transform, false);

        // 设置位置和大小
        RectTransform rect = testBtn.GetComponent<RectTransform>();
        rect.anchoredPosition = Vector2.zero;
        rect.sizeDelta = new Vector2(200, 50);

        // 设置颜色
        testBtn.GetComponent<Image>().color = Color.green;

        // 添加文本
        GameObject textObj = new GameObject("Text", typeof(Text));
        textObj.transform.SetParent(testBtn.transform, false);
        Text text = textObj.GetComponent<Text>();
        text.text = "测试按钮";
        text.alignment = TextAnchor.MiddleCenter;
        text.color = Color.black;
        text.font = Resources.GetBuiltinResource<Font>("Arial.ttf");

        RectTransform textRect = textObj.GetComponent<RectTransform>();
        textRect.anchorMin = Vector2.zero;
        textRect.anchorMax = Vector2.one;
        textRect.sizeDelta = Vector2.zero;

        // 添加点击事件
        Button btn = testBtn.GetComponent<Button>();
        btn.onClick.AddListener(() => {
            Debug.Log(" 终极测试按钮点击成功！");
        });

        Debug.Log("测试按钮创建完成");
    }
}
