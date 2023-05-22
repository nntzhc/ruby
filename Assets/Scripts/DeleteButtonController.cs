using UnityEngine;
using UnityEngine.UI;

public class DeleteButtonController : MonoBehaviour
{
    // inspector 导出 Button 组件
    private RunePanelController runePanelController; // 保存 PanelControl 组件的引用
    private Button deleteButton;

    void Start()
    {
        // 获取当前游戏对象下的 Button 组件引用
        deleteButton = GetComponent<Button>();

        // 在 Button 上注册点击事件响应函数 TaskOnClick
        // 需要注意的是，如果不加该判断直接AddListener会报错
        // 初步判断是以下原因：
        // 需要等待对象已经被创建 / 初始化
        // 如果您尝试在 Start() 方法中访问某个需要时间才可加载完整的组件或脚本（例如在动态加载资源或分布式系统中），
        // 则可能会出现此错误。可以将实例化代码移到 Awake() 方法中，该方法在所有 Start() 方法之前执行。
        // 那么在 Start() 方法中先检查需要实例化的组件是否为空，然后再进行相应操作。
        if (deleteButton == null) return;
        else deleteButton.onClick.AddListener(TaskOnClick);
        runePanelController = FindObjectOfType<RunePanelController>(); // 获取 PanelControl 组件的引用
    }

    void TaskOnClick()
    {
        // 输出消息到控制台
        runePanelController.DeleteSelectedRing();
    }
}
