//作为所有可交互物体的抽象基类，属于核心玩法机制
using UnityEngine;

public class Interactable: MonoBehaviour
{

    void OnDrawGizmosSelected()
    {
       Gizmos.color = Color.blue;
       // 假设交互范围与碰撞体一致，这里也可以可视化辅助调试
    }

    //虚方法，子类可以重写以实现特定的交互逻辑
    public virtual void Interact()
    {
        //基类方法，在控制台输出一条日志
        Debug.Log("正在与" + gameObject.name + "交互");
    }
}