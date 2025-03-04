using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;


public class Skill_Explain : MonoBehaviour
{
    public GameObject Skill_Ex;
    public TextMeshProUGUI InGame_Skill_Explain;
  

    public string Emma_skill;
    public string Emma_Passive;
    public string Enwha_skill;
    public string Enwha_Passive;
    public string Vesti_skill;
    public string Vesti_Passive;
  
    
    // Start is called before the first frame update
    void Start()
    {

        Skill_Ex.SetActive(false);
        Emma_skill = "넓은 범위로 적의 시야를 가립니다.";
        Emma_Passive = "스테이지 시작 시, 본인에게 쉴드를 부여합니다.";
        Enwha_skill = "적에게 강력한 탄환을 발포합니다.";
        Enwha_Passive = "적에게 감지되지 않으면, 데미지가 증가합니다.";
        Vesti_skill = "적에게 수류탄을 던져 넓은 범위의 엄폐물과 적에게 데미지를 줍니다.";
        Vesti_Passive = "공격 턴을 소비하여 한번 더 이동 할 수 있습니다.";


   
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("a"))
        {

            InGame_Skill_Explain.text = Emma_skill;
          
          
        }


    }

    public void Show_Emma_Passive_Explain()
    {
        Skill_Ex.SetActive(true);
        InGame_Skill_Explain.text = Emma_Passive;
    }

    public void Show_Emma_Skill_Explain()
    {
        Skill_Ex.SetActive(true);
        InGame_Skill_Explain.text = Emma_skill;
    }

    public void Show_Enwha_Passive_Explain()
    {
        Skill_Ex.SetActive(true);
        InGame_Skill_Explain.text = Enwha_Passive;
    }

    public void Show_Enwha_Skill_Explain()
    {
        Skill_Ex.SetActive(true);
        InGame_Skill_Explain.text = Enwha_skill;
    }
    public void Show_Vesti_Passive_Explain()
    {
        Skill_Ex.SetActive(true);
        InGame_Skill_Explain.text = Vesti_Passive;
    }

    public void Show_Vesti_Skill_Explain()
    {
        Skill_Ex.SetActive(true);
        InGame_Skill_Explain.text = Vesti_skill;
    }
    public void ExitExplain()
    {
        Skill_Ex.SetActive(false);
    }
  

}
