using UnityEngine;

//笔记本文本的派生类，还没设计好，暂时用不到
[System.Serializable]
public class DerivedBookText : GameText
{
    // 书籍特有的字段

    
    // 构造函数
    public DerivedBookText(string id, string title, string content)
        : base(id, title, content)
    {
    }
    
    // 重写解锁方法
    public override void Unlock()
    {
        base.Unlock();
    }

    public override string ToString()
    {
        return base.ToString();
    }

    public override void PrintDetails()
    {
        base.PrintDetails();
    }
}