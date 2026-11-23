using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VectorGraphics;

public class DialogueManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private GameObject dialogueBoxPanel;

    [Header("Settings")]
    [SerializeField] private float typingSpeed = 0.04f;

    private Queue<string> sentences;
    private bool isTyping;
    private string currentSentence;

    [SerializeField] private SceneChanger sceneChanger;
    [SerializeField] private string sceneName;
    [SerializeField] private AudioSource speakingSFX;

    void Start()
    {
        sentences = new Queue<string>();
        dialogueBoxPanel.SetActive(false);
    }

    public void StartDialogue(string[] lines)
    {
        dialogueBoxPanel.SetActive(true);
        sentences.Clear();

        foreach (string line in lines)
        {
            sentences.Enqueue(line);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (isTyping)
        {
            StopAllCoroutines();
            dialogueText.text = currentSentence;
            isTyping = false;
            return;
        }

        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        currentSentence = sentences.Dequeue();
        StartCoroutine(TypeSentence(currentSentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        isTyping = true;
        if (speakingSFX != null) speakingSFX.Play();
        
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
    }

    void EndDialogue()
    {
        dialogueBoxPanel.SetActive(false);
        sceneChanger.LoadTargetScene(sceneName);
    }
}