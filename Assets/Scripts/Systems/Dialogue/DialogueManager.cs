using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;  // Singleton (una sola instancia)
    [Header("UI References")]
    public GameObject dialoguePanel;
    public TMP_Text dialogueText;

    private Queue<string> lines = new Queue<string>();
    private bool isTyping = false;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        if (dialoguePanel != null) dialoguePanel.SetActive(false);
    }

    public void StartDialogue(DialogueData data)
    {
        if (data == null || data.lines == null || data.lines.Length == 0) return;

        dialoguePanel.SetActive(true);
        lines.Clear();

        foreach (var line in data.lines)
            lines.Enqueue(line);

        DisplayNextLine();
    }

    public void DisplayNextLine()
    {
        if (isTyping) return;

        if (lines.Count == 0)
        {
            EndDialogue();
            return;
        }

        string nextLine = lines.Dequeue();
        StartCoroutine(TypeLine(nextLine));
    }

    private IEnumerator TypeLine(string line)
    {
        isTyping = true;
        dialogueText.text = "";

        foreach (char c in line.ToCharArray())
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(0.02f);
        }

        isTyping = false;
    }

    public void EndDialogue()
    {
        dialoguePanel.SetActive(false);
    }

    public void ClearDialogueText()
    {
        if (dialogueText != null)
        {
            StopAllCoroutines();
            dialogueText.text = "";
        }
    }
}
