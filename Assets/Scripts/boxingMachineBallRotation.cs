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
        transform.DOLocalRotate(Vector3.zero, 0.5f);
    }
}
