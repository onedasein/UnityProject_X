using System.Diagnostics;
using UnityEngine;

public abstract class GameText
{
    public string id;
    public string title;
    public string content;
    public bool isUnlocked;
    
    //构造函数
    public GameText(string id,string title,string content)
    {
        this.id=id;
        this.title=title;
        this.content=content;
        this.isUnlocked=false;//默认未解锁
    }

    //虚方法·解锁时运行
    public virtual void Unlock()
    {
        isUnlocked=true;
        UnityEngine.Debug.Log($"文本已解锁：{title}");
    }

    //虚方法·显示详情
    public virtual string GetDetails()
    {
        return $"ID: {id}\n标题: {title}\n内容: {content}\n解锁状态: {(isUnlocked ? "已解锁" : "未解锁")}";
    }
    
    //虚方法·直接输出详情
    public virtual void PrintDetails()
    {
        UnityEngine.Debug.Log(GetDetails());
    }
}