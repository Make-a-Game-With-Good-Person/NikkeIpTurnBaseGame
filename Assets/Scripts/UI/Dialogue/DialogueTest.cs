using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTest : MonoBehaviour
{
    public void buttonAct()
    {
        if(DialogueManager.instance.dialogueData.Contains(DialogueManager.instance.dialogueData.Find(x => x.dialogue_index == 100000)))
        {
            var data = DialogueManager.instance.dialogueData.Find(x => x.dialogue_index == 100000);
            GetComponent<DialogueController>().StartDialogue(data);
        }
        else
        {
            Debug.Log("키 없어요");
        }
        
        
    }

    
}
