using UnityEngine;
//Guarda los textos para mostrar
[CreateAssetMenu(fileName = "NewDialogue", menuName = "Dialogue/DialogueData")]
public class DialogueData : ScriptableObject
{
    [TextArea(2, 5)]
    public string[] lines;
}
