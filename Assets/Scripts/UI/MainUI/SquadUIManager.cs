using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class SquadUIManager : MonoBehaviour
{
    bool isEquipOpen;
    public GameObject squadPanel;        // 스쿼드 창 UI
    public SquadUICharacter[] characters;      // 3D 캐릭터 배열
    public GameObject equipPanel;       // 상태창 UI
    public Button backButton;            // 뒤로가기 버튼
    public Camera mainCamera;            // 메인 카메라

    Vector3[] originalPositions; // 각 캐릭터 원위치 저장
    Vector3[] originalScale; // 각 캐릭터 원크기 저장

    float equipPanelSize;

    private void Start()
    {
        originalPositions = new Vector3[characters.Length];
        originalScale = new Vector3[characters.Length];

        equipPanelSize = equipPanel.GetComponent<RectTransform>().rect.width;
        // 스쿼드 창은 처음에 비활성화
        squadPanel.SetActive(false);
        equipPanel.SetActive(false); // 상태창 초기 상태는 숨김

        // 뒤로가기 버튼에 이벤트 연결
        backButton.onClick.AddListener(TurnOffScreen);
    }

    // 스쿼드 창을 열 때 호출
    public void ShowSquad()
    {
        squadPanel.SetActive(true);
        equipPanel.SetActive(false); // 상태창은 숨김
        PositionAndScaleCharacters(); // 캐릭터 배치
    }

    // 캐릭터 배치 함수
    private void PositionAndScaleCharacters()
    {
        Camera cam = mainCamera;
        float distance = 10f; // 캐릭터들과 카메라 간 거리

        // 화면 비율 계산 
        float screenRatio = (float)Screen.width / (float)Screen.height;

        // 카메라의 특정 위치를 기준으로 캐릭터들 배치
        Vector3 leftPos = cam.ViewportToWorldPoint(new Vector3(0.3f, 0.25f, distance));
        Vector3 centerPos = cam.ViewportToWorldPoint(new Vector3(0.5f, 0.25f, distance));
        Vector3 rightPos = cam.ViewportToWorldPoint(new Vector3(0.7f, 0.25f, distance));

        characters[0].transform.position = leftPos;
        characters[1].transform.position = centerPos;
        characters[2].transform.position = rightPos;

        float baseScale = 1f; // 기본 크기

        float adjustedScale = baseScale * (screenRatio / 1.7f);

        foreach (var character in characters)
        {
            character.transform.localScale = new Vector3(adjustedScale, adjustedScale, adjustedScale);
        }

        // 각 캐릭터의 원래 위치와 크기를 저장
        for (int i = 0; i < characters.Length; i++)
        {
            originalPositions[i] = characters[i].transform.position;
            originalScale[i] = characters[i].transform.localScale;
        }

    }

    // 캐릭터를 선택했을 때 호출 (선택한 캐릭터만 중앙으로 이동, 상태창 열기)
    public void SelectCharacter(int index)
    {
        if (index < 0 || index >= characters.Length || isEquipOpen) return;

        isEquipOpen = true;

        // 선택된 캐릭터만 중앙으로 이동
        characters[index].transform.DOMove(new Vector3(-1, characters[index].transform.position.y, characters[index].transform.position.z), 0.5f).SetEase(Ease.OutExpo);

        // 나머지 캐릭터들은 원위치로 숨김
        for (int i = 0; i < characters.Length; i++)
        {
            if (i != index)
            {
                int curIndex = i;  // 새로운 변수로 i 값을 고정
                characters[i].transform.DOScale(0, 0.3f).OnComplete(() => characters[curIndex].gameObject.SetActive(false));
            }
        }

        // 상태창이 오른쪽 화면 밖에서 왼쪽으로 슬라이딩하며 나타나도록
        equipPanel.transform.DOLocalMoveX(equipPanelSize, 0.5f).SetEase(Ease.OutExpo);
        equipPanel.SetActive(true);

        // 상태창의 투명도 변경 (투명 -> 선명)
        equipPanel.GetComponent<CanvasGroup>().DOFade(1, 0.5f);
    }

    public void TurnOffScreen()
    {
        if (isEquipOpen)
        {
            // 선택된 캐릭터는 원위치로 복귀
            for (int i = 0; i < characters.Length; i++)
            {
                characters[i].gameObject.SetActive(true);
                characters[i].transform.DOScale(originalScale[i], 0.3f); // 크기 복원
                characters[i].transform.DOMove(originalPositions[i], 0.5f).SetEase(Ease.OutExpo);
                characters[i].animator.SetBool("Selected", false);
            }

            // 상태창 슬라이딩으로 오른쪽 화면 밖으로 사라짐
            equipPanel.transform.DOLocalMoveX(1000f, 0.5f).SetEase(Ease.InExpo);

            // 상태창의 투명도 변경 (선명 -> 투명)
            equipPanel.GetComponent<CanvasGroup>().DOFade(0, 0.5f).OnComplete(() => equipPanel.SetActive(false));
            isEquipOpen = false;
        }
        else
        {
            // 선택된 캐릭터는 원위치로 복귀
            for (int i = 0; i < characters.Length; i++)
            {
                characters[i].gameObject.SetActive(false);
            }

            squadPanel.gameObject.SetActive(false);
        }
    }
}
