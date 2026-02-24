using System.Diagnostics;
using UnityEngine;

//道具描述的派生类
[System.Serializable]
public class DerivedItemDescriptionText : GameText
{
    //道具特有的字段
    public string itemType;// 道具类型（武器、消耗品、材料等）

    // 构造函数
    public DerivedItemDescriptionText(string id, string title, string content, string itemType = "")
        : base(id, title, content)
    {
        this.itemType = itemType;
    }

    public override void Unlock()
    {
        base.Unlock();
        UnityEngine.Debug.Log($"物品获得{title}\n物品类型{itemType}\n");
    }

    public override string GetDetails()
    {
        string baseDetails=base.GetDetails();
        return $"{baseDetails}\n道具类型: {itemType}\n";
    }
    
    public override void PrintDetails()
    {
        base.PrintDetails();
        UnityEngine.Debug.Log($"物品类型{itemType}");
    }
}