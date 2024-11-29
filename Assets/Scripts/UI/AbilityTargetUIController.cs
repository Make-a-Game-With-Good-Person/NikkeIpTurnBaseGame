using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AbilityTargetUIController : MonoBehaviour
{
    [Serializable]
    public class AbilityButtonList
    {
        public List<Button> buttonList;
    }


    public GameObject abilityTargetUI;
    public Button confirmButton;
    public Button cancelButton;
    public List<GameObject> charList = new List<GameObject>();
    public List<AbilityButtonList> skillButtonList = new List<AbilityButtonList>();
    List<GameObject> skillList;

    public void Display()
    {
        abilityTargetUI.SetActive(true);
    }
    public void Hide()
    {
        abilityTargetUI.SetActive(false);
    }

    public void UISetting(Unit unit)
    {
        int idx = unit.index % 10;

        charList[idx].SetActive(true);
        for(int i = 0; i < unit.unitSkills.Count; i++)
        {
            skillButtonList[idx].buttonList[i].GetComponent<Button>().interactable = true;
        }
    }

    public void ResetSkillUI(Unit unit)
    {
        int idx = unit.index % 10;

        charList[idx].SetActive(false);
        for (int i = 0; i < unit.unitSkills.Count; i++)
        {
            skillButtonList[idx].buttonList[i].GetComponent<Button>().interactable = false;
        }
    }

    

    // Start is called before the first frame update
    void Start()
    {
        Hide();
    }
}
