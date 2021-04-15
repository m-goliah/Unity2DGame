using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkeyMove : MonoBehaviour
{
    public float moveSpeed = 100;
    public float jumpForce;
    public int faceDirection = 1;
    public int pushDirection = 1;
    private Rigidbody2D rb;
    public Sprite headInSprite;
    private SpriteRenderer sr;
    private float jumpTiming;
    private float changeDirTiming;
    public float scale;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

    }
    private void FixedUpdate()
    {
        if(transform.name != "Boss")
        {
            Move();
            RandomJump();
        }

    }
    private void Move()
    {
        //2s has a chance to change direction
        if (changeDirTiming > 2)
        {
            int randomCount = Random.Range(0, 9);
            if (randomCount > 4) 
            {
                ChangeDirection();
            }
            changeDirTiming = 0;
        }
        changeDirTiming += Time.deltaTime;
        rb.velocity = new Vector2(-faceDirection * moveSpeed * Time.deltaTime, rb.velocity.y);
        
    }
    //random jump
    private void RandomJump()
    {
        int randomTime = Random.Range(3, 7);
        //3seconds jump one time
        if (jumpTiming > randomTime)
        {
            
            rb.velocity = new Vector2(rb.velocity.x , 5);
            jumpTiming = 0;
        }
        jumpTiming += Time.deltaTime;
    }
    private void ChangeDirection()
    {
        //向右faceDirection=1  左 -1
        faceDirection = -faceDirection / Mathf.Abs(faceDirection);
        transform.localScale = new Vector2(faceDirection*scale, transform.localScale.y);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 9 || collision.gameObject.layer == 11)
        {
            ChangeDirection();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            ChangeDirection();
        }
    }

    private void MonkeyReaction()
    {
        //Destroy(gameObject);
    }
}