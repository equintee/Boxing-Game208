using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class boxingMachineHitDetector : MonoBehaviour
{
    [SerializeField]
    private gameController _gameController;

    private async void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        Debug.Log("enter");
        float horizantalSpeed = _gameController.speedParameters.horizantal;
        float verticalSpeed = _gameController.speedParameters.vertical;
        _gameController.speedParameters.horizantal = 0;
        _gameController.speedParameters.vertical = 0;

        _gameController.gameObjects.player.transform.DOLocalMoveZ(_gameController.gameObjects.player.transform.localPosition.z - _gameController._bounceParameters.bounceRate, _gameController._bounceParameters.bounceTime);

        await Task.Delay(System.TimeSpan.FromSeconds(_gameController._bounceParameters.bounceTime));

        _gameController.speedParameters.horizantal = horizantalSpeed;
        _gameController.speedParameters.vertical = verticalSpeed;

    }

   
}
