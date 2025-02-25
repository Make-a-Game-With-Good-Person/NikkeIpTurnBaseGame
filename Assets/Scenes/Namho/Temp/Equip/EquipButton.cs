using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipButton : MonoBehaviour
{
    public int index;
    public EquipInven equipInven;
    public BaseItem item;

    private void OnEnable()
    {
        item = equipInven.baseItems[index];
    }
}
