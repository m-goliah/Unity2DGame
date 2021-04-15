using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestOpen : MonoBehaviour
{
    private Animator anim;
    private bool isOpen;
    private GameObject player;
    public GameObject sword;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindWithTag("player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void Reaction()
    {
        if (!isOpen)
        {
            anim.Play("chestOpen");
            isOpen = true;
            //放出武器动画
            //Invoke("SwordOut",0.8f);
            //获取武器
            Invoke("GetSword",0.5f);
        }
    }
    private void SwordOut()
    {
        var a = Instantiate(sword, new Vector2(transform.position.x-5,transform.position.y+5), Quaternion.identity, transform);
        Destroy(a, 0.5f);
    }
    //获取武器
    private void GetSword()
    {
        Instantiate(sword, player.transform);
        player.SendMessage("GotSword");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "player")
        {
            transform.Find("Canvas").gameObject.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "player")
        {
            transform.Find("Canvas").gameObject.SetActive(false);
        }
    }
}
