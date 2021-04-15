using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPartController : MonoBehaviour
{
    public GameObject bossBlood;

    // Update is called once per frame
    void Update()
    {
        BossPartStart();
    }
    private void BossPartStart()
    {
        GameObject player = GameObject.FindWithTag("player");
        if(GameController.Instance.hasGotRing && GameController.Instance.hasGotRose && player.transform.position.x > transform.position.x) 
        {
            GameController.Instance.isBossPartStart = true;
            GetComponent<Collider2D>().isTrigger = false;
            bossBlood.gameObject.SetActive(true);
        }
    }
}
