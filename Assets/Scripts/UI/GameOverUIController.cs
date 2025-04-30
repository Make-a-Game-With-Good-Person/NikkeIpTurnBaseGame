using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUIController : MonoBehaviour
{
    public GameObject gameOverUI;
    public TMPro.TextMeshProUGUI text;
    public Button backToMainButton;
    private int gameEndType;

    //이겼을때 1, 졌을때 2
    public void GameEnd(int type)
    {
        gameEndType = type;
        gameOverUI.SetActive(true);
        switch (type)
        {
            case 1:
                text.text = "Win!";
                break;
            case 2:
                text.text = "Lose...";
                break;
            default:
                Debug.LogError("not proper game end type");
                break;
        }
    }

    public void OnBackToMainButton()
    {
        if(FindObjectOfType<UserDataManager>().selectedStageIndex == 2 && gameEndType == 1)
        {
            SceneManager.LoadScene(3);
            return;
        }
        SceneManager.LoadScene(0);
    }

    private void Start()
    {
        backToMainButton.onClick.AddListener(OnBackToMainButton);
    }
}
