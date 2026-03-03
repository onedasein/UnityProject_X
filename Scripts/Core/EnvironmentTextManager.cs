//基于现有文本系统的集成
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class EnvironmentTextManager : MonoBehaviour
{
    // 继承ManualManager
    private ManualManager manualManager;

    // 环境交互文本配置
    [System.Serializable]
    public class EnvironmentTextData
    {
        public string objectId;
        public List<TextSequence> sequences;
    }

    [System.Serializable]
    public class TextSequence
    {
        public int sequenceNumber;      // 1,2,3...
        public string textId;           // 对应ManualManager中的文本ID
        public int repeatAfter = -1;    // 第几次后重复，-1表示不重复

    }

    public List<EnvironmentTextData> environmentTexts;

    // 获取指定物体的交互文本
    public string GetEnvironmentText(string objectId, int interactionCount)
    {
        var data = environmentTexts.Find(d => d.objectId == objectId);
        if (data == null) return "未找到交互文本";

        // 查找当前次数的文本
        foreach (var seq in data.sequences)
        {
            if (seq.repeatAfter >= 0 && interactionCount >= seq.repeatAfter)
            {
                // 使用重复的文本
                return manualManager.GetText(seq.textId).content;
            }

            if (seq.sequenceNumber == interacionCount)
            {
                return manualManager.GetText(seq.textId).content;
            }
        }

        // 默认返回最后一个
        var lastSeq = data.sequences.Last();
        return manualManager.GetText(lastSeq.textId).content;
    }
}