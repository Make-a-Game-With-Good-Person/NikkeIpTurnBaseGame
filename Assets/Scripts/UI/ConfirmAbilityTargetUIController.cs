using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ConfirmAbilityTargetUIController : MonoBehaviour
{
    public GameObject confirmAbilityTargetUI;

    /*
     ���⿡ �߰������� ���� ���ط�, ���߷� ����� ǥ���� �ؽ�Ʈ���� �ְ� �װ� ����� �Լ��� �־ Display�� �� ���Ŀ� ������ �� �ְ��ؾ���
     */
    public Text expectedBattleText;
    public Button cancelButton;
    public Button confirmButton;
    BattleCalculator battleCalculator;
    public void Display()
    {
        confirmAbilityTargetUI.SetActive(true);
    }
    public void Hide()
    {
        confirmAbilityTargetUI.SetActive(false);
    }

    public void SetExpectedText(Unit attacker, Unit target)
    {
        double truncatedValue = Math.Truncate(battleCalculator.CalculAccuracy(attacker, target)*100) / 100;
        expectedBattleText.text = $"���߷� : {truncatedValue * 100}%";
    }

    private void Start()
    {
        battleCalculator = new BattleCalculator();
        Hide();
    }
}
