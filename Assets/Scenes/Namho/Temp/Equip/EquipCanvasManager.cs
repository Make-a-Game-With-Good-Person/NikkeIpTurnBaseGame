using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipCanvasManager : MonoBehaviour
{
    UserDataManager userDataManager;
    EquipButtonManager equipButtonManager;

    public Text selectedEquipType;
    public Text upgradeCreditCostText;
    public Text upgradeBattleDataCostText;

    public Transform upgradeCanvas;
    public Transform sucessCanvas;
    public Transform failCanvas;

    // Start is called before the first frame update
    void Start()
    {
        userDataManager = UserDataManager.Instance;
        equipButtonManager = FindObjectOfType<EquipButtonManager>();
        if (equipButtonManager != null)
        {
            equipButtonManager.updateUpgradeCanvas.AddListener(UpdateUpgradeCanvas);
        }

    }
    #region TurnOn,Off
    public void TurnOnUpgradeCanvas()
    {
        upgradeCanvas.gameObject.SetActive(true);
    }

    public void TurnOffUpgradeCanvas()
    {
        upgradeCanvas.gameObject.SetActive(false);
    }

    public void TurnOnSuccessCanvas()
    {
        sucessCanvas.gameObject.SetActive(true);
    }

    public void TurnOffSuccessCanvas()
    {
        sucessCanvas.gameObject.SetActive(false);
    }

    public void TurnOnFailCanvas()
    {
        failCanvas.gameObject.SetActive(true);
    }

    public void TurnOffFailCanvas()
    {
        failCanvas.gameObject.SetActive(false);
    }

    #endregion

    #region Update

    void UpdateUpgradeCanvas(BaseItem selectedItem, int creditCost, int battleDataCost)
    {
        selectedEquipType.text = selectedItem.EquipType.ToString();
        upgradeCreditCostText.text = creditCost.ToString();
        upgradeBattleDataCostText.text = battleDataCost.ToString();
    }
    #endregion
}
