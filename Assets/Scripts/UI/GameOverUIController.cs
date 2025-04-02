using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUIController : MonoBehaviour
{
    public GameObject gameOverUI;
    public TMPro.TextMeshProUGUI text;
    public Button backToMainButton;

    //이겼을때 1, 졌을때 2
    public void GameEnd(int type)
    {
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
}
