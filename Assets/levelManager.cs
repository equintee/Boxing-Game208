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
        Debug.Log("Level " + level.ToString() + " loaded.");
        if (PlayerPrefs.HasKey("level"))
        {
            level = PlayerPrefs.GetInt("level");
        }
        else
        {
            PlayerPrefs.SetInt("level", 0);
        }
    }

    public void loadScene()
    {
        SceneManager.LoadScene(level);
    }

    public void incrementLevel()
    {
        level++;
        level = level % SceneManager.sceneCountInBuildSettings;
        PlayerPrefs.SetInt("level", level);
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
