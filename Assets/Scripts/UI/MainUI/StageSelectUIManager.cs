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
    public Transform[] stages;      // �������� �迭
    public Button backButton;            // �ڷΰ��� ��ư
    public GameObject stageEnterPanel;        // �������� ����â UI
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

        // �ڷΰ��� ��ư�� �̺�Ʈ ����
        backButton.onClick.AddListener(TurnOffScreen);
        stageSelectBtn.onClick.AddListener(OnStageEnterBtn);
    }

    public void OnStageEnterBtn()
    {
        FindObjectOfType<UserDataManager>().selectedStageIndex = selectedStageIndex;
        SceneManager.LoadScene(selectedStageIndex);
    }

    // ���������� �������� �� ȣ�� (������ �������� ��ư�� �߾����� �̵�, ����â ����)
    public void SelectStage(int index)
    {
        if (index < 0 || index >= stages.Length) return;

        isStageEnterOpen = true;

        selectedStageIndex = index + 1;

        // ���õ� ĳ���͸� �߾����� �̵�
        stages[index].transform.DOMove(new Vector3(-1, stages[index].transform.position.y, stages[index].transform.position.z), 0.5f).SetEase(Ease.OutExpo);

        // ������ ĳ���͵��� ����ġ�� ����
        for (int i = 0; i < stages.Length; i++)
        {
            if (i != index)
            {
                int curIndex = i;  // ���ο� ������ i ���� ����
                stages[i].transform.DOScale(0, 0.3f).OnComplete(() => stages[curIndex].gameObject.SetActive(false));
            }
        }

        // ����â�� ������ ȭ�� �ۿ��� �������� �����̵��ϸ� ��Ÿ������
        stageEnterPanel.transform.DOLocalMoveX(stageEnterPanelSize, 0.5f).SetEase(Ease.OutExpo);
        stageEnterPanel.SetActive(true);

        // ����â�� ���� ���� (���� -> ����)
        stageEnterPanel.GetComponent<CanvasGroup>().DOFade(1, 0.5f);

        // �� ĳ������ ���� ��ġ�� ũ�⸦ ����
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
                stages[i].transform.DOScale(originalScale[i], 0.3f); // ũ�� ����
                stages[i].transform.DOMove(originalPositions[i], 0.5f).SetEase(Ease.OutExpo);

            }

            // ����â �����̵����� ������ ȭ�� ������ �����
            stageEnterPanel.transform.DOLocalMoveX(1000f, 0.5f).SetEase(Ease.InExpo);

            // ����â�� ���� ���� (���� -> ����)
            stageEnterPanel.GetComponent<CanvasGroup>().DOFade(0, 0.5f).OnComplete(() => stageEnterPanel.SetActive(false));
            isStageEnterOpen = false;
        }
        else
        {
            // ���õ� ĳ���ʹ� ����ġ�� ����
            for (int i = 0; i < stages.Length; i++)
            {
                stages[i].gameObject.SetActive(false);
            }

            stagePanel.gameObject.SetActive(false);
        }
    }
}
