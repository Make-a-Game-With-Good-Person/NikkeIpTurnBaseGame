using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipPanel : MonoBehaviour
{
    public EquipButton[] equipButtons = new EquipButton[4];
    public Text statusDescription;
    public void SetEquipPanel(EquipInven inven)
    {
        inven.InitItemValue();

        for(int i = 0; i < equipButtons.Length; i++)
        {
            equipButtons[i].item = inven.baseItems[i];
            equipButtons[i].GetComponent<Button>().image.sprite = equipButtons[i].item.ItemIcon;
        }
    }

    public void SetDescription(string selectedCharacter)
    {
        statusDescription.text = selectedCharacter;
    }
}
