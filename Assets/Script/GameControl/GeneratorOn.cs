using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorOn : MonoBehaviour
{
    GameObject player;
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
            if (player.transform.position.x > transform.position.x && player.transform.position.x < transform.Find("RightBoundary").position.x)
            {
                transform.Find("EnemyGenerator").gameObject.SetActive(true);
            }
            else
            {
                transform.Find("EnemyGenerator").gameObject.SetActive(false);
            }
        }

    }
}
