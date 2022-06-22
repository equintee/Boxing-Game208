using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;

public class boxingMachineBallRotation : MonoBehaviour
{
    [SerializeField]
    private boxingMachineController _boxingMachineController;

    public float ballClosingTime;
    public float shakeTime;
    public float shakeAngle;


    private async void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("playerHand")) return;
        GetComponent<BoxCollider>().enabled = false;
        _boxingMachineController.hitParticle.Play();
        Invoke("disableBoxingMachine", 3 * shakeTime);

        if(gameController.playerLevel >= _boxingMachineController.value)
        {
            transform.DOLocalRotate(new Vector3(90,0,0), ballClosingTime);
            Camera.main.transform.DOLocalRotate(new Vector3(-7.835f, -180, shakeAngle), shakeTime);
            await Task.Delay(System.TimeSpan.FromSeconds(shakeTime));
            Camera.main.transform.DOLocalRotate(new Vector3(-7.835f, -180, -1 * shakeAngle), shakeTime);
            await Task.Delay(System.TimeSpan.FromSeconds(shakeTime));
            Camera.main.transform.DOLocalRotate(new Vector3(-7.835f, -180, 0), shakeTime);

            int coin = PlayerPrefs.GetInt("coin");
            coin += _boxingMachineController.value;
            PlayerPrefs.SetInt("coin", coin);
        }
        else
        {
            other.transform.parent.gameObject.GetComponent<Animator>().SetTrigger("damage");
            
        }

    }


    private void disableBoxingMachine()
    {
        transform.parent.gameObject.SetActive(false);
    }
}
