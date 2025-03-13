using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquadUICharacter : MonoBehaviour
{
    public int index;
    public SquadUIManager uiManager;
    public Animator animator;
    public EquipPanel equipPanel;
    public EquipInven inven;
    public SkillPanel skillPanel;
    public BaseSkillStatus[] skills;
    public string charDescription;
    public SquadUICamController camController;
    public void SelectCharacter()
    {
        uiManager.SelectCharacter(index);
        animator.SetBool("Selected", true);
        equipPanel.SetEquipPanel(inven);
        equipPanel.SetDescription(charDescription);
        skillPanel.SetSkillPanel(skills);
        camController.SwitchToSelectedView(this.transform);
    }
    
    
}
