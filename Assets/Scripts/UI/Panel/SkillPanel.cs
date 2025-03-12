using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillPanel : MonoBehaviour
{
    public SkillUIButton[] skillButtons = new SkillUIButton[2];
    public Text statusDescription;
    public void SetSkillPanel(BaseSkillStatus[] skills)
    {
        for (int i = 0; i < skillButtons.Length; i++)
        {
            skillButtons[i].skillStatus = skills[i];
        }

        statusDescription.text = skillButtons[0].skillStatus.skillDescription;
    }

    public void SetSkillDescription(SkillUIButton skillButton)
    {
        statusDescription.text = skillButton.skillStatus.skillDescription;
    }
    
}
