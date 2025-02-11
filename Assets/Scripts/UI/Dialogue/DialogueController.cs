using System.Collections;
using UnityEngine;
using TMPro;

public class DialogueController : MonoBehaviour
{
    [SerializeField]private TMP_Text dialogueContext;   // 이름 출력칸
    [SerializeField]private TMP_Text dialogueText; // 대사 출력칸
    private float typingSpeed = 0.05f; // 텍스트 출력 딜레이


    private DialogueData dialogueList;// JSON에서 불러온 대사 리스트
    private int currentNPCDialogueIndex = 0; // 현재 출력중인 일반 대사 인덱스 
    
    private Coroutine typingCoroutine;
    public void StartDialogue(DialogueData dialogueList)
    {
        if(currentNPCDialogueIndex == 0)
        {
            DialogueStart();            
        }
        
        this.dialogueList = dialogueList;
        dialogueContext.text = dialogueList.dialogue_context[currentNPCDialogueIndex];
        dialogueText.text = dialogueList.dialogue_text[currentNPCDialogueIndex];

        
        if(currentNPCDialogueIndex >= dialogueList.dialogue_context.Length)
        {
            DialogueExit();
            return;
        }
        if(typingCoroutine == null)
        {
            string typingDialogyeData = dialogueList.dialogue_text[currentNPCDialogueIndex];
            dialogueContext.text = dialogueList.dialogue_context[currentNPCDialogueIndex];
            typingCoroutine = StartCoroutine(TypeNpcDialogue(typingDialogyeData));
        }
        else
        {
            SkipDialogue();
        }
    }
    private IEnumerator TypeNpcDialogue(string dialgue)
    {
        dialogueText.text = "";
        
        foreach(char i in dialgue.ToCharArray())
        {
            dialogueText.text += i;
            yield return new WaitForSeconds(typingSpeed);
        }
        yield return new WaitForSeconds(1.5f);
        SkipDialogue();

    }
    private void SkipDialogue()
    {
        if(typingCoroutine != null) StopCoroutine(typingCoroutine);
        dialogueContext.text = dialogueList.dialogue_context[currentNPCDialogueIndex];
        dialogueText.text = dialogueList.dialogue_text[currentNPCDialogueIndex];
        currentNPCDialogueIndex++;
        typingCoroutine = null;
        if(currentNPCDialogueIndex >= dialogueList.dialogue_context.Length)
        {
            Debug.Log("스킵 끝");
            DialogueExit();
        }
        else
        {
            string typingDialogyeData = dialogueList.dialogue_text[currentNPCDialogueIndex];
            dialogueContext.text = dialogueList.dialogue_context[currentNPCDialogueIndex];
            typingCoroutine = StartCoroutine(TypeNpcDialogue(typingDialogyeData));
        }
        
    }
    
    private void DialogueStart()
    {
        //gameObject.SetActive(true);
       
    }
    private void DialogueExit()
    {
        if(typingCoroutine != null) StopCoroutine(typingCoroutine);
        typingCoroutine = null;
        //gameObject.SetActive(false);
        currentNPCDialogueIndex = 0;
    }
}
