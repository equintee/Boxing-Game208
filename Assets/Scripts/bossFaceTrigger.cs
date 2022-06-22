using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Threading.Tasks;

public class bossFaceTrigger : MonoBehaviour
{
    public gameController _gameController;

    [SerializeField]
    private GameObject _bossLevelCanvas;

    [SerializeField]
    private GameObject _endingScreen;

    [SerializeField]
    private levelManager _levelManager;

    private async void OnTriggerEnter(Collider other)
    {
        _gameController.bossHealth--;

        if(_gameController.bossHealth == 0)
        {
            _gameController.setAnimationTrigger(_gameController.gameObjects.boss, "playerWin");
            _bossLevelCanvas.SetActive(false);
            _bossLevelCanvas.SetActive(false);

            _levelManager.incrementLevel();
            _endingScreen.SetActive(false);

            await Task.Delay(System.TimeSpan.FromSeconds(1f));

            _levelManager.enableWinCanvas();

        }
        else
        {
            transform.parent.GetComponent<Animator>().SetTrigger("bossDamage");
        }
    }


}
