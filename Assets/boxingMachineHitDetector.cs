using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class boxingMachineHitDetector : MonoBehaviour
{
    [SerializeField]
    private gameController _gameController;

    public float bounceRate = 5f;
    public float bounceTime = 1f;

    private async void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        Debug.Log("enter");
        float horizantalSpeed = _gameController.speedParameters.horizantal;
        float verticalSpeed = _gameController.speedParameters.vertical;
        _gameController.speedParameters.horizantal = 0;
        _gameController.speedParameters.vertical = 0;

        _gameController.gameObjects.player.transform.DOLocalMoveZ(_gameController.gameObjects.player.transform.localPosition.z - bounceRate, bounceTime);

        await Task.Delay(System.TimeSpan.FromSeconds(bounceTime));

        _gameController.speedParameters.horizantal = horizantalSpeed;
        _gameController.speedParameters.vertical = verticalSpeed;

    }

   
}
