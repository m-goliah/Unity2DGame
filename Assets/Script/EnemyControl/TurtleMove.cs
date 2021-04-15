using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurtleMove : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    public int faceDirection = 1;
    public int pushDirection = 1;
    public bool isWing;
    private Animator anim;
    private Rigidbody2D rb;
    public CircleCollider2D circleCollid;
    public Sprite[] spriteList;  //0: headInSprite    1:WingSprite
    private SpriteRenderer sr;
    private bool isOnGround = true;
    private bool isDead;
    private LayerMask groundLayer;
    public PhysicsMaterial2D largeFriction;
    public PhysicsMaterial2D NoneFriction;

    public bool isHeadIn;
    [SerializeField] private bool isHeadInMoving;

    private void Awake()
    {
        groundLayer = (1 << 8) | (1 << 9) | (1 << 10);
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        if (!isDead)
        {
            Move();
            Jump();
        }

    }
    private void Move()
    {
        if (isHeadIn && isHeadInMoving)
        {
            rb.velocity = new Vector2(-3 * faceDirection * moveSpeed * Time.fixedDeltaTime, rb.velocity.y);
        }
        else if (isHeadIn && !isHeadInMoving)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(-faceDirection * moveSpeed * Time.fixedDeltaTime, rb.velocity.y);
        }
        
    }
    private void Jump()
    {
        if (isWing)
        {
            isOnGround = GroundCheck();
            //Debug.Log(isOnGround);
            if (isOnGround)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
        }
    }
    private void ChangeDirection()
    {
        //向右faceDirection=1  左 -1
        faceDirection = -faceDirection/Mathf.Abs(faceDirection);
        transform.localScale = new Vector2(faceDirection, transform.localScale.y);
    }

    private bool GroundCheck()
    {
        Vector2 pos = transform.position;
        RaycastHit2D groundCheck = Physics2D.Raycast(pos, Vector2.down, 0.6f, groundLayer);
        Debug.DrawRay(pos, Vector2.down, Color.red);
        if (groundCheck)
        {
            return true;
        }
        return false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            if(collision.contacts[0].normal.y == 0)
            {
                ChangeDirection();
            }

            rb.velocity = new Vector2(-faceDirection * 2, rb.velocity.y);
        }
        else if (collision.gameObject.layer == 11)
        {
            if (!isHeadInMoving)
            {
                ChangeDirection();
            }

            else
            {
                collision.gameObject.SendMessage("ChangeIsDead");
                collision.gameObject.SendMessage("Die");
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            ChangeDirection();
            rb.velocity = new Vector2(-faceDirection * 2, rb.velocity.y);
        }
    }
    private void TurtleReaction(bool isToLeft)
    {
        if (isWing)
        {
            sr.sprite = spriteList[1];
            isWing = false;
            anim.SetBool("isWing", false);
            
        }
        else if (!isHeadIn && !isHeadInMoving)
        {
            GetComponent<Animator>().enabled = false;
            circleCollid.enabled = false;
            isHeadIn = true;
            sr.sprite = spriteList[0];
            GetComponents<Collider2D>()[0].sharedMaterial = largeFriction;

        }
        else if(isHeadIn && !isHeadInMoving)
        {
            GetComponents<Collider2D>()[0].sharedMaterial = NoneFriction;
            sr.sprite = spriteList[2];
            if (isToLeft) 
            {
                pushDirection = -1;
            }
            else
            {
                pushDirection = 1;
            }
            isHeadInMoving = true;
            faceDirection = -1 * pushDirection;
        }
        else if(isHeadIn && isHeadInMoving)
        {
            isHeadInMoving = false;
            GetComponents<Collider2D>()[0].sharedMaterial = largeFriction;
            sr.sprite = spriteList[0];
        }
    }
    private void ChangeIsDead()
    {
        isDead = true;
    }
}
