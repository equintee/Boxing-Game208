﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using System.Threading.Tasks;
using PathCreation;

[System.Serializable]
public struct GameObjects
{
    public GameObject player;
    public GameObject boss;
}

[System.Serializable]
public struct Speed
{
    public float horizantal; //0.35f
    public float vertical; //20f
}



public class gameController : MonoBehaviour
{
    public static int gamePhase = 0; //0 game initialization, 1 gameplay, 2 boss fight
    public static int playerLevel = 1;


    [SerializeField]
    public GameObjects gameObjects;
    [SerializeField]
    public Speed speedParameters;

    public GameObject endingButton;

    public int bossLevel;

    [SerializeField]
    private static TextMeshProUGUI _playerLevelText;

    //Player standing offset from Boss
    public Vector3 playerFinishLineStandingPointOffset;
    private Vector3 playerFinishLineStanding;

    public PathCreator pathCreator;
    private void Start()
    {
        _playerLevelText = gameObjects.player.transform.Find("Canvas").transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>();
        UpdateLevelText();

        bossLevel = Random.Range(1, 100);
        bossTextUpdater();

        playerFinishLineStanding = gameObjects.boss.transform.localPosition - playerFinishLineStandingPointOffset;

        gamePhase++;
    }

    
    private float distanceTraveled;
    public float hitAnimationSpeed = 5;
    private bool hitAnimation = false;
    void Update()
    {
        if (gamePhase == 1) gamePhase1();
        if (playerWin && hitAnimation)
        {
            Camera.main.transform.parent = null;
            distanceTraveled += hitAnimationSpeed * Time.deltaTime;
            gameObjects.player.transform.position = pathCreator.path.GetPointAtDistance(distanceTraveled, EndOfPathInstruction.Stop);
            gameObjects.player.transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTraveled, EndOfPathInstruction.Stop);
            
        }
        
    }
    public void bossTextUpdater() //Updates Boss level text. Called only once in Start().
    {
        TextMeshProUGUI _bossLevelText = GameObject.Find("bossLevelText").GetComponent<TextMeshProUGUI>();
        _bossLevelText.text = "<size=100%>" + bossLevel.ToString() + "<size=60%> LEVEL";
    }
    public static void UpdateLevelText() //Updates Player level text.
    {
        _playerLevelText.text = "<size=100%>" + playerLevel.ToString() + "<size=60%> LEVEL";
    }

    private float _touchBoarder;
    private void gamePhase1() //Gameplay input handeler.
    {
        Touch _touch;

        gameObjects.player.transform.position = new Vector3(gameObjects.player.transform.position.x, gameObjects.player.transform.position.y, gameObjects.player.transform.position.z + (speedParameters.vertical * Time.deltaTime));

        if (Input.touchCount > 0)
        {
            _touch = Input.GetTouch(0);
            _touchBoarder = gameObjects.player.transform.position.x + _touch.deltaPosition.x * (speedParameters.horizantal * Time.deltaTime); //Moving piece verticly in a desired speed constantly.

            if (_touch.phase == TouchPhase.Moved)
            {
                gameObjects.player.transform.position = new Vector3(_touchBoarder, gameObjects.player.transform.position.y, gameObjects.player.transform.position.z); //Handling horizantal movement using inputs from user.

            }

            //Prevents Player to move outside of platform.
            if (gameObjects.player.transform.position.x < -4.5f)
            {
                gameObjects.player.transform.position = new Vector3(-4.5f, gameObjects.player.transform.position.y, gameObjects.player.transform.position.z);
            }
            if (gameObjects.player.transform.position.x > 4.5f)
            {
                gameObjects.player.transform.position = new Vector3(4.5f, gameObjects.player.transform.position.y, gameObjects.player.transform.position.z);
            }
        }
    }

    [HideInInspector]
    public bool playerWin = false;
    public async void gamePhase2() //Boss fight phase.
    {
        //Move Player to ring and wait 1 second to make calculations.
        gameObjects.player.transform.DOLocalMove(playerFinishLineStanding, 1f);
        await Task.Delay(System.TimeSpan.FromSeconds(1f));
        endingButton.SetActive(true);

        

        if (playerLevel >= bossLevel) playerWin = true; //Player wins if has higher level than Boss.
        else
        {
            //Boss waits than hits the Player.
            float waitTime = Random.Range(0.1f, 1f);
            await Task.Delay(System.TimeSpan.FromSeconds(waitTime));
            endingButton.SetActive(false);
            gameObjects.boss.GetComponent<Animator>().SetBool("bossWin", true);
            await Task.Delay(System.TimeSpan.FromSeconds(1f));
            gameObjects.boss.GetComponent<Animator>().SetBool("bossWin", false);
            Camera.main.transform.parent = null; //Since camera is a child of Player object initally, I had to detach it from Player otherwise camera moves along with the player.
            gameObjects.player.transform.DOLocalMoveX(75f, 1f); //Animation that plays when boss hits the player.

        }

        gamePhase++;
    }

    public void playWinningAnimation()
    {
        endingButton.SetActive(false);
        hitAnimation = true;
    }

    public void setAnimationTrigger(GameObject gameObject, string triggerName)
    {
        gameObject.GetComponent<Animator>().SetTrigger(triggerName);
    }

    public void setHitAnimation(bool flag)
    {
        hitAnimation = flag;
    }

    public Vector3 getFinishLineStanding()
    {
        return playerFinishLineStanding;
    }
}   
