using System.Collections;
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

[System.Serializable]
public struct bounceParameters
{
    public float bounceRate;
    public float bounceTime;
}
[System.Serializable]
public struct bossLevelParameters
{
    public int bossLevelMin;
    public int bossLevelMax;
}

[System.Serializable]
public struct levelMaterials
{
    public Material handMaterial;
    public Material wristMaterial;
    public int minLevel;
}

public class gameController : MonoBehaviour
{
    public int gamePhase = 0; //0 game initialization, 1 gameplay, 2 boss fight
    public int coin;
    public int playerLevel = 1;
    [SerializeField]
    public GameObjects gameObjects;
    [SerializeField]
    public Speed speedParameters;
    [SerializeField]
    public bounceParameters _bounceParameters;
    [SerializeField]
    public bossLevelParameters _bossLevelParameters;
    [SerializeField]
    private levelManager _levelManager;

    [SerializeField]
    private List<levelMaterials> _levelMaterials;
    
    public GameObject endingButton;


    [HideInInspector]
    public int bossLevel;

    public int bossHealth = 3;
    [SerializeField]
    private static TextMeshProUGUI _playerLevelText;

    //Player standing offset from Boss
    public Vector3 playerFinishLineStandingPointOffset;
    private Vector3 playerFinishLineStanding;

    private void Start()
    {
        _playerLevelText = gameObjects.player.transform.Find("Canvas").transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>();
        UpdateLevelText();

        bossLevel = Random.Range(_bossLevelParameters.bossLevelMin, _bossLevelParameters.bossLevelMax + 1);
        bossTextUpdater();

        playerFinishLineStanding = gameObjects.boss.transform.localPosition - playerFinishLineStandingPointOffset;

        if (PlayerPrefs.HasKey("coin"))
        {
            coin = PlayerPrefs.GetInt("coin");
        }
        else
        {
            PlayerPrefs.SetInt("coin", 0);
        }
        Debug.Log("Player has " + coin.ToString() + " coins.");

        levelMaterials _temp = _levelMaterials[0];
        _temp.minLevel = 1;
        _levelMaterials[0] = _temp;

        changeSkin();



    }

    public float hitAnimationSpeed = 5;
    void Update()
    {
        if (gamePhase == 0) gamePhase0();
        if (gamePhase == 1) gamePhase1();
        
    }
    public void bossTextUpdater() //Updates Boss level text. Called only once in Start().
    {
        TextMeshProUGUI _bossLevelText = GameObject.Find("bossLevelText").GetComponent<TextMeshProUGUI>();
        _bossLevelText.text = "<size=100%>" + bossLevel.ToString() + "<size=60%> LEVEL";
    }
    public void UpdateLevelText() //Updates Player level text.
    {
        _playerLevelText.text = "<size=100%>" + playerLevel.ToString() + "<size=60%> LEVEL";
    }

    private float _touchBoarder;
    [HideInInspector]
    public bool isObjectStuck = false;
    private void gamePhase1() //Gameplay input handeler.
    {
        Touch _touch;
        if(!isObjectStuck)
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
            await Task.Delay(System.TimeSpan.FromSeconds(1f));
            _levelManager.enableFailCanvas();

        }

        gamePhase++;
    }

    private float buttonDelay;
    public void playWinningAnimation()
    {
        if (playerWin && buttonDelay + 1f < Time.time)
        {
            buttonDelay = Time.time;
            int randomHand = Random.Range(0, 2);
            GameObject hand = randomHand == 0 ? GameObject.Find("Boxing_Hand_Left_Hit_Anim") : GameObject.Find("Boxing_Hand_Right_Hit_Anim");
            hand.GetComponent<Animator>().SetTrigger("hit");

        }
    }

    public void setAnimationTrigger(GameObject gameObject, string triggerName)
    {
        gameObject.GetComponent<Animator>().SetTrigger(triggerName);
    }

    public Vector3 getFinishLineStanding()
    {
        return playerFinishLineStanding;
    }
    public void gamePhase0()
    {
        if(Input.touchCount > 0)
        {
            _levelManager.disableTapToStart();
            gamePhase++;
        }
    }

    private int skinIndex = 0;
    public async void changeSkin()
    {
        GameObject[] hands = GameObject.FindGameObjectsWithTag("playerHand");
        bool levelUp = false;
        levelMaterials materialToChange = _levelMaterials[0];

        for(int i = _levelMaterials.Count - 1; i>=0; i--)
        {
            if(playerLevel > _levelMaterials[i].minLevel && i != skinIndex)
            {
                materialToChange = _levelMaterials[i];
                skinIndex = i;
                levelUp = true;
                break;
            }
        }

        if (!levelUp) return;

        foreach(GameObject hand in hands)
        {
            MeshRenderer handMesh = hand.GetComponent<MeshRenderer>();
            Material[] tempMaterials = handMesh.materials;

            tempMaterials[0] = materialToChange.handMaterial;
            tempMaterials[1] = materialToChange.wristMaterial;

            hand.transform.parent.GetComponent<Animator>().SetTrigger("levelUp");

            handMesh.materials = tempMaterials;
        }

    }

}   
