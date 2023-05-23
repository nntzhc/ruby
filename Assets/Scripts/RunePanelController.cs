using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class RunePanelController : MonoBehaviour
{
    // 保存所有环的列表
    private List<GameObject> rings = new List<GameObject>();
    //用于存储环的性质
    //第一列：环的序号
    //第二列：环的种类。1、主体；2、运动模型；3、分裂；4、存储；5、活性；6、赋予；7、随机
    //第三列：魔力输入速度
    //第四列：魔力容量
    //第五列：魔力消耗
    public List<List<int>> ringProperties = new List<List<int>>();
    public List<string> ringDictionary = new List<string>();
    //各类组件
    private ScrollbarPrefeb scrollbarPrefeb;
    private InputFieldPrefeb inputFieldPrefeb; // 保存 PanelControl 组件的引用
    public GameObject ringPrefab;

    //滚轮相关操作参数
    public float scaleSpeed = 1.0f;
    public float minScale = 0.2f;
    public float maxScale = 2.0f;
    public float originRingRadius = 50.0f;
    public Vector2 originRingCenter = new Vector2(-2, -5);
    private int ringsNum = 0;
    private float zoomScale = 1.0f;
    private float pastZoomScale;
    private float originScale = 0.8f;
    private float correctionCoefficientForGraphicProportion = 0.8f;
    private float ringThickness;
    public int selectedRingIndex = -1;
    private int selectedRingIndexPast = 0;
    public float clickPos;
    void Start()
    {
        EventTrigger eventTrigger = gameObject.AddComponent<EventTrigger>();

        ringDictionary.Add("objectRing");
        ringDictionary.Add("moveRing");
        ringDictionary.Add("activityRing");

        scrollbarPrefeb = FindObjectOfType<ScrollbarPrefeb>();
        inputFieldPrefeb = FindObjectOfType<InputFieldPrefeb>();

        // 添加鼠标点击事件监听器
        // 点击空白取消选中所有的环
        EventTrigger.Entry clickEntry = new EventTrigger.Entry();
        clickEntry.eventID = EventTriggerType.PointerClick;
        clickEntry.callback.AddListener((eventData) => OnPanelClick((PointerEventData)eventData));
        eventTrigger.triggers.Add(clickEntry);

    }
    private void Update()
    {
        Vector2 mousePos = Input.mousePosition;
        Vector2 center = gameObject.GetComponent<Image>().rectTransform.position;
        // Debug.Log(mousePos);

        // 用滚轮调整魔法阵的大小
        float scrollValue = Input.GetAxis("Mouse ScrollWheel");
        if (scrollValue != 0)
        {
            zoomScale = zoomScale * (1 + scrollValue * scaleSpeed);
            zoomScale = zoomScale > maxScale ? maxScale : zoomScale;
            zoomScale = zoomScale < minScale ? minScale : zoomScale;
            ArrangeRingsPlacement();
        }
    }

    public void ArrangeRingsPlacement()
    {
        for (int i = 0; i < ringsNum; i++)
        {
            RectTransform RingI = transform.GetChild(GetActualIndex(i)).GetComponent<RectTransform>();
            RectTransform RingIRT = RingI.GetComponent<RectTransform>();
            float newScale;
            // 获取新建环的Transform组件
            Transform RingITransform = RingIRT.GetComponent<Transform>();
            // scale相比内侧环增加zoomScale
            newScale = (i + 1) * zoomScale;
            ringThickness = zoomScale * originRingRadius * correctionCoefficientForGraphicProportion;
            // 设置环的scale
            // Debug.Log("ring index is " + i + "ring newScale is " + newScale);
            RingITransform.localScale = new Vector3(newScale, newScale, 1f);
            RingI.GetComponent<RingPrefeb>().index = i;
            //乘了一个修正系数
            RingI.GetComponent<RingPrefeb>().radius = ringThickness;
            RingITransform.localScale = new Vector3(newScale, newScale, 1f);
        }
    }
    public void AddRingCommon(int ringKind)
    {
        ringsNum++;
        ringProperties.Insert(selectedRingIndex + 1, new List<int> { selectedRingIndex + 1, ringKind, 0, 0, 0 });
        // // 获取最外侧的符文环的RectTransform组件，后期要改成选中环的RectTransform组件
        // RectTransform innerRingRT = transform.GetChild(GetActualIndex(ringsNum - 1).GetComponent<RectTransform>();
        // 实例化新的符文环，实际上只创建了一个空的ringPrefeb，但是由于逻辑上是马上被选中，所以立即通过ringProperties[selectedRingIndex]
        GameObject newRing = Instantiate(ringPrefab, transform);
        // GameObject newRing = Instantiate(Ring, transform);
        // // 获取新建环的Transform组件
        Transform newRingTransform = newRing.GetComponent<RectTransform>().GetComponent<Transform>();
        newRingTransform.name = ringDictionary[ringKind];
        //放置在选择环的外层
        newRingTransform.SetSiblingIndex(GetActualIndex(selectedRingIndex + 1));
        // 新建环后自动选中
        selectedRingIndex = selectedRingIndex + 1;
        ArrangeRingsPlacement();
        SelectRing();
    }
    // 点击空白取消选中所有的环
    private void OnPanelClick(PointerEventData eventData)
    {
        UnSelectRing();
    }

    public void OnMouseClick(float distance)
    {
        selectedRingIndex = (int)(distance / ringThickness);
        Debug.Log("Mouse click at " + distance);
        Debug.Log("ringThickness " + ringThickness);
        selectedRingIndex = (int)(distance / ringThickness);
        if (selectedRingIndex < ringsNum) SelectRing();
        else UnSelectRing();
    }
    private void UnSelectRing()
    {
        // 将原来选中环的变为未选中样式
        if (selectedRingIndexPast < 0 || selectedRingIndexPast >= ringsNum) return;
        Image ringImage = gameObject.transform.GetChild(GetActualIndex(selectedRingIndexPast)).GetComponent<Image>();
        string path = "UI/" + ringDictionary[ringProperties[selectedRingIndexPast][1]];
        Sprite newSprite = Resources.Load<Sprite>(path);
        ringImage.sprite = newSprite;
    }
    private void SelectRing()
    {
        UnSelectRing();
        // 更新选中环的序号
        selectedRingIndexPast = selectedRingIndex;
        Image ringImage = gameObject.transform.GetChild(GetActualIndex(selectedRingIndex)).GetComponent<Image>();
        string path = "UI/" + ringDictionary[ringProperties[selectedRingIndex][1]] + "Selected";
        Sprite newSprite = Resources.Load<Sprite>(path);
        ringImage.sprite = newSprite;

        // 更新右侧输入
        inputFieldPrefeb.UpdateValue();
        scrollbarPrefeb.UpdateValue();
    }
    // 删除选中的环
    public void DeleteSelectedRing()
    {
        if (selectedRingIndex < ringsNum)
        {
            UnSelectRing();
            ringProperties.RemoveAt(selectedRingIndex);
            Destroy(gameObject.transform.GetChild(GetActualIndex(selectedRingIndex)).gameObject);
            ringsNum = ringsNum - 1;
            //删除最外侧环不知为何尺寸会出问题
            if (selectedRingIndex != ringsNum) ArrangeRingsPlacement();
        }
    }

    private int GetActualIndex(int i)
    {
        return ringsNum - 1 - i;
    }
}
