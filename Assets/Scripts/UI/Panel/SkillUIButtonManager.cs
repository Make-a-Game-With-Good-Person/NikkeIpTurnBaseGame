using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SkillUIButtonManager : MonoBehaviour
{
    public SkillPanel skillPanel;

    SkillUIButton selectedSkillButton;
    public void CilckSkillUIButton()
    {
        selectedSkillButton = EventSystem.current.currentSelectedGameObject.GetComponent<SkillUIButton>();
        skillPanel.SetSkillDescription(selectedSkillButton);
    }
}
