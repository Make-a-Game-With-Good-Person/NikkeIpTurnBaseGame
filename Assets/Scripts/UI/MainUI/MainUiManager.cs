using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUiManager : MonoBehaviour
{
    public Transform[] characters;
    public Transform squadPanel;
    public SquadUIManager squadUIManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void ShowSquadUI()
    {
        squadUIManager.ShowSquad();
        foreach (Transform character in characters)
        {
            character.gameObject.SetActive(true);
            character.transform.localScale = Vector3.zero; // ó���� ������ �ʰ� ����
            character.transform.DOScale(1f, 0.3f).SetEase(Ease.OutBack); // �ε巴�� ����
        }
    }

}
