using System.Collections;
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


    void Start()
    {
        // 获取该物体下所有Scrollbar组件
        scrollbars = GetComponentsInChildren<Scrollbar>();
        inputFieldPrefeb = FindObjectOfType<InputFieldPrefeb>(); // 获取 ScrollbarPrefeb 组件的引用
        // 给每个Scrollbar添加监听
        foreach (var scrollbar in scrollbars)
        {
            scrollbar.onValueChanged.AddListener(OnScrollbarValueChanged);
            scrollbar.size = 0.01f;
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
            Debug.Log("!!!!!!!!! ");
            Debug.Log(index + ": " + scrollbar.value);
            inputFieldPrefeb.UpdateValue(index, value);
            isUpdating = false;
            // 打印该Scrollbar名字的最后一位数字
            // Debug.Log(transform.name.Substring(transform.name.Length - 1));
        }
    }
    public void UpdateValue(int index, float value)
    {
        if (!isUpdating)
        {
            isUpdating = true;
            scrollbars[index].value = value;
            isUpdating = false;
        }
        // Debug.Log(transform.name.Substring(transform.name.Length - 1));
    }
}
