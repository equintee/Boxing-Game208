using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class finishLineScript : MonoBehaviour
{
    [SerializeField]
    private gameController _gameController;
    private void OnTriggerEnter(Collider other)
    {
        gameController.gameEnded = true;
        gameController.gamePhase++;
        _gameController.gamePhase2();
        //GameObject.FindObjectsOfType<gameController>()[0].gamePhase2();
    }
}
