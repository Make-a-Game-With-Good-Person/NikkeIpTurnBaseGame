using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityTargetUIController : MonoBehaviour
{
    public GameObject abilityTargetUI;
    public Button confirmButton;
    public Button cancelButton;

    public void Display()
    {
        abilityTargetUI.SetActive(true);
    }
    public void Hide()
    {
        abilityTargetUI.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        Hide();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
