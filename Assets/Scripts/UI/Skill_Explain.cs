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
        Emma_skill = "���� ������ ���� �þ߸� �����ϴ�.";
        Emma_Passive = "�������� ���� ��, ���ο��� ���带 �ο��մϴ�.";
        Enwha_skill = "������ ������ źȯ�� �����մϴ�.";
        Enwha_Passive = "������ �������� ������, �������� �����մϴ�.";
        Vesti_skill = "������ ����ź�� ���� ���� ������ ���󹰰� ������ �������� �ݴϴ�.";
        Vesti_Passive = "���� ���� �Һ��Ͽ� �ѹ� �� �̵� �� �� �ֽ��ϴ�.";


   
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
