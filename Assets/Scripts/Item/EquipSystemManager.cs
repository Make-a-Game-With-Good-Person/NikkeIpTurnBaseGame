using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading.Tasks;
using Firebase.Firestore;
using Unity.VisualScripting;

public class EquipSystemManager : MonoBehaviour
{
    private FirebaseFirestore db;
    UserDataManager userDataManager;
    int credits;
    int battleDatas;

    private void Start()
    {
        db = FirebaseFirestore.DefaultInstance;
        userDataManager = UserDataManager.Instance;
    }

    public async Task<bool> UpgradeEquipment(EquipType equipType, int creditsCost, int battleDataCost)
    {
        var userRef = db.Collection("users").Document(userDataManager.uid);

        try
        {
            await db.RunTransactionAsync(async transaction =>
            {
                var snapshot = await transaction.GetSnapshotAsync(userRef);

                if (!snapshot.Exists)
                {
                    throw new Exception("데이터가 존재하지 않음");
                }

                //현재 서버에 있는 최신 데이터 가져오기
                credits = snapshot.GetValue<int>("goods.credits");
                battleDatas = snapshot.GetValue<int>("goods.battleData");
                var equip = snapshot.GetValue<Dictionary<string, int>>("equipLevel"); // <장비 레벨 이름, int값의 레벨>

                // 업그레이드할 장비의 현재 레벨 가져오기
                if (!equip.ContainsKey(equipType.ToString()))
                {
                    throw new Exception("존재하지 않는 장비");
                }

                // 비용 체크
                if (credits < creditsCost || battleDatas < battleDataCost)
                {
                    throw new Exception("강화에 필요한 재화 부족");
                }

                credits -= creditsCost;
                battleDatas -= battleDataCost;
                equip[equipType.ToString()]++;

                transaction.Update(userRef, new Dictionary<string, object>
            {
                { "goods.credits", credits },
                { "goods.battleData", battleDatas },
                { "equipLevel", equip }
            });

                userDataManager.UserData.UpdateEquipmentUpgrade(creditsCost, battleDataCost, equipType);

            }
            );
            return true;
        }
        catch (Exception e)
        {
            Debug.LogError($"[UpgradeEquipment] 장비 강화 실패: {e.Message}");
            return false;
        }
    }

}
