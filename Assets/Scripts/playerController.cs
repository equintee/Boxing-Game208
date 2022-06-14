using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public Vector3 cameraOffset;
    public float speed = 0.35f;
    public GameObject player;
    public int level = 1;
    private void Start()
    {
        cameraOffset = Camera.main.transform.localPosition;
    }

    // Update is called once per frame
    float _touchBoarder;
    void Update()
    {
        Touch _touch;
        
        if (Input.touchCount > 0)
        {
            _touch = Input.GetTouch(0);
            _touchBoarder = player.transform.position.x + _touch.deltaPosition.x * (speed * Time.deltaTime);

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
}
