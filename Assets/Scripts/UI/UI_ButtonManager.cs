using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ButtonManager : MonoBehaviour
{
    public GameObject ItemUI;
    public Button SquadBtn;
    public Button XBtn;

    // Start is called before the first frame update
    void Start()
    {
        ItemUI.SetActive(false);

        XBtn = GameObject.Find("X_Button").GetComponent<Button>();
        SquadBtn = GameObject.Find("SquadBtn").GetComponent<Button> ();
       
    }

    void ClickXButton()
    {
        ItemUI.SetActive(false);
    }

    void ClickSquadBtn()
    {
        ItemUI.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        XBtn.onClick.AddListener(ClickXButton);
        SquadBtn.onClick.AddListener(ClickSquadBtn);
    }
}
