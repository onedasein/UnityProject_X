using UnityEngine;

public class PlayerInteracionHandler: MonoBehaviour
{
    //用于存储当前玩家可以交互的物体
    private Interactable currentInteractable;

    private void Update()
    {
        
        //每一帧都检查是否按下了F键并且当前有可交互物体
        if (Input.GetKeyDown(KeyCode.F) && currentInteractable != null)
        {
            //如果条件满足则调用该物体的交互方法
            currentInteractable.Interact();
        }
        else if (Input.GetKeyDown(KeyCode.F) && currentInteractable == null)
        {
            Debug.Log("按下了F键,但是未检测到可交互物体");
        }
        
    }

    //当玩家进入某个触发碰撞器时调用
    void OnTriggerEnter2D(Collider2D other)
    {
        //检查进入触发器的物体是否有Interactable组件
        Interactable interactable = other.GetComponent<Interactable>();
        if (interactable != null)
        {
            //如果有将其设置为当前可交互物体
            currentInteractable = interactable;
            Debug.Log("发现可交互物体" + other.gameObject.name);
            //这里可以添加UI提示，如显示"按F交互"
        }
    }

    //当玩家离开某个触发碰撞器时调用
     void OnTriggerExit2D(Collider2D other)
    {
        //检查离开触发器的物体是否有Interactable组件
        Interactable interactable = other.GetComponent<Interactable>();
        //如果离开的物体正好是当前记录的可交互物体，则清空记录
        if (interactable != null && interactable == currentInteractable)
        {
            currentInteractable = null;
            Debug.Log("远离可交互物体");
            //这里可以关闭UI提示   
        }
    }
}