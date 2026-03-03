// 交互检测脚本
public class InteractionDetector : MonoBehaviour
{
    [SerializeField] private float interactionRange = 2f;
    [SerializeField] private KeyCode interactKey = KeyCode.F;
    
    private Camera mainCamera;
    private InteractableObject currentTarget;
    
    void Start()
    {
        mainCamera = Camera.main;
    }
    
    void Update()
    {
        RaycastForInteractable();
        
        if (Input.GetKeyDown(interactKey) && currentTarget != null)
        {
            string resultText = currentTarget.OnInteract();
            DisplayText(resultText);
        }
    }
    
    void RaycastForInteractable()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit, interactionRange))
        {
            InteractableObject io = hit.collider.GetComponent<InteractableObject>();
            if (io != null && io != currentTarget)
            {
                currentTarget = io;
                ShowPrompt("按 F 交互");
            }
        }
        else
        {
            currentTarget = null;
            HidePrompt();
        }
    }
    
    void DisplayText(string text)
    {
        // 调用你的UI系统显示文本
        DialogueSystem.Instance.ShowText(text);
    }
}