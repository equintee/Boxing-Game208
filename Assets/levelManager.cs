using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


[System.Serializable]
public struct Menu
{
    public GameObject tapToStart;
    public GameObject failCanvas;
    public GameObject nextLevelCanvas;
}

public class levelManager : MonoBehaviour
{
    public static int level = 0;


    [SerializeField]
    private Menu canvasList;
    void Start()
    {
        level = SceneManager.GetActiveScene().buildIndex;
        Debug.Log("Level " + level.ToString() + " loaded.");
    }

    public void loadScene()
    {
        SceneManager.LoadScene(level);
    }

    public void incrementLevel()
    {
        level++;
        level = level % SceneManager.sceneCountInBuildSettings;
    }

    public void disableTapToStart()
    {
        canvasList.tapToStart.SetActive(false);
    }

    public void enableFailCanvas()
    {
        canvasList.failCanvas.SetActive(true);
    }

    public void disableFailCanvas()
    {
        canvasList.failCanvas.SetActive(false);
    }

    public void enableWinCanvas()
    {
        canvasList.nextLevelCanvas.SetActive(true);
    }

    
}
