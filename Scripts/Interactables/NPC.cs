using UnityEngine;

public class NPC: Interactable
{
    //重写基类的Interact方法，实现NPC特有的交互行为
    public override void Interact()
    {
        //首先调用基类方法，仍然输出日志
        base.Interact();

        //以下是NPC独有的交互逻辑
        Debug.Log("你好,旅行者!");
        //这里可以触发对话系统、打开商店界面、播放动画登
    }
}