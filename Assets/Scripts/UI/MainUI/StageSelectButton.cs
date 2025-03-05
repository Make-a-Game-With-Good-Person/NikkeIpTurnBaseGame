using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSelectButton : MonoBehaviour
{
    public int index;
    public StageSelectUIManager stageSelectManager;

    private void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(OnStageButtonClick);
    }

    void OnStageButtonClick()
    {
        stageSelectManager.SelectStage(index);
    }
}
