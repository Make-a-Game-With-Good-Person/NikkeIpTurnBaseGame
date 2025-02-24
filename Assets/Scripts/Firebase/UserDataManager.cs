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
            var equip = snapshot.GetValue<Dictionary<string, object>>("equip");

            UserData.SetData(nickname, stage,
                Convert.ToInt32(goods["credits"]),
                Convert.ToInt32(goods["battleData"]),
                new Dictionary<string, int>
                {
                    { "helmetLevel", Convert.ToInt32(equip["helmetLevel"]) },
                    { "armorLevel", Convert.ToInt32(equip["armorLevel"]) },
                    { "glovesLevel", Convert.ToInt32(equip["glovesLevel"]) },
                    { "bootsLevel", Convert.ToInt32(equip["bootsLevel"]) }
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
            { "equip", UserData.Equip },
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
            { "helmetLevel", 1 },
            { "armorLevel", 1 },
            { "glovesLevel", 1 },
            { "bootsLevel", 1 }
        });
        await SaveUserData(userId);
    }
    #endregion

}