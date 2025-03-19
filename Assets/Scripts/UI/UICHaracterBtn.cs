using DG.Tweening.Core.Easing;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICHaracterBtn : MonoBehaviour
{
    [Header("캐릭터")]
    public GameObject CH_Main_Field;

    [Space(10f)]
    [Header("상태 버튼")]
    public Button Status_LevelUp_Btn;
    public Button Status_Items;
    public Button Status_Skills;

    [Space(10f)]
    [Header("장비")]

    public Button Item_Weapon;
    public Button Item_Armor1;
    public Button Item_Armor2;
    public Button Item_Armor3;


    [Space(10f)]
    [Header("하단")]
    public GameObject Field_SKills;
    public GameObject Field_Equips;

    [Space(10f)]
    public GameObject BG_Items_explain;
    public GameObject BG_CH_LevelUP;

    public Button X_Items_explain;
    public Button X_LevelUP;

    [Space(10f)]
    [Header("버튼 선택 유무")]
    public Sprite Skill_Unselected;
    public Sprite Skill_Selected;
    public Sprite Weapon_Unselected;
    public Sprite Weapon_Selected;



    // Start is called before the first frame update
    void Start()
    {
        BG_Items_explain.SetActive(false);
        BG_CH_LevelUP.SetActive(false);
        Field_SKills.SetActive(false);

        Status_LevelUp_Btn = GameObject.Find("Status_LevelUp_Btn").GetComponent<Button>();
        Status_Items = GameObject.Find("Status_Items").GetComponent<Button>();
        Status_Skills = GameObject.Find("Status_Skills").GetComponent<Button>();

        Item_Weapon = GameObject.Find("Weapons").GetComponent<Button>();
        Item_Armor1 = GameObject.Find("Armor1").GetComponent<Button>();
        Item_Armor2 = GameObject.Find("Armor2").GetComponent<Button>();
        Item_Armor3 = GameObject.Find("Armor3").GetComponent<Button>();

        X_Items_explain = GameObject.Find("Items_Explain_X").GetComponent<Button>();
        X_LevelUP = GameObject.Find("LevelUP_X").GetComponent<Button>();

    }


    void Click_X_items()
    {
        BG_Items_explain.SetActive(false);
        CH_Main_Field.SetActive(true);
    }

    void Click_X_Levelup()
    {
        BG_CH_LevelUP.SetActive(false);
        CH_Main_Field.SetActive(true);
    }

    void Click_LevelUP_Btn()
    {
        BG_CH_LevelUP.SetActive(true);
        CH_Main_Field.SetActive(false);
    }
    void Click_Skills_Btn()
    {
        Field_SKills.SetActive(true);
        GameObject.Find("Status_Items").GetComponent<Image>().sprite = Weapon_Unselected;
        GameObject.Find("Status_Skills").GetComponent<Image>().sprite = Skill_Selected;
        Field_Equips.SetActive(false);

    }

    void Click_Equip_Btn()
    {
        Field_Equips.SetActive(true);
        GameObject.Find("Status_Items").GetComponent<Image>().sprite = Weapon_Selected;
        GameObject.Find("Status_Skills").GetComponent<Image>().sprite = Skill_Unselected;
        Field_SKills.SetActive(false);
    }

    void Click_Item_Weapon()
    {
        CH_Main_Field.SetActive(false);
        BG_Items_explain.SetActive(true);

    }
    void Click_Item_Armor1()
    {
        CH_Main_Field.SetActive(false);
        BG_Items_explain.SetActive(true);
    }
    void Click_Item_Armor2()
    {
        CH_Main_Field.SetActive(false);
        BG_Items_explain.SetActive(true);
    }
    void Click_Item_Armor3()
    {
        CH_Main_Field.SetActive(false);
        BG_Items_explain.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        X_Items_explain.onClick.AddListener(Click_X_items);
        X_LevelUP.onClick.AddListener(Click_X_Levelup);

        Status_LevelUp_Btn.onClick.AddListener(Click_LevelUP_Btn);

        Status_Skills.onClick.AddListener(Click_Skills_Btn);
        Status_Items.onClick.AddListener(Click_Equip_Btn);

        Item_Weapon.onClick.AddListener(Click_Item_Weapon);
        Item_Armor1.onClick.AddListener(Click_Item_Armor1);
        Item_Armor2.onClick.AddListener(Click_Item_Armor2);
        Item_Armor3.onClick.AddListener(Click_Item_Armor3);


    }



}
