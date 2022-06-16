using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraScript : MonoBehaviour
{
    public GameObject player;

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position - new Vector3(0.07276498f, 0, -0.1123781f);
    }
}
