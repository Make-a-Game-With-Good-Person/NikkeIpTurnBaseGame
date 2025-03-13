using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using UnityEngine.Events;

public class LoginSystem : MonoBehaviour
{
    public Transform loginCanvas;

    public Transform loginScreen;
    public Transform nicknameScreen;
    public Transform registerScreen;
    public Transform failLoginScreen;
    public Transform completeRegisterScreen;
    public Transform failRegisterScreen;

    public InputField idInput;
    public InputField pwInput;
    public InputField registerIdInput;
    public InputField registerPwInput;
    public InputField registerPwCheckInput;
    public InputField nicknameIdInput;

    AuthManager authManager;
    UserDataManager userDataManager;

    public Transform mainScreen;
    private void Start()
    {
        authManager = new AuthManager();
        userDataManager = UserDataManager.Instance;
    }

    #region TryFunc
    public void TryLogin()
    {
        _ = UserLogin();
    }
    public void TryRegister()
    {
        _ = UserRegsiter();
    }

    public void TryMakeNickName()
    {
        _ = UserNickName();
    }
    #endregion

    #region Task
    async Task UserLogin()
    {
        bool success = await authManager.Login(idInput.text, pwInput.text);
        if (success) // 로그인 성공
        {
            userDataManager.uid = authManager.GetCurrenUserID();

            bool dataLoad = await userDataManager.LoadUserData(userDataManager.uid);

            if (dataLoad)
            {
                Debug.Log(" 이미 닉네임이 존재하니 처음 온 사람이 아니니깐 바로 게임을 시작하도록 해요 ");
                TurnOffLoginScreen();
            }
            else
            {
                TurnOnNickNameMakeScreen();
            }
        }
        else
        {
            // 로그인 실패창을 연다.
            TurnOnLoginFailScreen();
        }
    }

    async Task UserRegsiter()
    {
        bool success = await authManager.Register(registerIdInput.text, registerPwInput.text, registerPwCheckInput.text);
        if (success) // 회원가입성공
        {
            // 회원가입 성공창을 띄운다.
            completeRegisterScreen.gameObject.SetActive(true);
        }
        else
        {
            // 회원가입 실패창을 띄운다.
            failRegisterScreen.gameObject.SetActive(true);
        }
    }

    async Task UserNickName()
    {
        await userDataManager.InitializeUserData(userDataManager.uid, nicknameIdInput.text);

        Debug.Log("닉네임 생성 성공!");
        TurnOffNickNameMakeScreen();
        TurnOffLoginFailScreen();
        TurnOnMainCanvas();
    }
    #endregion

    #region ToggleScreen
    public void TurnOffLoginScreen()
    {
        loginCanvas.gameObject.SetActive(false);
        TurnOnMainCanvas();
    }
    public void TurnOnNickNameMakeScreen()
    {
        loginScreen.gameObject.SetActive(false);

        nicknameScreen.gameObject.SetActive(true);
    }

    public void TurnOffNickNameMakeScreen()
    {
        nicknameScreen.gameObject.SetActive(false);
    }

    public void TurnOnLoginFailScreen()
    {
        failLoginScreen.gameObject.SetActive(true);
    }

    public void TurnOffLoginFailScreen()
    {
        failLoginScreen.gameObject.SetActive(false);
    }

    //회원가입 창을 띄운다.
    public void TurnOnRegisterScreen()
    {
        loginScreen.gameObject.SetActive(false);
        registerScreen.gameObject.SetActive(true);
        ResetRegsiterScreen();
    }

    //회원가입 창을 끈다.
    public void TurnOffRegisterScreen()
    {
        ResetRegsiterScreen();
        registerScreen.gameObject.SetActive(false);
        loginScreen.gameObject.SetActive(true);
    }

    void ResetRegsiterScreen()
    {
        completeRegisterScreen.gameObject.SetActive(false);
        failRegisterScreen.gameObject.SetActive(false);

        registerIdInput.text = string.Empty;
        registerPwInput.text = string.Empty;
        registerPwCheckInput.text = string.Empty;
    }


    public void TurnOnMainCanvas()
    {
        mainScreen.gameObject.SetActive(true);
    }
    #endregion

}
