﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class RingPrefeb : MonoBehaviour, IPointerClickHandler
{
    public Vector3 Position;
    public float insideRadius;
    public float outsideRadius;
    public UnityEvent onRingClicked;
    private RunePanelController runePanelController; // 保存 PanelControl 组件的引用
    public int index = 0;
    public float radius;
    private Image image;
    void Start()
    {
        image = GetComponent<Image>();

        // 重置 Image 的 sprite 和 type，并启用 raycastTarget
        // image.type = Image.Type.Simple;
        image.raycastTarget = true;

        //添加指针点击监听器
        AddClickListener();
        runePanelController = FindObjectOfType<RunePanelController>(); // 获取 PanelControl 组件的引用
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        // 获取点击位置，并从中心点计算距离
        Vector2 clickPos = eventData.position;
        float distanceToCenter = Vector2.Distance(clickPos, (Vector2)image.rectTransform.position);
        runePanelController.OnMouseClick(distanceToCenter);
    }
    void AddClickListener()
    {
        // 获取 EventTrigger 组件，如果不存在就添加一个
        EventTrigger eventTrigger = gameObject.GetComponent<EventTrigger>() ?? gameObject.AddComponent<EventTrigger>();

        // 创建 PointerClick 事件类型的 Entry
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;

        // 添加监听器
        entry.callback.AddListener((data) => { OnPointerClickDelegate((PointerEventData)data); });

        // 将 Entry 添加到 EventTrigger 的 triggers 列表中
        eventTrigger.triggers.Add(entry);
    }
    public void OnPointerClickDelegate(PointerEventData eventData)
    {
        OnPointerClick(eventData);
    }

}
