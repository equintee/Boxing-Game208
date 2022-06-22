using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorContoller : MonoBehaviour
{
    [System.Serializable]
    public struct Boundries
    {
        public int minAddition;
        public int maxAddition;
        public int minMultiplication;
        public int maxMultiplication;
    }

    [SerializeField]
    public List<Boundries> _boundries = new List<Boundries>();

    [SerializeField]
    public Material negativeMaterial;
    void Start()
    {
        int i = 0;
        foreach(Transform parentTrap in transform)
        {
            Boundries doorBoundries = _boundries[i];
            GameObject leftTrap = parentTrap.Find("Left").gameObject;
            GameObject rightTrap = parentTrap.Find("Right").gameObject;

            int randomOperation = Random.Range(0, 4);

            leftTrap.GetComponent<trapController>().operation = randomOperation;
            leftTrap.GetComponent<trapController>().value = generateRandomValue(randomOperation, doorBoundries);

            if(randomOperation % 2 == 0)
            {
                randomOperation = Random.Range(0,2) == 1 ? 3 : 1;
                rightTrap.GetComponent<trapController>().operation = randomOperation;
                rightTrap.GetComponent<trapController>().value = generateRandomValue(randomOperation, doorBoundries);
            }
            else
            {
                int temp = Random.Range(0, 2);
                randomOperation = temp == 0 ? 0 : 2;
                rightTrap.GetComponent<trapController>().operation = randomOperation;
                rightTrap.GetComponent<trapController>().value = generateRandomValue(randomOperation, doorBoundries);
            }


            GameObject negativeTrap = randomOperation % 2 == 1 ? rightTrap : leftTrap;

            Material[] negativeMaterials = negativeTrap.GetComponent<MeshRenderer>().materials;
            negativeMaterials[0] = negativeMaterial;
            negativeTrap.GetComponent<MeshRenderer>().materials = negativeMaterials;


            leftTrap.GetComponent<trapController>().changeText();
            rightTrap.GetComponent<trapController>().changeText();

            i++;
        }
    }


    public int generateRandomValue(int operation, Boundries boundries)
    {
        return operation > 1 ? generateMultiplicationDivisonValue(boundries.minMultiplication, boundries.maxMultiplication) : generateAdditionSubstractionValue(boundries.minAddition, boundries.maxAddition);
    }
    private int generateMultiplicationDivisonValue(int min, int max)
    {
        return Random.Range(min, max + 1);
    }

    private int generateAdditionSubstractionValue(int min, int max)
    {
        return Random.Range(min, max + 1);
    }

}
