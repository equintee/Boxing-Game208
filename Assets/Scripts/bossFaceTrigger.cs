using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class bossFaceTrigger : MonoBehaviour
{
    public gameController _gameController;

    [SerializeField]
    private GameObject _bossLevelCanvas;

    [SerializeField]
    private GameObject _endingScreen;
    private void OnTriggerEnter(Collider other)
    {
        _gameController.bossHealth--;

        if(_gameController.bossHealth == 0)
        {
            _gameController.setAnimationTrigger(_gameController.gameObjects.boss, "playerWin");
            _bossLevelCanvas.SetActive(false);
            _bossLevelCanvas.SetActive(false);
        }
    }
}
