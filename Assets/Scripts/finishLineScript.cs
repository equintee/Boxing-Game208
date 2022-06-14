using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class finishLineScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        gameController.gamePhase++;
    }
}
