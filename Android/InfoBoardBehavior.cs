using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoBoardBehavior : MonoBehaviour
{
    public DialogueTrigger claseDlgTrigger;
    public DialogueManager claseDlgManager;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            claseDlgTrigger.TriggerDialogue();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            claseDlgManager.EndDialogue();
        }
    }
}
