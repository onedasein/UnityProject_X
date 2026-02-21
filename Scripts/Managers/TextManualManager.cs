using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class ManualManager:MonoBehaviour
{
    //单例模式
    public static ManualManager Instance{get;private set;}

    //使用字典储存所有文段，以id为键
    private Dictionary<string,GameText>allTexts=new Dictionary<string, GameText>();

    void Awake()
    {
        //单例初始化模式
        if (Instance == null)
        {
            Instance = this;  //设置静态单例
            DontDestroyOnLoad(gameObject);//跨场景不销毁
            Initialize();  //初始化数据，通过调用后面自定义的初始化函数
        }
        else
        {
            //如果已存在实例，销毁
            Destroy(gameObject);
        }
    }
    void Initialize()
    {
        // 清空旧数据（防止重复初始化）
        allTexts.Clear();
        
        //调用后面自定义的函数，从Resources加载所有文本数据
        LoadAllTexts();
        Debug.Log($"文案系统初始化完成，共加载{allTexts.Count}个文本");
        PrintAllTexts();//调试用，加载完成后打印所有文本信息，记得最后注释掉哦/doge
    }
    void LoadAllTexts()
    {
        //加载剧情文本
        LoadTextsFromJson<DerivedPlotText>("TextJson/PlotData");
        
        //加载道具描述文本
        LoadTextsFromJson<DerivedItemDescriptionText>("TextJson/ItemData");
        
        //加载书籍文本
        LoadTextsFromJson<DerivedBookText>("TextJson/BookData");
        
        //更多类型
        //LoadTextsFromJson<Derived?????Text>("TextJson/?????Data");
    }
    void LoadTextsFromJson<T>(string filePath) where T : GameText//使用泛型，避免对每个类型都写一遍从json读取的代码
    {
        //从Resources加载JSON文件
        TextAsset jsonFile=UnityEngine.Resources.Load<TextAsset>(filePath);
        
        if(jsonFile==null)
        {
            Debug.LogWarning($"未找到文件: {filePath}");
            return;
        }
        //解析JSON数据
        //因为JsonUtility不能直接解析JSON数组，需要包装成对象，所以需要TextDataArray包装。
        string wrappedJson = $"{{\"items\":{jsonFile.text}}}";
        TextDataArray<T> dataArray = JsonUtility.FromJson<TextDataArray<T>>(wrappedJson);
        
        if(dataArray==null||dataArray.items==null)
        {
            Debug.LogError($"解析JSON失败: {filePath}");
            return;
        }
        
        // 将每个文本添加到管理器
        foreach(T text in dataArray.items)
        {
            AddText(text);
        }
        
        Debug.Log($"已加载 {dataArray.items.Length} 个{typeof(T).Name}");
    }

    void AddText(GameText text)
    {
        if(text==null||string.IsNullOrEmpty(text.id))
        {
            Debug.LogWarning("尝试添加无效的文本");
            return;
        }
        
        // 检查是否已存在相同ID
        if(allTexts.ContainsKey(text.id))
        {
            Debug.LogWarning($"文本ID重复: {text.id}");
            return;
        }
        // 添加到主字典
        allTexts[text.id] = text;
    }
    
    //以下为公共接口，外部调用都在这里！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！
    //接口1，根据ID获取文本
    public GameText GetText(string id)
    {
        if(allTexts.ContainsKey(id))
        {
            return allTexts[id];
        }
        
        Debug.LogWarning($"未找到文本: {id}");
        return null;
    }
    
    //接口2，解锁文本
    public bool UnlockText(string id)
    {
        GameText text = GetText(id);
        if(text==null)
        {
            Debug.LogWarning($"尝试解锁不存在的文本: {id}");
            return false;
        }
        
        if(text.isUnlocked)
        {
            Debug.LogWarning($"尝试解锁已经解锁的文本: {id}");
            return false;
        }
        //调用文本的解锁方法
        text.Unlock();
        //触发解锁事件
        OnTextUnlocked?.Invoke(text);
        /*看到这段话的人：在外部代码定义一个方法，变量只有一个，是GameText类型(类型见BaseGameText.cs)。假设你给这个方法起名为method1，你可以写
        ManualManager.OnTextUnlocked+=method1
        这样，在接口2被调用的时候，method1就会被调用，入参是接口2id对应的GameText
        同样，你可以添加method23456，或者-=让method不再被调用。
        如果你同时+=了多个method，它们会按照你+=的顺序依次执行。
        */
        Debug.Log($"文本解锁成功: {text.title}");
        return true;
    }
    
    //接口3，批量解锁
    public void UnlockTexts(List<string> ids)
    {
        foreach(string id in ids)
        {
            UnlockText(id);
        }
    }
    
    //接口4，获取所有已解锁的文本
    public List<GameText> GetAllUnlockedTexts()
    {
        List<GameText> unlocked = new List<GameText>();
        foreach(var text in allTexts.Values)
        {
            if(text.isUnlocked)
            {
                unlocked.Add(text);
            }
        }
        return unlocked;
    }
    
    //接口5，获取特定类型的文本
    public List<T> GetTextsByType<T>() where T : GameText
    {
        List<T> result = new List<T>();
        
        foreach(GameText text in allTexts.Values)
        {
            if(text is T typedText)
            //is操作符干了两件事，先检查是否是某个类型，如果是，则进行一次类型转换（同类之间的类型转换），然后进行赋值
            {
                result.Add(typedText);
            }
        }
        return result;
    }
    
    //接口6，获取特定类型的已解锁文本
    public List<T> GetUnlockedTextsByType<T>() where T : GameText
    {
        List<T> allOfThisType=GetTextsByType<T>();
        List<T> unlocked=new List<T>();
        foreach(T text in allOfThisType)
        {
            if (text.isUnlocked)
            {
                unlocked.Add(text);
            }
        }
        return unlocked;
    }
    
    //接口7，检查文本是否解锁
    public bool IsTextUnlocked(string id)
    {
        GameText text = GetText(id);
        if(text==null){Debug.LogWarning("尝试检查不存在的文本");}
        return text!=null&&text.isUnlocked;
    }
    
    //接口8，清空所有解锁状态
    public void ResetAllUnlocks()
    {
        foreach(var text in allTexts.Values)
        {
            text.isUnlocked = false;
        }
        Debug.Log("所有文本解锁状态已重置");
    }
    
    //事件系统
    //定义事件委托
    public delegate void TextUnlockedHandler(GameText unlockedText);
    
    // 事件：文本解锁时触发
    public event TextUnlockedHandler OnTextUnlocked;
    
    //调试方法
    //仅供开发使用，记得最后注释掉哦/doge
    public void PrintAllTexts()
    {
        Debug.Log("=== 所有文本 ===");
        foreach (var kvp in allTexts)
        {
            GameText text = kvp.Value;
            Debug.Log($"--- {kvp.Key} ---");
            text.PrintDetails();
            Debug.Log("");
        }
    }
    public void PrintText(string id)
    {
        GameText text = GetText(id);
        if (text != null)
        {
            text.PrintDetails();
            Debug.Log("");
        }
    }
}
[System.Serializable]
public class TextDataArray<T>
{
    public T[] items;
}