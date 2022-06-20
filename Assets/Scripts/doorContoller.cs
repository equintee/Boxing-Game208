using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorContoller : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        foreach(Transform parentTrap in transform)
        {
            GameObject leftTrap = parentTrap.Find("Left").gameObject;
            GameObject rightTrap = parentTrap.Find("Right").gameObject;
        }
    }

}
