using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmAbilityTargetUIController : MonoBehaviour
{
    public GameObject confirmAbilityTargetUI;

    /*
     ���⿡ �߰������� ���� ���ط�, ���߷� ����� ǥ���� �ؽ�Ʈ���� �ְ� �װ� ����� �Լ��� �־ Display�� �� ���Ŀ� ������ �� �ְ��ؾ���
     */
    public Button cancelButton;
    public Button confirmButton;
    public void Display()
    {
        confirmAbilityTargetUI.SetActive(true);
    }
    public void Hide()
    {
        confirmAbilityTargetUI.SetActive(false);
    }

    private void Start()
    {
        Hide();
    }
}
