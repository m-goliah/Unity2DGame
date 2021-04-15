using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyDieEffect : MonoBehaviour
{
    public Slider bossBlood;
    Rigidbody2D rb;
    public bool isTurtle;
    private SpriteRenderer sr;
    public Sprite sprite;
    private AudioSource deadAudio;
    private int life;
    private void Start()
    {
        deadAudio = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        if(transform.tag == "Koopa")
        {
            life = 3;
        }
        else if(transform.tag == "Boss")
        {
            life = 20;
        }
    }
    private void Die()
    {
        deadAudio.Play();
        if(transform.tag == "Koopa" || transform.name == "Boss")
        {
            if(transform.name == "Boss")
            {
                if(--bossBlood.value == 0)
                {
                    Destroy(bossBlood.gameObject, 2);
                }
            }
            life--;
            if (life > 0)
            {
                return;
            }
            GameController.Instance.koopaKillCount++;
            GetComponent<MonkeyMove>().enabled = false;
        }
        else if(transform.tag == "Monkey")
        {
            GameController.Instance.monkeyKillCount++;
            GetComponent<MonkeyMove>().enabled = false;
        }
        if (isTurtle)
        {
            //Debug.Log("Turtle Die");
            GetComponent<Animator>().enabled = false;
            GetComponent<TurtleMove>().enabled = false;
            sr.sprite = sprite; 
        }
        transform.localScale = new Vector2(transform.localScale.x, -1);
        if(transform.tag == "Boss")
        {
            transform.localScale = new Vector2(transform.localScale.x, -5);
        }
        Collider2D[] colliders = GetComponents<Collider2D>();
        for(int i = 0; i < colliders.Length; i++)
        {
            colliders[i].enabled = false;
        }
        rb.velocity = new Vector2(rb.velocity.x, 5);
        Destroy(gameObject, 1);

    }
}
