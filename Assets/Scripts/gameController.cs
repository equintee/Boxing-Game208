using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using System.Threading.Tasks;

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
    //public float horizantalSpeed = 0.35f;
    //public float verticalSpeed = 1f;
    public static int playerLevel = 1;


    [SerializeField]
    public GameObjects gameObjects;
    [SerializeField]
    public Speed speedParameters;

    public GameObject endingButton;

    public int bossLevel;

    [SerializeField]
    private static TextMeshProUGUI _playerLevelText;

    public Vector3 playerFinishLineStandingPointOffset;
    private Vector3 playerFinishLineStanding;
    private void Start()
    {
        _playerLevelText = gameObjects.player.transform.Find("Canvas").transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>();
        UpdateLevelText();

        bossLevel = Random.Range(1, 100);
        bossTextUpdater();

        playerFinishLineStanding = gameObjects.boss.transform.localPosition - playerFinishLineStandingPointOffset;
        playerFinishLineStanding.y = -25f;

        gamePhase++;
    }

    public static bool gameEnded = false;
    void Update()
    {
        if (gamePhase == 1) gamePhase1();
        
        
    }
    public void bossTextUpdater()
    {
        TextMeshProUGUI _bossLevelText = GameObject.Find("bossLevelText").GetComponent<TextMeshProUGUI>();
        _bossLevelText.text = "<size=100%>" + bossLevel.ToString() + "<size=60%> LEVEL";
    }
    public static void UpdateLevelText()
    {
        _playerLevelText.text = "<size=100%>" + playerLevel.ToString() + "<size=60%> LEVEL";
    }

    float _touchBoarder;
    private void gamePhase1()
    {
        Touch _touch;

        gameObjects.player.transform.position = new Vector3(gameObjects.player.transform.position.x, gameObjects.player.transform.position.y, gameObjects.player.transform.position.z + (speedParameters.vertical * Time.deltaTime));

        if (Input.touchCount > 0)
        {
            _touch = Input.GetTouch(0);
            _touchBoarder = gameObjects.player.transform.position.x + _touch.deltaPosition.x * (speedParameters.horizantal * Time.deltaTime);

            if (_touch.phase == TouchPhase.Moved)
            {
                gameObjects.player.transform.position = new Vector3(_touchBoarder, gameObjects.player.transform.position.y, gameObjects.player.transform.position.z);

            }

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

    private float deltaTime = 0;
    public bool playerWin = false;
    public async void gamePhase2()
    {
        Debug.Log("qweqwe");
        gameEnded = false;
        gameObjects.player.transform.DOLocalMove(playerFinishLineStanding, 1f);
        await Task.Delay(System.TimeSpan.FromSeconds(1f));
        endingButton.SetActive(true);

        

        if (playerLevel >= bossLevel)
        {
            Debug.Log("player win");
            playerWin = true;
        }
        else
        {
            float waitTime = Random.Range(0.1f, 1f);
            await Task.Delay(System.TimeSpan.FromSeconds(waitTime));
            endingButton.SetActive(false);
            gameObjects.boss.GetComponent<Animator>().SetBool("bossWin", true);
            await Task.Delay(System.TimeSpan.FromSeconds(1f));
            gameObjects.boss.GetComponent<Animator>().SetBool("bossWin", false);
            Camera.main.transform.parent = null;
            gameObjects.player.transform.DOLocalMoveX(75f, 1f);

        }

        gamePhase++;
    }

    public async void playWinningAnimation()
    {
        endingButton.SetActive(false);
        gameObjects.player.transform.DOMove(GameObject.Find("bossFace").transform.position, 0.3f);
        await Task.Delay(System.TimeSpan.FromSeconds(0.3f));
        gameObjects.player.transform.DOLocalMove(playerFinishLineStanding, 0.5f);
        GameObject.Find("bossLevelText").SetActive(false);
        setAnimationTrigger(gameObjects.boss, "playerWin");
    }

    private void setAnimationTrigger(GameObject gameObject, string triggerName)
    {
        gameObject.GetComponent<Animator>().SetTrigger(triggerName);
    }

    public void buttonClicked()
    {
        if (playerWin)
        {
            endingButton.SetActive(false);
            playWinningAnimation();
        }
        
    }

}   
