using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ImageClick : MonoBehaviour, IPointerClickHandler
{
    private Image image;

    void Start()
    {
        // 获取 Image 组件
        image = GetComponent<Image>();

        // 检查是否已经绑定 Button 或 Event Trigger 组件
        if (GetComponent<Button>() != null || GetComponent<EventTrigger>() != null)
        {
            Debug.LogError("Please remove Button or EventTrigger component from the Image object before adding the ClickableRing script!");
            return;
        }

        // 重置 Image 的 sprite 和 type，并启用 raycastTarget
        image.sprite = null;
        image.type = Image.Type.Simple;
        image.raycastTarget = true;

        // 添加指针点击监听器
        AddClickListener();
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

    public void OnPointerClick(PointerEventData eventData)
    {
        // 获取点击位置，并从中心点计算距离
        Vector2 clickPos = eventData.position;
        float distanceToCenter = Vector2.Distance(clickPos, (Vector2)image.rectTransform.position);

        // 如果距离超出圆环的限制范围，则不处理该事件
        float outerRadius = image.rectTransform.sizeDelta.x / 2.0f;
        float innerRadius = outerRadius - 20.0f; // 修改内环半径的值来控制圆环大小
        if (distanceToCenter > outerRadius || distanceToCenter < innerRadius)
        {
            return;
        }

        // 处理圆环区域内的点击事件
        Debug.Log("111");
    }

    public void OnPointerClickDelegate(PointerEventData eventData)
    {
        OnPointerClick(eventData);
    }
}
