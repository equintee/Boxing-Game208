using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class boxingMachineBallRotation : MonoBehaviour
{
    [SerializeField]
    private boxingMachineController _boxingMachineController;
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("playerHand")) return;
        transform.DOLocalRotate(Vector3.zero, 0.5f);
        GetComponent<BoxCollider>().enabled = false;

        if(gameController.playerLevel >= _boxingMachineController.value)
        {
            int coin = PlayerPrefs.GetInt("coin");
            coin += _boxingMachineController.value;
            PlayerPrefs.SetInt("coin", coin);
        }

    }
}
