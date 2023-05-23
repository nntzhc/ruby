using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class InputFieldPrefeb : MonoBehaviour
{
    // Start is called before the first frame update
    private RunePanelController runePanelController; // 保存 PanelControl 组件的引用
    private ScrollbarPrefeb scrollbarPrefeb;
    public bool isUpdating = false;
    private string previousValue;
    public int value;
    TMP_InputField[] inputFields;
    void Start()
    {
        inputFields = gameObject.GetComponentsInChildren<TMP_InputField>();
        previousValue = "";
        scrollbarPrefeb = FindObjectOfType<ScrollbarPrefeb>(); // 获取 ScrollbarPrefeb 组件的引用
        runePanelController = FindObjectOfType<RunePanelController>(); // 获取 PanelControl 组件的引用
        // 遍历并添加监听器
        foreach (TMP_InputField inputField in inputFields)
        {
            inputField.onValueChanged.AddListener(delegate { OnTMP_InputFieldValueChanged(inputField); });
        }
    }
    void OnTMP_InputFieldValueChanged(TMP_InputField inputField)
    {
        // 待做：仅能输入数字
        // string newValue = inputField.text;
        // if (newValue == "")
        // {
        //     previousValue = "";
        //     return;
        // }

        // int parsedValue;
        // if (int.TryParse(newValue, out parsedValue) && parsedValue >= 0 && parsedValue <= 999999)
        // {
        //     previousValue = newValue;
        //     return;
        // }
        // else
        // {
        //     GetComponent<TMP_InputField>().text = previousValue;
        //     return;
        // }
        // Debug.Log(newValue);

        if (!isUpdating)
        {
            float inputValue;
            Debug.Log("input Updating");
            isUpdating = true;
            if (float.TryParse(inputField.text, out inputValue) || inputField.text == "")
            {
                if (inputField.text == "") value = 0;
                else value = (int)inputValue;

                string name = inputField.name;
                int index = name[name.Length - 1] - '0';
                inputFields[index].text = value.ToString();
                List<int> ringProperty = runePanelController.ringProperties[runePanelController.selectedRingIndex];
                // 有个2是因为ringProperty前两位是环的序号和种类，不进行直接修改。
                ringProperty[index + 2] = value;
                runePanelController.ringProperties[runePanelController.selectedRingIndex] = ringProperty;
                //在更新完环属性后，再更新scrollbar
                scrollbarPrefeb.UpdateValue();
            }
            Debug.Log("input Updated");
            isUpdating = false;
        }

    }
    // 数值更新的逻辑是：inputField或slider两者之一数值更新->更新环的属性ringProperty->另一方根据ringProperty更新。
    public void UpdateValue()
    {
        if (!isUpdating)
        {
            Debug.Log("input Updating");
            isUpdating = true;
            // inputFields[index].text = value.ToString();
            // 有个2是因为ringProperty前两位是环的序号和种类，不进行直接修改。
            List<int> ringProperty = runePanelController.ringProperties[runePanelController.selectedRingIndex];
            for (int i = 2; i < ringProperty.Count; i++)
            {
                inputFields[i - 2].text = ringProperty[i].ToString();
            }
            isUpdating = false;
        }
    }
}
