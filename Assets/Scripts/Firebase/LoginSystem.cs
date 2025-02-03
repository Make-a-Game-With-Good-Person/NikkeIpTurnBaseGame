using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;

public class LoginSystem : MonoBehaviour
{
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
    string uid;
    private void Start()
    {
        authManager = new AuthManager();
        userDataManager = new UserDataManager();
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
        if (success) // ȸ�����Լ���
        {
            uid = authManager.GetCurrenUserID();

            bool hasNickName = await userDataManager.CheckIfNicknameExists(uid);

            if (hasNickName)
            {
                Debug.Log(" �̹� �г����� �����ϴ� ó�� �� ����� �ƴϴϱ� �ٷ� ������ �����ϵ��� �ؿ� ");
            }
            else
            {
                TurnOnNickNameMakeScreen();
            }
        }
        else
        {
            // �α��� ����â�� ����.
            TurnOnLoginFailScreen();
        }
    }

    async Task UserRegsiter()
    {
        bool success = await authManager.Register(registerIdInput.text, registerPwInput.text, registerPwCheckInput.text);
        if (success) // ȸ�����Լ���
        {
            // ȸ������ ����â�� ����.
            completeRegisterScreen.gameObject.SetActive(true);
        }
        else
        {
            // ȸ������ ����â�� ����.
            failRegisterScreen.gameObject.SetActive(true);
        }
    }

    async Task UserNickName()
    {
        await userDataManager.SetNickname(uid, nicknameIdInput.text);
    }
    #endregion

    #region ToggleScreen
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

    //ȸ������ â�� ����.
    public void TurnOnRegisterScreen()
    {
        loginScreen.gameObject.SetActive(false);
        registerScreen.gameObject.SetActive(true);
        ResetRegsiterScreen();
    }

    //ȸ������ â�� ����.
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

    #endregion

}
