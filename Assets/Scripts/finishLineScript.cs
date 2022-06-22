using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class finishLineScript : MonoBehaviour
{
    [SerializeField]
    private gameController _gameController;
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        _gameController.gamePhase++;
        _gameController.gamePhase2();
    }
}
