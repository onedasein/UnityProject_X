// 窗的具体实现（带图片）
using UnityEngine;

public class WindowInteractable : InteractableObject
{
    [Header("窗特有设置")]
    public Sprite windowImage;
    public string[] windowTexts;

    private int textIndex = 0;

    public override string OnInteract()
    {
        string text = "";

        if (interactCount == 0)
        {
            text = "窗纸半透，能看见外面的小院。\n";
            text += "院子不大，青石地面扫得很干净。\n";
            text += "檐下挂着几串红灯笼，纸面新换过，红得扎眼。\n";
            text += "院墙外，天空是灰的。\n";
            text += "白驿.LIN: ......快过年了。";

            // 显示图片
            UIManager.Instance.ShowIamge(windowImage);
        }
        else
        {
            text = "窗纸半透，灯笼还在晃。\n";
            text += "天空还是灰的。";
        }

        interactCount++;
        return text;
        
    }
}