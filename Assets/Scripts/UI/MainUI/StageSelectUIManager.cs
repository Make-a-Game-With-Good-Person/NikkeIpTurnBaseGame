using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class StageSelectUIManager : MonoBehaviour
{
    bool isStageEnterOpen;
    private int selectedStageIndex;
    public GameObject stagePanel;
    public Transform[] stages;      // 스테이지 배열
    public Button backButton;            // 뒤로가기 버튼
    public GameObject stageEnterPanel;        // 스테이지 입장창 UI
    public Button stageSelectBtn;
    Vector3[] originalPositions; 
    Vector3[] originalScale; 

    float stageEnterPanelSize;
    // Start is called before the first frame update
    void Start()
    {
        stageEnterPanelSize = stageEnterPanel.GetComponent<RectTransform>().rect.width;

        originalPositions = new Vector3[stages.Length];
        originalScale = new Vector3[stages.Length];

        stagePanel.SetActive(false);
        stageEnterPanel.SetActive(false);

        // 뒤로가기 버튼에 이벤트 연결
        backButton.onClick.AddListener(TurnOffScreen);
        stageSelectBtn.onClick.AddListener(OnStageEnterBtn);
    }

    public void OnStageEnterBtn()
    {
        FindObjectOfType<UserDataManager>().selectedStageIndex = selectedStageIndex;
        SceneManager.LoadScene(selectedStageIndex);
    }

    // 스테이지를 선택했을 때 호출 (선택한 스테이지 버튼을 중앙으로 이동, 입장창 열기)
    public void SelectStage(int index)
    {
        if (index < 0 || index >= stages.Length) return;

        isStageEnterOpen = true;

        selectedStageIndex = index + 1;

        // 선택된 캐릭터만 중앙으로 이동
        stages[index].transform.DOMove(new Vector3(-1, stages[index].transform.position.y, stages[index].transform.position.z), 0.5f).SetEase(Ease.OutExpo);

        // 나머지 캐릭터들은 원위치로 숨김
        for (int i = 0; i < stages.Length; i++)
        {
            if (i != index)
            {
                int curIndex = i;  // 새로운 변수로 i 값을 고정
                stages[i].transform.DOScale(0, 0.3f).OnComplete(() => stages[curIndex].gameObject.SetActive(false));
            }
        }

        // 상태창이 오른쪽 화면 밖에서 왼쪽으로 슬라이딩하며 나타나도록
        stageEnterPanel.transform.DOLocalMoveX(stageEnterPanelSize, 0.5f).SetEase(Ease.OutExpo);
        stageEnterPanel.SetActive(true);

        // 상태창의 투명도 변경 (투명 -> 선명)
        stageEnterPanel.GetComponent<CanvasGroup>().DOFade(1, 0.5f);

        // 각 캐릭터의 원래 위치와 크기를 저장
        for (int i = 0; i < stages.Length; i++)
        {
            originalPositions[i] = stages[i].transform.position;
            originalScale[i] = stages[i].transform.localScale;
        }
    }

    public void TurnOffScreen()
    {
        if (isStageEnterOpen)
        {
            for (int i = 0; i < stages.Length; i++)
            {
                stages[i].gameObject.SetActive(true);
                stages[i].transform.DOScale(originalScale[i], 0.3f); // 크기 복원
                stages[i].transform.DOMove(originalPositions[i], 0.5f).SetEase(Ease.OutExpo);

            }

            // 상태창 슬라이딩으로 오른쪽 화면 밖으로 사라짐
            stageEnterPanel.transform.DOLocalMoveX(1000f, 0.5f).SetEase(Ease.InExpo);

            // 상태창의 투명도 변경 (선명 -> 투명)
            stageEnterPanel.GetComponent<CanvasGroup>().DOFade(0, 0.5f).OnComplete(() => stageEnterPanel.SetActive(false));
            isStageEnterOpen = false;
        }
        else
        {
            // 선택된 캐릭터는 원위치로 복귀
            for (int i = 0; i < stages.Length; i++)
            {
                stages[i].gameObject.SetActive(false);
            }

            stagePanel.gameObject.SetActive(false);
        }
    }
}
