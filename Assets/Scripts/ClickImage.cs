using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ClickImage : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // 在 Inspector 面板中指定要打印的数字
    private Button button;

    private void Start()
    {
        // // 获取 Image 组件
        // Image image = GetComponent<Image>();
        // Debug.Log("Start");
        // // 获取 EventTrigger组件
        // EventTrigger trigger = image.GetComponent<EventTrigger>();

        // if (trigger == null) // 如果该 Image GameObject 上没有 Event Trigger 组件，添加一个
        // {
        //     trigger = image.gameObject.AddComponent<EventTrigger>();
        // }
        // // 创建 Entry
        // EventTrigger.Entry entry = new EventTrigger.Entry();
        // entry.eventID = EventTriggerType.PointerEnter; // 设置为鼠标移入事件
        // // 添加回调方法
        // entry.callback.AddListener((eventData) => PrintNumber());

        // // 将 Entry 添加到 Event Trigger 组件中进行注册
        // trigger.triggers.Add(entry);

        // button = GetComponent<Button>();

        // // 添加指针进入监听器
        // EventTrigger trigger = button.gameObject.AddComponent<EventTrigger>();
        // EventTrigger.Entry entry = new EventTrigger.Entry();
        // entry.eventID = EventTriggerType.PointerEnter;
        // entry.callback.AddListener((data) => { PrintNumber(); });
        // trigger.triggers.Add(entry);
    }
    public bool isOver = false;

    // public void OnPointerEnter(PointerEventData eventData)
    // {
    //     Debug.Log("Mouse enter");
    //     isOver = true;
    // }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Mouse exit");
        isOver = false;
    }


    public void PrintNumber()
    {
        // 输出日志信息
        Debug.Log("111111111111");
    }
    void ringButtonOnClick()
    {
        PrintNumber();
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        // 把事件传递到回调函数中
        // 该方法由 IPointerEnterHandler 接口所实现
        PrintNumber();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("ImageHover - Update: " + Time.time);
        }
    }
}

// using UnityEngine;

// public class ClickImage : MonoBehaviour
// {

//     void Update()
//     {

//         if (Input.GetMouseButtonDown(0))

//             // if (GetComponent<PolygonCollider2D>().OverlapPoint(Input.mousePosition))

//             print("点击到图片");
//     }

// }
