using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class DialogueTrigger : MonoBehaviour
{
    [System.Serializable]
    public class DialogueData
    {
        [TextArea(3, 10)] // Gives text box in inspector
        public string[] dialogueLines;
    }

    [Header("Dialogue Content")]
    [SerializeField] private DialogueData dialogue;

    [SerializeField] public GameObject eTag;

    private DialogueManager dialogueManager;

    private bool interacting = false;


    void Start()
    {
        // Find DialogueManager in the scene
        dialogueManager = Object.FindFirstObjectByType<DialogueManager>();
    }

    void Update()
    {
        if (Keyboard.current.eKey.wasPressedThisFrame && !interacting)
        {
            eTag.SetActive(false);
            interacting = true;
            TriggerDialogue();
        }
    }

    public void TriggerDialogue()
    {
        if (dialogueManager != null)
        {
            dialogueManager.StartDialogue(dialogue.dialogueLines);
        }
    }
}