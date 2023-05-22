using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class AddRingButtonPrefeb : MonoBehaviour
{
    private RunePanelController runePanelController; // 保存 PanelControl 组件的引用
    List<string> ringDictionary;
    void Start()
    {
        // 获取 AddRingButtonPrefeb 中的所有 Button 组件
        Button[] buttons = GetComponentsInChildren<Button>();
        runePanelController = FindObjectOfType<RunePanelController>(); // 获取 PanelControl 组件的引用
        ringDictionary = runePanelController.ringDictionary;
        // 遍历并为每个按钮添加“onClick”监听器
        foreach (var button in buttons)
        {
            button.onClick.AddListener(delegate { onButtonClicked(button); });
        }
    }

    // 打印按钮名称的方法
    void onButtonClicked(Button button)
    {
        string ringName = button.GetComponentInChildren<Text>().text.ToString();
        int ringKind = ringDictionary.IndexOf(ringName);
        runePanelController.AddRingCommon(ringKind);
    }
}

