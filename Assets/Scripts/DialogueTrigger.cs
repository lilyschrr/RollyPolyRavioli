using UnityEngine;
using UnityEngine.EventSystems;

public class DialogueTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    [System.Serializable]
    public class DialogueData
    {
        [TextArea(3, 10)] // Gives a nice large text box in the Unity Inspector
        public string[] dialogueLines;
    }

    [Header("Dialogue Content")]
    [SerializeField] private DialogueData dialogue;

    private DialogueManager dialogueManager;

    public void OnPointerEnter(PointerEventData eventData)
    {
        // TODO: Change to adding sprite effect
        Debug.Log("Mouse entered");

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // TODO: Change to removing sprite effect
        Debug.Log("Mouse exited");
    }

    public void OnPointerDown(PointerEventData eventData)
        {
            Debug.Log("clicked!!");
            TriggerDialogue();
        }

    void Start()
    {
        // Find DialogueManager in the scene
        dialogueManager = Object.FindFirstObjectByType<DialogueManager>();
    }

    public void TriggerDialogue()
    {
        if (dialogueManager != null)
        {
            dialogueManager.StartDialogue(dialogue.dialogueLines);
        }
    }
}