using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class boxingMachineFinishLineController : MonoBehaviour
{
    [SerializeField]
    private gameController _gameController;
    [SerializeField]
    private boxingMachineController _boxingMachineController;

    private async void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        gameController.gamePhase = 5;
        GameObject player = _gameController.gameObjects.player;
        player.transform.DOLocalMove(_boxingMachineController.playerBoxingMachineStandingPointOfset, 1f);
        await Task.Delay(System.TimeSpan.FromSeconds(1.3f));

        int randomHand = Random.Range(0,2);
        GameObject hand = randomHand == 0 ? GameObject.Find("Boxing_Hand_Left_Hit_Anim"): GameObject.Find("Boxing_Hand_Right_Hit_Anim"); //0:left, 1:right
        hand.GetComponent<Animator>().SetTrigger("hit");

        
        await Task.Delay(System.TimeSpan.FromSeconds(0.7f));
        _gameController.gameObjects.player.transform.DOLocalMoveY(-30.31f, 1f);
        await Task.Delay(System.TimeSpan.FromSeconds(1f));

        //TODO: AFALLA????

        gameController.gamePhase = 1;
    }
}
