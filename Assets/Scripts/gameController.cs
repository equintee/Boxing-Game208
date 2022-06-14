using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class gameController : MonoBehaviour
{
    public static int gamePhase = 0; //0 game initialization, 1 gameplay, 2 boss fight
    public float horizantalSpeed = 0.35f;
    public float verticalSpeed = 1f;
    public GameObject player;
    public static int level = 1;


    public GameObject boss;
    private int bossLevel;

    [SerializeField]
    private static TextMeshProUGUI _playerLevelText;

    public Vector3 playerFinishLineStandingPointOffset;
    private Vector3 playerFinishLineStanding;
    private void Start()
    {
        _playerLevelText = player.transform.Find("Canvas").transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>();
        UpdateLevelText();

        bossLevel = Random.Range(1, 100);
        bossTextUpdater();

        playerFinishLineStanding = boss.transform.localPosition - playerFinishLineStandingPointOffset;
        playerFinishLineStanding.y = 0.5f;
        gamePhase++;
    }

    void Update()
    {
        switch (gamePhase)
        {
            case 1:
                gamePhase1();
                break;
            case 2:
                Debug.Log("Lerp");
                gamePhase2();
                break;
        }
    }
    public void bossTextUpdater()
    {
        TextMeshProUGUI _bossLevelText = boss.transform.Find("Canvas").transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>();
        _bossLevelText.text = "<size=100%>" + bossLevel.ToString() + "<size=60%> LEVEL";
    }
    public static void UpdateLevelText()
    {
        _playerLevelText.text = "<size=100%>" + level.ToString() + "<size=60%> LEVEL";
        Debug.Log(level);
    }

    float _touchBoarder;
    private void gamePhase1()
    {
        Touch _touch;

        player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z + (verticalSpeed * Time.deltaTime));

        if (Input.touchCount > 0)
        {
            _touch = Input.GetTouch(0);
            _touchBoarder = player.transform.position.x + _touch.deltaPosition.x * (horizantalSpeed * Time.deltaTime);

            if (_touch.phase == TouchPhase.Moved)
            {
                player.transform.position = new Vector3(_touchBoarder, player.transform.position.y, player.transform.position.z);

            }

            if (player.transform.position.x < -4.5f)
            {
                player.transform.position = new Vector3(-4.5f, player.transform.position.y, player.transform.position.z);
            }
            if (player.transform.position.x > 4.5f)
            {
                player.transform.position = new Vector3(4.5f, player.transform.position.y, player.transform.position.z);
            }
        }
    }

    private float deltaTime = 0;
    private void gamePhase2()
    {
        if (deltaTime < 1) {
            deltaTime += Time.deltaTime;
            player.transform.localPosition = Vector3.Lerp(player.transform.localPosition, playerFinishLineStanding, deltaTime);
        }
    }

}   
