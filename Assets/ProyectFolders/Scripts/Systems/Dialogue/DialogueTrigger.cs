using UnityEngine;

// Para NPC o zonas donde se activen dialogos

// Para activar por codigo: DialogueManager.Instance.StartDialogue(miDialogueData);

public class DialogueTrigger : MonoBehaviour
{
    public DialogueData dialogueData;
    public KeyCode interactKey = KeyCode.E;
    private bool playerInRange = false;

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(interactKey))
        {
            DialogueManager.Instance.StartDialogue(dialogueData);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            Debug.Log("Presiona E para hablar");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}
