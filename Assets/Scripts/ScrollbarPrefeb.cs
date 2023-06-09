﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

public class ScrollbarPrefeb : MonoBehaviour
{
    // Start is called before the first frame update
    private InputFieldPrefeb inputFieldPrefeb;
    public bool isUpdating = false;
    private Scrollbar[] scrollbars;
    public int scrollbarSize;
    private RunePanelController runePanelController; // 保存 PanelControl 组件的引用


    void Start()
    {
        // 获取该物体下所有Scrollbar组件
        scrollbars = GetComponentsInChildren<Scrollbar>();
        inputFieldPrefeb = FindObjectOfType<InputFieldPrefeb>(); // 获取 ScrollbarPrefeb 组件的引用
        runePanelController = FindObjectOfType<RunePanelController>(); // 获取 PanelControl 组件的引用
        scrollbarSize = 100;
        // 给每个Scrollbar添加监听
        foreach (var scrollbar in scrollbars)
        {
            scrollbar.onValueChanged.AddListener(OnScrollbarValueChanged);
            scrollbar.size = 1.0f / (float)scrollbarSize;
            // scrollbar.numberOfSteps = 100;
        }

    }

    // 当Scrollbar数值改变时调用的方法
    void OnScrollbarValueChanged(float value)
    {
        // 获取当前调用该方法的Scrollbar的名字
        if (!isUpdating && EventSystem.current.currentSelectedGameObject != null)
        {
            isUpdating = true;
            Scrollbar scrollbar = EventSystem.current.currentSelectedGameObject.GetComponent<Scrollbar>();
            string scrollbarName = "";
            scrollbarName = scrollbar.name;
            // 打印该Scrollbar名字的最后一位数字
            int index = int.Parse(scrollbarName.Substring(scrollbarName.Length - 1));
            List<int> ringProperty = runePanelController.ringProperties[runePanelController.selectedRingIndex];
            // 有个2是因为ringProperty前两位是环的序号和种类，不进行直接修改。
            ringProperty[index + 2] = (int)(value * scrollbarSize);
            runePanelController.ringProperties[runePanelController.selectedRingIndex] = ringProperty;
            //在更新完环属性后，再更新inputField
            inputFieldPrefeb.UpdateValue();
            isUpdating = false;
            // 打印该Scrollbar名字的最后一位数字
            // Debug.Log(transform.name.Substring(transform.name.Length - 1));
        }
    }
    public void UpdateValue()
    {
        if (!isUpdating)
        {
            isUpdating = true;
            List<int> ringProperty = runePanelController.ringProperties[runePanelController.selectedRingIndex];
            for (int i = 2; i < ringProperty.Count; i++)
            {
                scrollbars[i - 2].value = (float)ringProperty[i] / (float)scrollbarSize;
            }
            isUpdating = false;
        }
        // Debug.Log(transform.name.Substring(transform.name.Length - 1));
    }
}
