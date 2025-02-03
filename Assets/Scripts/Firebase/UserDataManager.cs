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
    
    /*public void LoadUserData(FirebaseAuth auth) // ���� �̸�, ����, ����ġ�� �о�� ��ųʸ��� ��ȯ �� ���� ����ϱ� ���� ����ȭ ��Ŵ
    {
        string uid = auth.CurrentUser.UserId;
        DocumentReference docRef = db.Collection(uid).Document("Users");

        docRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                DocumentSnapshot snapshot = task.Result;
                if (snapshot.Exists) // ���������� �ε��� ����, �������� ������ ��ȯ�ϰ� ������ �� ���� UI�� ����� ������ �����ϵ��� �ϸ� �ɵ�
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
    // Firestore���� �г����� �����ϴ��� Ȯ���ϴ� �Լ�
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

    // �г��� ���� �� Firestore�� �����ϴ� �Լ�
    public async Task SetNickname(string userId, string nickname)
    {
        var userDocRef = FirebaseFirestore.DefaultInstance.Collection(userId).Document("userInfo");

        var docSnapshot = await userDocRef.GetSnapshotAsync();
        if (docSnapshot.Exists)
        {
            // ������ �����ϸ� nickname�� ������Ʈ
            await userDocRef.UpdateAsync("nickname", nickname);
            Debug.Log("�г��� ������Ʈ ����: " + nickname);
        }
        else
        {
            // ������ �������� ������ ���� ����
            await userDocRef.SetAsync(new { nickname = nickname });
            Debug.Log("���ο� ������ �г��� ���� ����: " + nickname);
        }
    }
    #endregion
}
