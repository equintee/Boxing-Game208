using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class trapController : MonoBehaviour
{
    public int value;
    public int operation; //0: +, 1: -, 2: x, 3: ÷
    private bool isTriggered = false;

    [SerializeField]
    private TextMeshProUGUI _text;

    private static gameController _playerController;
   
    void Start()
    {
        value = Random.Range(1, 20);
        operation = Random.Range(0, 4);
        changeText();

        _playerController = GameObject.FindGameObjectWithTag("eventSystem").GetComponent<gameController>();
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



    private void OnTriggerEnter(Collider other)
    {
        
        int playerLevel = gameController.playerLevel;

        switch (operation)
        {
            case 0:
                playerLevel += value;
                break;
            case 1:
                playerLevel = playerLevel - value > 0 ? playerLevel - value : 0;
                break;
            case 2:
                playerLevel *= value;
                break;
            case 3:
                playerLevel /= value;
                break;
        }
        gameController.playerLevel = playerLevel;
        gameController.UpdateLevelText();
    }
}
