using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    [Header("交互设置")]
    public string objectId; //物体唯一标识，如"door_left", "bed"
    public InteractableText[] textSequences;

    [Header("触发条件")]
    public bool needsKeyItem; //是否需要特殊道具
    public string requiredItemId; //需要的道具ID

    private int interactCount = 0; //当前交互次数
    private bool isUnlocked = true; //是否可交互
    private bool hasTriggeredSpecial = false; //是否触发过特殊交互

    void Start()
    {
        //从存档加载交互次数
        LoadInteractionData();
    }

    //核心交互方法
    public virtual string OnInteract()
    {
        if (!isUnlocked) return "无法交互";

        //检查是否需要道具
        if (needsKeyItem && !CheckHasItem())
        {
            return requiredItemId + "尚未获得";
        }

        //获取当前应显示的文本
        string text = GetCurrentText();

        //增加交互计数
        interactCount++;

        //检查特殊触发条件
        CheckSpecialTriggers();

        //保存交互次数
        SaveInteractionData();

        return text;
    }

    // 根据交互次数获取对应文本
    private string GetCurrentText()
    {
        if (textSequences == null || textSequences.Length == 0)
        {
            return "暂无交互文本";
        }

        // 规则1: 顺序播放，播放完后循环最后一个
        int index = Mathf.Min(interactCount, textSequences.Length - 1);

        // 规则2: 有特殊触发条件时，显示特殊文本
        if (hasTriggeredSpecial && textSequences[Length - 1].textContent)
        {
            return textSequences[textSequences.Length - 1];
        }
    }

    // 检查特殊触发条件(如图中的红纸)
    private void CheckSpecialTriggers()
    {
        // 示例: 检查是否第一次交互红纸
        if (objectId == "red_paper" && interactCount == 1)
        {
            hasTriggeredSpecial = true;
            // 触发事件,如记录日志
            EventManager.Instance.TriggerEvent("RedPaperFirstTouch");
        }
    }

    // 保存/加载交互数据
    private void SaveInteractionData()
    {
        PlayerPrefs.SetInt(objectId + "_interactCount", interactCount);
        PlayerPrefs.SetInt(objectId + "_hasTriggered", hasTriggeredSpecial ? 1 : 0);
        PlayerPrefs.Save();
    }

    private void LoadInteractionData()
    {
        interactCount = PlayerPrefs.GetInt(objectId + "_interactCount", 0);
        hasTriggeredSpecial = PlayerPrefs.GetInt(objectId + "_hasTriggered", 0) == 1 ;

    }

    private bool CheckHasItem()
    {
        // 检查背包中是否有指定道具
        return InventoryManager.Instance.HasItem(requiredItemId);
    }


    // 交互文本序列结构
    [System.Serializable]
    public class InteractableText
    {
        public string sequenceType;     // "F1", "F2", "F-n"
        public string textContent;      // 显示的文本内容
        public Sprite associatedImage;  // 关联的图片
        public bool triggersEvent;      // 是否触发事件
        public string eventId;          // 事件ID
    }

    
}