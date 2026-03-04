using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Npc_talk : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    public Animator interactAnim;
    public DialogueSO dialogueSO;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }

    private void OnEnable()
    {
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;
        anim.Play("Idle");
        interactAnim.Play("Open");
    }

    private void OnDisable()
    {
        interactAnim.Play("Close");
        rb.isKinematic = false;
    }

    private void Update()
    {
        if(Input.GetButtonDown("Interact"))
        {
            if (DialogueManager.Instance.isDialogueActive == true)
            {
                DialogueManager.Instance.AdvanceDialogue();
            }
            else
            { 
                DialogueManager.Instance.StartDialogue(dialogueSO); 
            }
        }
    }
}
