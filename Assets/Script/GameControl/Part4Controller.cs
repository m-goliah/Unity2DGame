using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Part4Controller : MonoBehaviour
{
    public static List<int> randomOrder;
    private GameObject player;
    public GameObject virtualCamera;
    public GameObject ringImage;
    void Start()
    {
        randomOrder = new List<int>();
        GetRandomOrder();
        player = GameObject.FindWithTag("player");
        Debug.Log(randomOrder[0]);
        Debug.Log(randomOrder[1]);
        Debug.Log(randomOrder[2]);
        Debug.Log(randomOrder[3]);
    }

    // Update is called once per frame
    void Update()
    {
        Part4Win();
    }
    private void Part4Win()
    {
        if (GameController.Instance.koopaKillCount >= 1 && GameController.Instance.hasGotRing && transform.Find("RightBoundary").position.x > player.transform.position.x)
        {
            GameController.Instance.isPartStart = false;
            GameObject.FindWithTag("Boss").gameObject.SetActive(true);
            ringImage.SetActive(true);
        }
    }
    private void GetRandomOrder()
    {
        List<int> randomList = new List<int>();
        while (true)
        {
            if (randomList.Count >= 4)
            {
                break;
            }
            int a = Random.Range(0, 100);
            if (!randomList.Contains(a))
            {
                randomList.Add(a);
            }
        }
        for (int i = 0; i < 4; i++)
        {
            int minIndex = 0;
            for (int j = 1; j < 4; j++)
            {
                if (randomList[j] < randomList[minIndex])
                {
                    minIndex = j;
                }
            }
            randomList[minIndex] = 101;
            randomOrder.Add(minIndex);

        }
    }
}
