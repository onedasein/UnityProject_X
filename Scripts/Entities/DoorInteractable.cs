// 门的具体实现
using UnityEngine;
public class DoorInteractable : InteractableObject
{
    [Header("门特有设置")]
    public bool isLocked = true;
    public string lockDescription = "锁住了";

    public override string OnInteract()
    {
        if (isLocked)
        {
            // 门的特殊逻辑：多次交互后文本优化
            if (interactCount == 0)
            {
                return "陈旧的木门，锁住了。";
            }
            else if (interactCount == 1)
            {
                return "锁住了。";
            }
            else if (interactCount == 2)
            {
                return "主人家锁住的门，还是不要好奇了......";
            }
            else
            {
                return "锁住了。";
            }
        }

        return base.OnInteract();
    }
}