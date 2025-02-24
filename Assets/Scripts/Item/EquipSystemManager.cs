using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading.Tasks;
using Firebase.Firestore;
using Unity.VisualScripting;

public class EquipSystemManager : MonoBehaviour
{
    public EquipType testType;
    public int testCredit;
    public int testBattleData;
    private FirebaseFirestore db;
    UserDataManager userDataManager;
    int credits;
    int battleDatas;

    private void Start()
    {
        db = FirebaseFirestore.DefaultInstance;
        userDataManager = UserDataManager.Instance;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5))
        {
            Upgrade();
        }
    }

    public async void Upgrade()
    {
        bool success = await UpgradeEquipment(testType, testCredit, testBattleData);
        if (success)
        {
            Debug.Log("���������� ��ȭ �Ϸ�");
        }
        else
        {
            Debug.Log("��ȭ ����");
        }
    }

    async Task<bool> UpgradeEquipment(EquipType equipType, int creditsCost, int battleDataCost)
    {
        var userRef = db.Collection("users").Document(userDataManager.uid);

        try
        {
            await db.RunTransactionAsync(async transaction =>
            {
                var snapshot = await transaction.GetSnapshotAsync(userRef);

                if (!snapshot.Exists)
                {
                    throw new Exception("�����Ͱ� �������� ����");
                }

                //���� ������ �ִ� �ֽ� ������ ��������
                credits = snapshot.GetValue<int>("goods.credits");
                battleDatas = snapshot.GetValue<int>("goods.battleData");
                var equip = snapshot.GetValue<Dictionary<string, int>>("equipLevel"); // <��� ���� �̸�, int���� ����>

                // ���׷��̵��� ����� ���� ���� ��������
                if (!equip.ContainsKey(equipType.ToString()))
                {
                    throw new Exception("�������� �ʴ� ���");
                }

                // ��� üũ
                if (credits < creditsCost || battleDatas < battleDataCost)
                {
                    throw new Exception("��ȭ�� �ʿ��� ��ȭ ����");
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
            Debug.LogError($"[UpgradeEquipment] ��� ��ȭ ����: {e.Message}");
            return false;
        }
    }

    void CalculUpgrade(int upgradeCost, int battleDataCost)
    {
        credits -= upgradeCost;
        battleDatas -= battleDataCost;
    }


    
}
