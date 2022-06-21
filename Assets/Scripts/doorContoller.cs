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

            int randomOperation = Random.Range(0, 4);

            leftTrap.GetComponent<trapController>().operation = randomOperation;
            leftTrap.GetComponent<trapController>().value = generateRandomValue(randomOperation);

            if(randomOperation % 2 == 0)
            {
                randomOperation = Random.Range(0,2) == 1 ? 3 : 1;
                rightTrap.GetComponent<trapController>().operation = randomOperation;
                rightTrap.GetComponent<trapController>().value = generateRandomValue(randomOperation);
            }
            else
            {
                int temp = Random.Range(0, 2);
                randomOperation = temp == 0 ? 0 : 2;
                rightTrap.GetComponent<trapController>().operation = randomOperation;
                rightTrap.GetComponent<trapController>().value = generateRandomValue(randomOperation);
            }

            leftTrap.GetComponent<trapController>().changeText();
            rightTrap.GetComponent<trapController>().changeText();
        }
    }


    public int generateRandomValue(int operation)
    {
        return operation > 1 ? generateMultiplicationDivisonValue() : generateAdditionSubstractionValue();
    }
    private int generateMultiplicationDivisonValue()
    {
        return Random.Range(1, 11);
    }

    private int generateAdditionSubstractionValue()
    {
        return Random.Range(5, 21);
    }

}
