using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraScript : MonoBehaviour
{
    public GameObject target;
    public Vector3 distance;
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, target.transform.position + distance, Time.deltaTime);
    }
}
