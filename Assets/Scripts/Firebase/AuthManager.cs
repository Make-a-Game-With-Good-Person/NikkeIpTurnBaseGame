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
        // 객체 초기화
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
            Debug.Log(emailField + "로 로그인 성공");
            return true;
        }
        catch (Exception e)
        {
            Debug.Log("회원가입 실패: " + e.Message);
            return false;
        }
    }
    
    public async Task<bool> Register(string emailField, string passField, string passCheckField)
    {
        if (!passField.Equals(passCheckField))
        {
            Debug.Log("입력한 비밀번호와 재확인하는 비밀번호가 달라서 회원가입 불가");
            return false;
        }

        try
        {
            var result = await auth.CreateUserWithEmailAndPasswordAsync(emailField, passField);
            Debug.Log(emailField + "로 회원가입 성공");
            return true;
        }
        catch (Exception e)
        {
            Debug.Log("회원가입 실패: " + e.Message);
            return false;
        }
    }
}
