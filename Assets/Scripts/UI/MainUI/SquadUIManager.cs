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
        PositionAndScaleCharacters(); // ĳ���� ��ġ
    }

    // ĳ���� ��ġ �Լ�
    private void PositionAndScaleCharacters()
    {
        Camera cam = mainCamera;
        float distance = 10f; // ĳ���͵�� ī�޶� �� �Ÿ�

        // ȭ�� ���� ��� 
        float screenRatio = (float)Screen.width / (float)Screen.height;

        // ī�޶��� Ư�� ��ġ�� �������� ĳ���͵� ��ġ
        Vector3 leftPos = cam.ViewportToWorldPoint(new Vector3(0.3f, 0.25f, distance));
        Vector3 centerPos = cam.ViewportToWorldPoint(new Vector3(0.5f, 0.25f, distance));
        Vector3 rightPos = cam.ViewportToWorldPoint(new Vector3(0.7f, 0.25f, distance));

        characters[0].transform.position = leftPos;
        characters[1].transform.position = centerPos;
        characters[2].transform.position = rightPos;

        float baseScale = 1f; // �⺻ ũ��

        float adjustedScale = baseScale * (screenRatio / 1.7f);

        foreach (var character in characters)
        {
            character.transform.localScale = new Vector3(adjustedScale, adjustedScale, adjustedScale);
        }

        // �� ĳ������ ���� ��ġ�� ũ�⸦ ����
        for (int i = 0; i < characters.Length; i++)
        {
            originalPositions[i] = characters[i].transform.position;
            originalScale[i] = characters[i].transform.localScale;
        }

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
            // ���õ� ĳ���ʹ� ����ġ�� ����
            for (int i = 0; i < characters.Length; i++)
            {
                characters[i].gameObject.SetActive(true);
                characters[i].transform.DOScale(originalScale[i], 0.3f); // ũ�� ����
                characters[i].transform.DOMove(originalPositions[i], 0.5f).SetEase(Ease.OutExpo);
                characters[i].animator.SetBool("Selected", false);
            }

            // ����â �����̵����� ������ ȭ�� ������ �����
            equipPanel.transform.DOLocalMoveX(1000f, 0.5f).SetEase(Ease.InExpo);

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
