using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class SquadUIManager : MonoBehaviour
{
    bool isEquipOpen;
    public GameObject squadPanel;        // ������ â UI
    public SquadUICharacter[] characters;      // 3D ĳ���� �迭
    public GameObject equipPanel;       // ����â UI
    public Button backButton;            // �ڷΰ��� ��ư
    public Camera mainCamera;            // ���� ī�޶�

    Vector3[] originalPositions; // �� ĳ���� ����ġ ����
    Vector3[] originalScale; // �� ĳ���� ��ũ�� ����

    float equipPanelSize;

    private void Start()
    {
        originalPositions = new Vector3[characters.Length];
        originalScale = new Vector3[characters.Length];

        // �� ĳ������ ���� ��ġ�� ũ�⸦ ����
        for (int i = 0; i < characters.Length; i++)
        {
            originalPositions[i] = characters[i].transform.position;
            originalScale[i] = characters[i].transform.localScale;
        }

        equipPanelSize = equipPanel.GetComponent<RectTransform>().rect.width;
        // ������ â�� ó���� ��Ȱ��ȭ
        squadPanel.SetActive(false);
        equipPanel.SetActive(false); // ����â �ʱ� ���´� ����

        // �ڷΰ��� ��ư�� �̺�Ʈ ����
        backButton.onClick.AddListener(TurnOffScreen);
    }

    // ������ â�� �� �� ȣ��
    public void ShowSquad()
    {
        squadPanel.SetActive(true);
        equipPanel.SetActive(false); // ����â�� ����
    }

    // ĳ���͸� �������� �� ȣ�� (������ ĳ���͸� �߾����� �̵�, ����â ����)
    public void SelectCharacter(int index)
    {
        if (index < 0 || index >= characters.Length || isEquipOpen) return;

        isEquipOpen = true;

        // ���õ� ĳ���͸� �߾����� �̵�
        characters[index].transform.DOMove(new Vector3(-1, characters[index].transform.position.y, characters[index].transform.position.z), 0.5f).SetEase(Ease.OutExpo);

        // ������ ĳ���͵��� ����ġ�� ����
        for (int i = 0; i < characters.Length; i++)
        {
            if (i != index)
            {
                int curIndex = i;  // ���ο� ������ i ���� ����
                characters[i].transform.DOScale(0, 0.3f).OnComplete(() => characters[curIndex].gameObject.SetActive(false));
            }
        }

        // ����â�� ������ ȭ�� �ۿ��� �������� �����̵��ϸ� ��Ÿ������
        equipPanel.transform.DOLocalMoveX(equipPanelSize, 0.5f).SetEase(Ease.OutExpo);
        equipPanel.SetActive(true);

        // ����â�� ���� ���� (���� -> ����)
        equipPanel.GetComponent<CanvasGroup>().DOFade(1, 0.5f);
    }

    public void TurnOffScreen()
    {
        if (isEquipOpen)
        {
            // ����â �����̵����� ������ ȭ�� ������ �����
            equipPanel.transform.DOLocalMoveX(2000f, 0.5f).SetEase(Ease.InExpo);

            // ���õ� ĳ���ʹ� ����ġ�� ����
            for (int i = 0; i < characters.Length; i++)
            {
                characters[i].gameObject.SetActive(true);
                characters[i].transform.DOScale(originalScale[i], 0.3f); // ũ�� ����
                characters[i].transform.DOMove(originalPositions[i], 0.5f).SetEase(Ease.OutExpo);
                characters[i].animator.SetBool("Selected", false);
            }

            // ����â�� ���� ���� (���� -> ����)
            equipPanel.GetComponent<CanvasGroup>().DOFade(0, 0.5f).OnComplete(() => equipPanel.SetActive(false));
            isEquipOpen = false;
        }
        else
        {
            // ���õ� ĳ���ʹ� ����ġ�� ����
            for (int i = 0; i < characters.Length; i++)
            {
                characters[i].gameObject.SetActive(false);
            }

            squadPanel.gameObject.SetActive(false);
        }
    }
}
