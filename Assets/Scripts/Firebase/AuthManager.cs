using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Auth;
using System.Threading.Tasks;
using System;

public class AuthManager
{
    public FirebaseAuth auth;

    public AuthManager()
    {
        // ��ü �ʱ�ȭ
        auth = FirebaseAuth.DefaultInstance;
    }

    public string GetCurrenUserID()
    {
        return auth.CurrentUser?.UserId;
    }

    public async Task<bool> Login(string emailField, string passField)
    {
        try
        {
            var result = await auth.SignInWithEmailAndPasswordAsync(emailField, passField);
            Debug.Log(emailField + "�� �α��� ����");
            return true;
        }
        catch (Exception e)
        {
            Debug.Log("ȸ������ ����: " + e.Message);
            return false;
        }
    }
    
    public async Task<bool> Register(string emailField, string passField, string passCheckField)
    {
        if (!passField.Equals(passCheckField))
        {
            Debug.Log("�Է��� ��й�ȣ�� ��Ȯ���ϴ� ��й�ȣ�� �޶� ȸ������ �Ұ�");
            return false;
        }

        try
        {
            var result = await auth.CreateUserWithEmailAndPasswordAsync(emailField, passField);
            Debug.Log(emailField + "�� ȸ������ ����");
            return true;
        }
        catch (Exception e)
        {
            Debug.Log("ȸ������ ����: " + e.Message);
            return false;
        }
    }
}
