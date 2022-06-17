using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossFaceTrigger : MonoBehaviour
{
    public gameController _gameController;

    private void OnTriggerEnter(Collider other)
    {
        _gameController.setHitAnimation(false);

        _gameController.gameObjects.player.transform.DOLocalMove(_gameController.getFinishLineStanding(), 0.5f);
        Debug.Log("asd");
        GameObject.Find("bossLevelText").SetActive(false);
        _gameController.setAnimationTrigger(_gameController.gameObjects.boss, "playerWin");
        _gameController.gameObjects.player.transform.DOLocalRotate(new Vector3(-15, 0, 90), 0.5f);
    }
}
