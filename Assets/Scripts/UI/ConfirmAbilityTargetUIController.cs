using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmAbilityTargetUIController : MonoBehaviour
{
    public GameObject confirmAbilityTargetUI;

    /*
     여기에 추가적으로 예상 피해량, 적중률 등등을 표기할 텍스트들이 있고 그걸 계산할 함수가 있어서 Display나 그 이후에 실행할 수 있게해야함
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
