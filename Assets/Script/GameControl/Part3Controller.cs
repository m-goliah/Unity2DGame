using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Part3Controller : MonoBehaviour
{
    private GameObject player;
    public GameObject virtualCamera;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("player");
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameController.Instance.isDead)
        {
            Part3Win();
        }

    }
    private void Part3Win()
    {

        if (GameController.Instance.monkeyKillCount >= 18 && transform.Find("RightBoundary").position.x > player.transform.position.x)
        {
            GameController.Instance.isPartStart = false;
        }
    }
}
