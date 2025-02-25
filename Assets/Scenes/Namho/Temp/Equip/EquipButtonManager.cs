using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class EquipButtonManager : MonoBehaviour
{
    public UnityEvent<BaseItem, int, int> updateUpgradeCanvas;
    public UnityEvent updateEquipCanvas;

    EquipSystemManager equipSystemManager;
    EquipCanvasManager equipCanvasManager;
    BaseItem selectedItem;
    int baseCreditCost = 1;
    int baseBattleDataCost = 1;

    public int creditCost;
    public int battleDataCost;

    private void Start()
    {
        equipSystemManager = FindObjectOfType<EquipSystemManager>();
        equipCanvasManager = FindObjectOfType<EquipCanvasManager>();
    }


    public void SelectItemToUpgrade()
    {
        equipCanvasManager.TurnOnUpgradeCanvas();
        selectedItem = EventSystem.current.currentSelectedGameObject.GetComponent<EquipButton>().item;
        switch (selectedItem.EquipType)
        {
            case EquipType.helmet:
                creditCost = baseCreditCost * 10 * selectedItem.ItemLv;
                battleDataCost = baseBattleDataCost * 10 * selectedItem.ItemLv;
                break;
            case EquipType.armor:
                creditCost = baseCreditCost * 20 * selectedItem.ItemLv;
                battleDataCost = baseBattleDataCost * 20 * selectedItem.ItemLv;
                break;
            case EquipType.gloves:
                creditCost = baseCreditCost * 30 * selectedItem.ItemLv;
                battleDataCost = baseBattleDataCost * 30 * selectedItem.ItemLv;
                break;
            case EquipType.boots:
                creditCost = baseCreditCost * 40 * selectedItem.ItemLv;
                battleDataCost = baseBattleDataCost * 40 * selectedItem.ItemLv;
                break;
        }

        updateUpgradeCanvas?.Invoke(selectedItem, creditCost, battleDataCost);
    }

    public void UpgradeEquip()
    {
        _ = TryUpgrade(selectedItem.EquipType, creditCost, battleDataCost);
    }

    async Task TryUpgrade(EquipType equipType, int creditCost, int battleDataCost)
    {
        if (selectedItem == null) return;

        bool success = await equipSystemManager.UpgradeEquipment(equipType, creditCost, battleDataCost);

        if (success)
        {
            equipCanvasManager?.TurnOnSuccessCanvas();
            updateEquipCanvas?.Invoke();
        }
        else
        {
            equipCanvasManager?.TurnOnFailCanvas();
        }
    }
}
