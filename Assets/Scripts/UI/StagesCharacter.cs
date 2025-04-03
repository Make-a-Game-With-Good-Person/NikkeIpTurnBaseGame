using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StagesCharacter : MonoBehaviour
{

    public Button Stage1;
    public Button Stage2;
    public Button Stage3;
    public Button Stage4;
    public Button Exit;

    public GameObject Stage_1;
    public GameObject Stage_Unknown;

  




    // Start is called before the first frame update
    void Start()
    {
        Stage_1.SetActive(false);
        Stage_Unknown.SetActive(false);

        Stage1 = GameObject.Find("Stage1").GetComponent<Button>();
        Stage2 = GameObject.Find("Stage2").GetComponent<Button>();
        Stage3 = GameObject.Find("Stage3").GetComponent<Button>();
        Stage4 = GameObject.Find("Stage4").GetComponent<Button>();

        Exit = GameObject.Find("StageSelect_Exit").GetComponent<Button>();


    }

    // Update is called once per frame
    void Update()
    {
        Stage1.onClick.AddListener(Click_Stage1);
        Stage2.onClick.AddListener(Click_Stage2);
        Stage3.onClick.AddListener(Click_Stage3);
        Stage4.onClick.AddListener(Click_Stage4);

        Exit.onClick.AddListener(Click_Exit);
    }

    void Click_Stage1()
    {
        Stage_1.SetActive(true);
        Stage_Unknown.SetActive(false);
    }
    void Click_Stage2()
    {
        Stage_1.SetActive(false);
        Stage_Unknown.SetActive(true);
    }
    void Click_Stage3()
    {
        Stage_1.SetActive(false);
        Stage_Unknown.SetActive(true);
    }
    void Click_Stage4()
    {
        Stage_1.SetActive(false);
        Stage_Unknown.SetActive(true);
    }

    void Click_Exit()
    {
        Stage_1.SetActive(false);
        Stage_Unknown.SetActive(false);
    }
}
