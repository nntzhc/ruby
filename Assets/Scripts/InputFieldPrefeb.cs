using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class InputFieldPrefeb : MonoBehaviour
{
    // Start is called before the first frame update
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
        Debug.Log(inputFields.Length);
        // 遍历并添加监听器
        foreach (TMP_InputField inputField in inputFields)
        {
            inputField.onValueChanged.AddListener(delegate { OnTMP_InputFieldValueChanged(inputField); });
        }
    }
    void OnTMP_InputFieldValueChanged(TMP_InputField inputField)
    {
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
            isUpdating = true;
            float floatValue;
            if (float.TryParse(inputField.text, out floatValue))
            {
                string name = inputField.name;
                int index = name[name.Length - 1] - '0';
                scrollbarPrefeb.UpdateValue(index, floatValue);
            }
            isUpdating = false;
        }
    }
    // Update is called once per frame
    public void UpdateValue(int index, float value)
    {
        if (!isUpdating)
        {

            isUpdating = true;
            inputFields[index].text = value.ToString();
            isUpdating = false;
        }
    }
}
