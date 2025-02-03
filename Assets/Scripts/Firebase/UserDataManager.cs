using Firebase.Auth;
using Firebase.Firestore;
using Firebase.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading.Tasks;

public class UserDataManager
{
    FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
    
    /*public void LoadUserData(FirebaseAuth auth) // 유저 이름, 레벨, 경험치를 읽어와 딕셔너리로 변환 후 각각 사용하기 쉽게 변수화 시킴
    {
        string uid = auth.CurrentUser.UserId;
        DocumentReference docRef = db.Collection(uid).Document("Users");

        docRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                DocumentSnapshot snapshot = task.Result;
                if (snapshot.Exists) // 성공적으로 로드한 상태, 정보들을 변수로 변환하고 적용한 뒤 이후 UI를 출력해 게임을 진행하도록 하면 될듯
                {
                    Debug.Log("user data found!");
                }
                else
                {
                    Debug.Log("No user data found!");
                }
            }
            else
            {
                Debug.LogError("Failed to load user data: " + task.Exception);
            }
        });
    }*/

    #region Task
    // Firestore에서 닉네임이 존재하는지 확인하는 함수
    public async Task<bool> CheckIfNicknameExists(string userId)
    {
        var userDoc = await FirebaseFirestore.DefaultInstance
                            .Collection(userId)
                            .Document("userInfo")
                            .GetSnapshotAsync();

        if (userDoc.Exists)
        {
            var nickname = userDoc.GetValue<string>("nickname");
            if (!string.IsNullOrEmpty(nickname))
            {
                return true;
            }
        }

        return false;
    }

    // 닉네임 생성 후 Firestore에 저장하는 함수
    public async Task SetNickname(string userId, string nickname)
    {
        var userDocRef = FirebaseFirestore.DefaultInstance.Collection(userId).Document("userInfo");

        var docSnapshot = await userDocRef.GetSnapshotAsync();
        if (docSnapshot.Exists)
        {
            // 문서가 존재하면 nickname만 업데이트
            await userDocRef.UpdateAsync("nickname", nickname);
            Debug.Log("닉네임 업데이트 성공: " + nickname);
        }
        else
        {
            // 문서가 존재하지 않으면 새로 생성
            await userDocRef.SetAsync(new { nickname = nickname });
            Debug.Log("새로운 문서로 닉네임 생성 성공: " + nickname);
        }
    }
    #endregion
}
