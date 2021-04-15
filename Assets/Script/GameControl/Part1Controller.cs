using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Part1Controller : MonoBehaviour
{
    public GameObject virtualCamera;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("player");
    }

    // Update is called once per frame
    void Update()
    {
        Part1Win();
    }

    private void Part1Win()
    {

        if (GameController.Instance.roseCount >= 79 && transform.Find("RightBoundary").position.x > player.transform.position.x)
        {
            GameController.Instance.isPartStart = false;
        }
    }
}
