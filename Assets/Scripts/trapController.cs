using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class trapController : MonoBehaviour
{
    public int value;
    public int operation; //0: +, 1: -, 2: x, 3: ÷

    [SerializeField]
    private gameController _gameController;
    [SerializeField]
    private TextMeshProUGUI _text;
    public void changeText()
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

        if (!other.CompareTag("Player")) return;

        Transform parentTrap = transform.parent;
        foreach(Transform childTrap in parentTrap)
        {
            childTrap.gameObject.GetComponent<BoxCollider>().enabled = false;
        }


        int playerLevel = _gameController.playerLevel;

        switch (operation)
        {
            case 0:
                playerLevel += value;
                break;
            case 1:
                playerLevel = playerLevel - value > 0 ? playerLevel - value : 0; // Player level cannot be below 0.
                break;
            case 2:
                playerLevel *= value;
                break;
            case 3:
                playerLevel /= value;
                break;
        }
        _gameController.playerLevel = playerLevel;
        _gameController.changeSkin();
        _gameController.UpdateLevelText();
    }
}
