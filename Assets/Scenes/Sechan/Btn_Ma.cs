using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Btn_Ma : MonoBehaviour
{
    public Button MyButton;
    public TMP_Text myText;
    public string newText = "hello"; 


    // Start is called before the first frame update
    void Start()
    {
        MyButton.onClick.AddListener(ChangeText);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void ChangeText()
    {
        myText.text = newText;
    }
}
