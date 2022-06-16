using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    [SerializeField]
    private gameController _gameController;
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("boss") && _gameController.playerWin == false)
        {
            other.transform.parent.GetComponent<Animator>().SetTrigger("bossWin");
        }
    }
}
