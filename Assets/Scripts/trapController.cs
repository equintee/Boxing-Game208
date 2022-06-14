using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class trapController : MonoBehaviour
{
    private int value;
    public int operation; //0: +, 1: -, 2: x, 3: /

    [SerializeField]
    private TextMeshProUGUI _text;
    void Start()
    {
        value = Random.Range(1, 6);
        operation = Random.Range(0, 4);
        changeText();
    }

    private void changeText()
    {
        string doorText = "";

        switch (operation)
        {
            case 0:
                doorText += "+";
                break;
            case 1:
                doorText += "-";
                break;
            case 2:
                doorText += "X";
                break;
            case 3:
                doorText += "÷";
                break;
        }

        doorText += value.ToString();
            
        _text.text = doorText;

       
    }

}
