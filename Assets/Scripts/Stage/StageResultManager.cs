using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageResultManager : MonoBehaviour
{
    UserDataManager userDataManager;
    EquipCanvasManager equipCanvasManager;
    public int stageLv;
    public int rewardCredits;
    public int rewardBattleDatas;
    // Start is called before the first frame update
    void Start()
    {
        userDataManager = UserDataManager.Instance;
    }

    public async void StageClear()
    {
        userDataManager.UserData.UpdateStageClear(stageLv, rewardCredits, rewardBattleDatas);
        await userDataManager.SaveUserData(userDataManager.uid);

        equipCanvasManager?.UpdateEquipCanvas();
    }

    private void Update()
    {
        // 테스트용
        if (Input.GetKeyDown(KeyCode.F1))
        {
            StageClear();
        }
    }
}
