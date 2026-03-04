using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Xml.Serialization;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [Header("UI References")]
    public CanvasGroup canvasGroup;
    public Image portrait;
    public TMP_Text actorName;
    public TMP_Text dialogueText;

    public bool isDialogueActive;

    private DialogueSO currentDialogue;
    private int dialogueIndex;

    private void Awake()
    {
        if (Instance == null)
        { 
            Instance = this;
        }
        else
            Destroy(gameObject);

        // 加canvasGroup空值判断
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 0;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }
        else
        {
            Debug.LogError("DialogueManager的canvasGroup未赋值！", this);
        }
    }

    public void StartDialogue(DialogueSO dialogueSO)
    {
        currentDialogue = dialogueSO;
        dialogueIndex = 0;
        isDialogueActive = true;
        ShowDialogue();
    }

    public void AdvanceDialogue()
    {
        if (dialogueIndex < currentDialogue.lines.Length)
            ShowDialogue();
        else
            EndDialogue();
    }

    private void ShowDialogue()
    {
        DialogueLine line = currentDialogue.lines[dialogueIndex];

        portrait.sprite = line.speaker.portrait;
        actorName.text = line.speaker.actorName;

        dialogueText.text = line.text;

        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        dialogueIndex++;
    }

    private void EndDialogue()
    {
        dialogueIndex = 0;
        isDialogueActive = false;
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }
}
