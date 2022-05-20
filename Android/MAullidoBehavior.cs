using System.Collections;
using System.Collections.Generic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MAullidoBehavior : MonoBehaviour
{
    public DialogueTrigger claseDlgTrigger;
    public DialogueManager claseDlgManager;
    // Start is called before the first frame update
    void Start()
    {
        pausita();
        claseDlgTrigger.TriggerDialogue();
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator pausita()
    {
        yield return new WaitForSeconds(2);

    }

}
