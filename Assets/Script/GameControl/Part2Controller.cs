using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class Part2Controller : MonoBehaviour
{
    public GameObject rose;
    public GameObject virtualCamera;
    private GameObject player;
    public GameObject bunchRose;
    private bool isPainted;

    void Start()
    {
        player = GameObject.FindWithTag("player");
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameController.Instance.isDead)
        {
            Part2Win();
        }

    }
    private void Part2Win()
    {
        if (GameController.Instance.roseCount >= 99)
        {
            if(transform.Find("RightBoundary").position.x > player.transform.position.x)
            {
                GameController.Instance.isPartStart = false;

            }

            if (!isPainted)
            {
                ShowHeart();
                isPainted = true;
                GameController.Instance.hasGotRose = true;
            }
            
        }
    }
    private void ShowHeart()
    {

        for (float y = 1.6f,offSetY = 0f; y > -1.6f; y -= 0.14f)
        {
            float offSetX=0f;
            for (float x = -1.6f; x < 1.6f; x += 0.07f)
            {
                float a = x * x + y * y - 1;
                if (a * a * a - x * x * y * y * y <= 0.0f)
                {
                    var heartRose = Instantiate(rose, new Vector2(transform.localPosition.x + offSetX + 5f, transform.localPosition.y+5 + offSetY - 3f), Quaternion.identity,transform);
                    heartRose.GetComponent<Collider2D>().enabled = false;
                    heartRose.GetComponent<Animator>().enabled = true;
                }
                offSetX += 0.7f;
            }
            offSetY -= 0.9f;
        }
        bunchRose.SetActive(true);
    }
}
