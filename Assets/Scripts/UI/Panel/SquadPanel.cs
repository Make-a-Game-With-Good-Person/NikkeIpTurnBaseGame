using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SquadPanel : MonoBehaviour
{
    public EquipButtonManager equipButtonManager;
    UserDataCache userDataCache;
    public Text creditText;
    public Text battleDataText;
    private void OnEnable()
    {
        userDataCache = UserDataManager.Instance.UserData;

        creditText.text = userDataCache.Credits.ToString();
        battleDataText.text = userDataCache.BattleData.ToString();
    }

    private void Start()
    {
        equipButtonManager.updateEquipCanvas.AddListener(UpdateGoods);
    }

    public void UpdateGoods()
    {
        creditText.text = userDataCache.Credits.ToString();
        battleDataText.text = userDataCache.BattleData.ToString();
    }
}
