using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using TMPro;
public class boxingMachineController : MonoBehaviour
{
    [SerializeField]
    private gameController _gameController;
    public Vector3 hitOffset;
    [HideInInspector]
    public Vector3 playerBoxingMachineStandingPointOfset;
    
    public int minValue;
    public int maxValue;
    public int value;

    [SerializeField]
    private TextMeshProUGUI _boxingMachineValueText;

    public ParticleSystem hitParticle;
    void Start()
    {
        value = Random.Range(minValue, maxValue + 1);
        playerBoxingMachineStandingPointOfset = calculatePlayerBoxingMachineStandingPoint();
        Debug.Log(playerBoxingMachineStandingPointOfset.ToString());

        _boxingMachineValueText.text = value.ToString();
    }


    private Vector3 calculatePlayerBoxingMachineStandingPoint()
    {
        return transform.localPosition - hitOffset;
    }

}
