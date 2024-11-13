using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UI_Controller : MonoBehaviour
{

    public GameObject UIMiddle;

    public GameObject BtnCharater;
    public GameObject BtnMySquad;
    public GameObject BtnRobby;
    public GameObject BtnInventory;
    public GameObject BtnRecruit;



    public GameObject BtnProfile;
    public GameObject BtnOptions;

    public GameObject PageCharater;
    public GameObject PageMySquad;
    public GameObject PageInventory;
    public GameObject PageRecruit;

    public GameObject PageProfile;
    public GameObject PageOptions;
    // 0은 로비 상태 1은 페이지가 하나라도 켜져 있는 상태
    public int PageController;

    // Start is called before the first frame update
    void Start()
    {
        PageCharater.SetActive(false);
        PageMySquad.SetActive(false);
        PageInventory.SetActive(false);
        PageRecruit.SetActive(false);
        PageOptions.SetActive(false);
        PageProfile.SetActive(false);
        PageController = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PressBtnCharcter()
    {
        if (PageController == 0)
        {
            UIMiddle.SetActive(false);
            PageMySquad.SetActive(false);
            PageInventory.SetActive(false);
            PageRecruit.SetActive(false);
            PageCharater.SetActive(true);
        }
    }

    public void PressBtnMySquad()
    {
        if (PageController == 0)
        {
            UIMiddle.SetActive(false);
            PageCharater.SetActive(false);
            PageInventory.SetActive(false);
            PageRecruit.SetActive(false);
            PageMySquad.SetActive(true);
        }
    }

    public void PressBtnInventory()
    {
        if (PageController == 0)
        {
            UIMiddle.SetActive(false);
            PageCharater.SetActive(false);
            PageMySquad.SetActive(false);
            PageRecruit.SetActive(false);
            PageInventory.SetActive(true);
        }
    }

    public void PressBtnRecruit()
    {
        if (PageController == 0)
        {
            UIMiddle.SetActive(false);
            PageCharater.SetActive(false);
            PageMySquad.SetActive(false);
            PageInventory.SetActive(false);
            PageRecruit.SetActive(true);
        }
    }
    public void PressBtnRobby()
    {
        if (PageController == 0)
        {
            PageCharater.SetActive(false);
            PageMySquad.SetActive(false);
            PageInventory.SetActive(false);
            PageRecruit.SetActive(false);
            UIMiddle.SetActive(true);
        }
    }

    public void PressBtnProfile()
    {
        if (PageController == 0)
        {
            PageProfile.SetActive(true);
            PageController = 1;
        }
    }
    public void PressBtnProfileClosed()
    {
        PageProfile.SetActive(false);
        PageController = 0;
    }

    public void PressBtnOptions()
    {
        if (PageController == 0)
        {
            PageOptions.SetActive(true);
            PageController = 1;
        }
    }
    public void PressBtnOptionsClosed()
    {
        PageOptions.SetActive(false);
        PageController = 0;
    }
}
