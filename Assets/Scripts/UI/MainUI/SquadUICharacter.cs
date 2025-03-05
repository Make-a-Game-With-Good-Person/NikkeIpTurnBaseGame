using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquadUICharacter : MonoBehaviour
{
    public int index;
    public SquadUIManager uiManager;
    public Animator animator;

    public void SelectCharacter()
    {
        uiManager.SelectCharacter(index);
        animator.SetBool("Selected", true);
    }
    
    
}
