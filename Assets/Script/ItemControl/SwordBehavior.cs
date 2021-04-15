using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordBehavior : MonoBehaviour
{
    private Animator anim;
    public AudioSource attackAudio;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
    }
    private void Attack()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            attackAudio.Play();
            transform.GetComponent<Collider2D>().enabled = true;
            anim.Play("swordAttack");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 11)
        {
            GetComponent<Collider2D>().enabled = false;
            collision.gameObject.SendMessage("Die");
        }
    }
    public void TriggerOn()
    {
        GetComponent<Collider2D>().enabled = true;
    }
    public void TriggerOff()
    {
        GetComponent<Collider2D>().enabled = false;
    }
}
