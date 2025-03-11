using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipPanel : MonoBehaviour
{
    public EquipButton[] equipButtons = new EquipButton[4];

    public void SetEquipPanel(EquipInven inven)
    {
        inven.InitItemValue();

        for(int i = 0; i < equipButtons.Length; i++)
        {
            equipButtons[i].item = inven.baseItems[i];
        }
    }
}
