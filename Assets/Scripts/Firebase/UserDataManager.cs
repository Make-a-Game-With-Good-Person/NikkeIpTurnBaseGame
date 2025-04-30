using Firebase.Auth;
using Firebase.Firestore;
using Firebase.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading.Tasks;

public class UserDataManager : MonoBehaviour
{
    public static UserDataManager Instance { get; private set; }
    private FirebaseFirestore db;
    public UserDataCache UserData { get; private set; } = new UserDataCache();
    public string uid;
    public int selectedStageIndex;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            db = FirebaseFirestore.DefaultInstance;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #region Save,Load,Init
    public async Task<bool> LoadUserData(string userId)
    {
        var userRef = db.Collection("users").Document(userId);
        var snapshot = await userRef.GetSnapshotAsync();

        if (snapshot.Exists)
        {
            var nickname = snapshot.GetValue<string>("nickname");
            var stage = snapshot.GetValue<int>("stage");
            var goods = snapshot.GetValue<Dictionary<string, object>>("goods");
            var equipLevel = snapshot.GetValue<Dictionary<string, object>>("equipLevel");

            UserData.SetData(nickname, stage,
                Convert.ToInt32(goods["credits"]),
                Convert.ToInt32(goods["battleData"]),
                new Dictionary<string, int>
                {
                    { "helmet", Convert.ToInt32(equipLevel["helmet"]) },
                    { "armor", Convert.ToInt32(equipLevel["armor"]) },
                    { "gloves", Convert.ToInt32(equipLevel["gloves"]) },
                    { "boots", Convert.ToInt32(equipLevel["boots"]) }
                });

            return true;
        }
        else
        {
            return false;
        }
    }

    public async Task SaveUserData(string userId)
    {
        var userRef = db.Collection("users").Document(userId);
        var updateData = new Dictionary<string, object>
        {
            { "nickname", UserData.Nickname },
            { "stage", UserData.Stage },
            { "equipLevel", UserData.EquipLevel },
            { "goods", new Dictionary<string, object>
                {
                    { "credits", UserData.Credits },
                    { "battleData", UserData.BattleData }
                }
            }
        };
        try
        {
            await userRef.SetAsync(updateData, SetOptions.MergeAll);
            Debug.Log($"[SaveUserData] 유저 데이터 저장 성공: {userId}");
        }
        catch (Exception e)
        {
            Debug.LogError($"[SaveUserData] 유저 데이터 저장 실패: {e.Message}");
        }
    }


    public async Task InitializeUserData(string userId, string nickname)
    {
        UserData.SetData(nickname, 1, 0, 0, new Dictionary<string, int>
        {
            { "helmet", 1 },
            { "armor", 1 },
            { "gloves", 1 },
            { "boots", 1 }
        });
        await SaveUserData(userId);
    }
    #endregion

}