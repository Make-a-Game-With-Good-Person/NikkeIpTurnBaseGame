using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UI_ButtonManager : MonoBehaviour
{
    #region 메인UI
    [Header("Main_UI")]
    public GameObject ItemUI;
    public GameObject MainStoryUI;
    public Button SquadBtn;
    public Button MainStoryBtn;
    public Button StageSelectXBtn;
    public Button XBtn;
    #endregion

    #region Squad 캐릭터 선택 UI
    [Space(10f)]
    [Header("CharacterUI")]
    public Button Btn_CH1;
    public Button Btn_CH2;
    public Button Btn_CH3;
  
    [Space(10f)]
    [Header("세부 캐릭터")]
    public GameObject Sub_Parents;
    public GameObject Sub_CH1;
    public GameObject Sub_CH2;
    public GameObject Sub_CH3;
    public Button Sub_GoBack;
    #endregion

    #region Status

    [Space(10f)]
    [Header("상태창")]
    public GameObject CH_Status;
    public Button Status_Itembtn;
    public Button Status_Skillbtn;
    //아이템 필드
    public GameObject Item_Field;
    //스킬 필드
    public GameObject Skill_Field;
    [Space(10f)]
    [Header("아이템 강화")]
    public Button Item_Weapon;
    public Button Item_Armor1;
    public Button Item_Armor2;
    public Button Item_Armor3;

    public GameObject Items_Explain;

    public Button Items_Reinforce_X;

    #endregion
    // Start is called before the first frame update
    void Start()
    {
        ItemUI.SetActive(false);   
        MainStoryUI.SetActive(false);
        Sub_Parents.SetActive(false); 
        CH_Status.SetActive(false);
        Item_Field.SetActive(false);
        Skill_Field.SetActive(false);
        Items_Explain.SetActive(false);
               
        XBtn = GameObject.Find("X_Button").GetComponent<Button>();
        StageSelectXBtn = GameObject.Find("StageSelect_X").GetComponent<Button>();
        SquadBtn = GameObject.Find("SquadBtn").GetComponent<Button>();
        MainStoryBtn = GameObject.Find("MainStoryBtn").GetComponent<Button>();

        Btn_CH1 = GameObject.Find("CH_1").GetComponent<Button>();
        Btn_CH2 = GameObject.Find("CH_2").GetComponent<Button>();
        Btn_CH3 = GameObject.Find("CH_3").GetComponent<Button>();

        Sub_GoBack = GameObject.Find("Sub_GoBack").GetComponent<Button>();

        Status_Itembtn = GameObject.Find("Status_Items").GetComponent<Button>();
        Status_Skillbtn = GameObject.Find("Status_Skills").GetComponent<Button>();

        Item_Weapon = GameObject.Find("Weapons").GetComponent<Button>();
        Item_Armor1 = GameObject.Find("Armor1").GetComponent<Button>();
        Item_Armor2 = GameObject.Find("Armor2").GetComponent<Button>();
        Item_Armor3 = GameObject.Find("Armor3").GetComponent<Button>();

        Items_Reinforce_X = GameObject.Find("Items_Explain_x").GetComponent<Button>();

    }

    void ClickXButton()
    {
        ItemUI.SetActive(false);
    }
    void ClickStageSelectXButton()
    {
        MainStoryUI.SetActive(false);
    }

    void ClickSquadBtn()
    {
        ItemUI.SetActive(true);
    }
    
    void ClickMainStoryBtn()
    {
        MainStoryUI.SetActive(true); 
        
    }


    #region 캐릭터 선택 시
    //캐릭터 클릭 시
    void ClickCH1()
    {
        Sub_Parents.SetActive(true);
        CH_Status.SetActive(true);
        Item_Field.SetActive(true);

        Sub_CH2.SetActive(false);
        Sub_CH3.SetActive(false);
        Sub_CH1.SetActive(true);

        XBtn.gameObject.SetActive(false);
    }

    void ClickCH2()
    {
        Sub_Parents.SetActive(true);
        CH_Status.SetActive(true);
        Item_Field.SetActive(true);

        Sub_CH3.SetActive(false);
        Sub_CH1.SetActive(false);
        Sub_CH2.SetActive(true);

        XBtn.gameObject.SetActive(false);
    }

    void ClickCH3()
    {
        Sub_Parents.SetActive(true);
        CH_Status.SetActive(true);
        Item_Field.SetActive(true);

        Sub_CH1.SetActive(false);
        Sub_CH2.SetActive(false);
        Sub_CH3.SetActive(true);

        XBtn.gameObject.SetActive(false);
    }
    //자세한 캐릭터 창에서 뒤로가기
    void ClickSub_GoBack()
    {
        Sub_Parents.SetActive(false);
        CH_Status.SetActive(false);
        Sub_CH1.SetActive(false);
        Sub_CH2.SetActive(false);
        Sub_CH3.SetActive(false);
        XBtn.gameObject .SetActive(true);
    }
    #endregion

    #region 캐릭터 상태 창

    //스테이터스 버튼
    void ClickStatus_Items()
    {
        Item_Field.SetActive(true);
        Skill_Field.SetActive(false);
    }

    void ClickStatus_Skills()
    {
        Item_Field.SetActive(false);
        Skill_Field.SetActive(true);
    }
    #endregion

    #region 아이템 강화
    //아이템 클릭
    void ClickWeapon()
    {
        Items_Explain.SetActive(true);
    }

    void ClickArmor1()
    {
        Items_Explain.SetActive(true);
    }
    void ClickArmor2()
    {
        Items_Explain.SetActive(true);
    }
    void ClickArmor3()
    {
        Items_Explain.SetActive(true);
    }
    void Click_Items_Explain_x()
    {
        Items_Explain.SetActive(false);
    }
    #endregion


    // Update is called once per frame
    void Update()
    {
        XBtn.onClick.AddListener(ClickXButton);
        StageSelectXBtn.onClick.AddListener(ClickStageSelectXButton);
        SquadBtn.onClick.AddListener(ClickSquadBtn);
        MainStoryBtn.onClick.AddListener(ClickMainStoryBtn);

        Btn_CH1.onClick.AddListener(ClickCH1);
        Btn_CH2.onClick.AddListener(ClickCH2);
        Btn_CH3.onClick.AddListener(ClickCH3);

        Sub_GoBack.onClick.AddListener(ClickSub_GoBack);

        Status_Itembtn.onClick.AddListener(ClickStatus_Items);
        Status_Skillbtn.onClick.AddListener(ClickStatus_Skills);

        Item_Weapon.onClick.AddListener(ClickWeapon);
        Item_Armor1.onClick.AddListener(ClickArmor1);
        Item_Armor2.onClick.AddListener(ClickArmor2);
        Item_Armor3.onClick.AddListener(ClickArmor3);

        Items_Reinforce_X.onClick.AddListener(Click_Items_Explain_x);
    }

    


}
