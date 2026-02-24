using System.Diagnostics;
using UnityEngine;

// 剧情文本的派生类
[System.Serializable]//这个类可以转变成json，也可以从json转变过来
public class DerivedPlotText : GameText
{
    //剧情特有字段
    public string inGameTime;
    public string[] choices;// 对话选项（如果有）
    
    //构造函数
    public DerivedPlotText(string id, string title, string content, string inGameTime = "")
        : base(id, title, content)
    {
        this.inGameTime = inGameTime;
        this.choices = new string[0];
    }
    
    public override void Unlock()
    {
        base.Unlock();
        UnityEngine.Debug.Log($"剧情解锁：{title}，游戏时间：{inGameTime}");
    }

    public override string GetDetails()
    {
        string baseDetails=base.GetDetails();
        return $"{baseDetails}\n游戏时间: {inGameTime}\n";
    }
    
    public override void PrintDetails()
    {
        base.PrintDetails();
        UnityEngine.Debug.Log($"游戏时间：{inGameTime}\n");
    }
}