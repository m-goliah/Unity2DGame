using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrincessBehavior : MonoBehaviour
{
    private GameObject player;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("player");
    }

    // Update is called once per frame
    void Update()
    {
        Pray();
    }
    private void Pray()
    {
        if(GameController.Instance.isBossPartStart && GameController.Instance.isDead)
        {
            GetComponent<Animator>().Play("princessPray");
            //播放特效动画
            transform.Find("MagicEffect").gameObject.SetActive(true);
            player.transform.Find("ReviveEffect").gameObject.SetActive(true);
            //回血
            Invoke("RevivePlayer",2);
        }
        else
        {
            GetComponent<Animator>().Play("princessStand");
        }
    }
    private void RevivePlayer()
    {
        GameController.Instance.lifeValue = 3;
        GameController.Instance.isDead = false;
        player.GetComponent<Animator>().Play("stand");
        transform.Find("MagicEffect").gameObject.SetActive(false);
        player.transform.Find("ReviveEffect").gameObject.SetActive(false);
    }
}
