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
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private async void OnTriggerEnter(Collider other)
    {
        gameController.gamePhase = 5;
        GameObject player = _gameController.gameObjects.player;
        player.transform.DOLocalMove(_boxingMachineController.playerBoxingMachineStandingPointOfset, 1f);
        await Task.Delay(System.TimeSpan.FromSeconds(1.3f));

        int randomHand = 0;
        //TODO: Random hand pick
        GameObject hand = GameObject.Find("Boxing_Hand_Left_Hit_Anim");
        hand.GetComponent<Animator>().SetTrigger("hit");


    }
}
