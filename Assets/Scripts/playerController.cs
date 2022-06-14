using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class playerController : MonoBehaviour
{
    public Vector3 cameraOffset;
    public float horizantalSpeed = 0.35f;
    public float verticalSpeed = 1f;
    public GameObject player;
    public static int level = 100;

    [SerializeField]
    private static TextMeshProUGUI _playerLevelText;

    private void Start()
    {
        cameraOffset = Camera.main.transform.localPosition;
        _playerLevelText = player.transform.Find("Canvas").transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>();
        UpdateLevelText();
    }

    // Update is called once per frame
    float _touchBoarder;
    void Update()
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

    public static void UpdateLevelText()
    {
        _playerLevelText.text = "<size=100%>" + level.ToString() + "<size=60%> LEVEL";
        Debug.Log(level);
    }
}   
