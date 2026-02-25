using UnityEngine;
using UnityEngine.UI; // 引入UI命名空间
using System.Collections.Generic;
using TMPro;
using System.Runtime.InteropServices;

public class DialogUIManager : MonoBehaviour
{
    // 单例模式，便于从任何地方访问
    public static DialogUIManager Instance { get; private set; }

    // 在Inspector面板中拖拽对应的UI组件进行赋值
    [Header("UI组件引用")]
    public GameObject dialogPanel; // 整个对话框面板
    public TMP_Text speakerNameText;   // 说话者名字文本
    public TMP_Text contentText;       // 对话内容文本
    public TMP_Text continuePromptText;// “继续”提示文本

    [Header("对话设置")]
    public float textDisplaySpeed = 0.05f; // 每个字符显示的时间间隔（用于打字机效果）

    // 当前正在进行的对话队列
    private Queue<string> sentencesQueue = new Queue<string>();
    // 当前对话的说话者
    private string currentSpeaker;
    // 是否正在显示文本（用于打字机效果协程）
    private bool isDisplayingText = false;

    void Awake()
    {
        UnityEngine.Debug.Log("DialogUIManager Awake");
        // 简单的单例初始化
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        // 初始时隐藏对话框
        dialogPanel.SetActive(false);
    }

    void Update()
    {
        // 如果对话框正在显示，监听F键以继续下一条对话
        if (dialogPanel.activeInHierarchy && Input.GetKeyDown(KeyCode.F))
        {
            DisplayNextSentence();
        }
    }

    // 开始一段新的对话
    public void StartDialog(string speakerName, string[] sentences)
    {
        Debug.Log($"开始对话，共 {sentences.Length}句");
        // 设置当前说话者
        currentSpeaker = speakerName;
        speakerNameText.text = currentSpeaker;

        // 清空旧队列，装入新对话
        sentencesQueue.Clear();
        foreach (string sentence in sentences)
        {
            sentencesQueue.Enqueue(sentence);
        }
        Debug.Log($"队列初始化后,句数： {sentencesQueue.Count}");
        // 显示对话框
        dialogPanel.SetActive(true);
        // 立即显示第一条对话
        DisplayNextSentence();
    }

    // 显示下一条对话内容
    public void DisplayNextSentence()
{
    // 状态 1：如果正在打字，按下 F 应该“瞬间完成”当前句
    if (isDisplayingText)
    {
        StopAllCoroutines(); 
        isDisplayingText = false;
        // 注意：此时不需要操作队列，因为当前显示的句子已经在 StartCoroutine 时传入了
        // 我们只需要让提示符出现即可
        continuePromptText.gameObject.SetActive(true);
        // 如果你希望瞬间显示完当前句，你可以考虑在协程开始前存一个 currentFullSentence 变量
        return; // 直接返回，等待下一次按 F 切换下一句
    }

    // 状态 2：如果没在打字，检查队列是否还有下一句
    if (sentencesQueue.Count == 0)
    {
        EndDialog();
        return;
    }

    // 状态 3：取出下一句并开始打字
    string sentenceToShow = sentencesQueue.Dequeue();
    StartCoroutine(TypeSentence(sentenceToShow));
}

    // 打字机效果协程
    private System.Collections.IEnumerator TypeSentence(string sentence)
    {
        isDisplayingText = true;
        contentText.text = ""; // 清空内容
        continuePromptText.gameObject.SetActive(false); // 隐藏“继续”提示

        // 逐个字符添加到文本中
        foreach (char letter in sentence.ToCharArray())
        {
            contentText.text += letter;
            yield return new WaitForSeconds(textDisplaySpeed);
        }

        // 显示完毕
        isDisplayingText = false;
        continuePromptText.gameObject.SetActive(true); // 显示“继续”提示
    }

    // 结束对话
    void EndDialog()
    {
        UnityEngine.Debug.Log("EndDialog方法被调用,关闭对话框");
        dialogPanel.SetActive(false);
        UnityEngine.Debug.Log("对话结束。");
        // 这里可以触发对话结束事件，例如恢复玩家控制、执行后续任务等
    }
}
