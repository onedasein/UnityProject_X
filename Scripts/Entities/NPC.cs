//继承自Interactable的具体实类
using UnityEngine;

public class NPC : Interactable
{
    [Header("对话内容")]
    public string npcName = "village"; // NPC的名字
    [TextArea(3, 10)] // 在Inspector中显示为多行文本区域
    public string[] dialogSentences = new string[] {"hello, traveller!"}; // 存储多句对话的数组

    // 重写基类的Interact方法
    public override void Interact()
    {
        // 首先调用基类方法（输出日志）
        base.Interact();

        // 调用UI管理器，开始播放对话
        if (DialogUIManager.Instance != null && dialogSentences.Length > 0)
        {
            DialogUIManager.Instance.StartDialog(npcName, dialogSentences);
        }
        else
        {
            Debug.LogWarning("DialogUIManager未找到或对话内容为空!");
        }
    }
}