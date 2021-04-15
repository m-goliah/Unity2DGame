using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOp : MonoBehaviour
{
    public int moveSpeed;
    public int jumpSpeed;
    private float elasticity;
    public bool isLeavingGround;
    public bool isHurt;
    private float h;
    private Rigidbody2D rb;
    private Animator anim;
    private LayerMask groundLayer;
    private CapsuleCollider2D collid;
    public PhysicsMaterial2D friction;    
    public PhysicsMaterial2D NoneFriction;
    public PhysicsMaterial2D largeFriction;
    public AudioSource jumpAudio,roseAudio,brokenAudio,turtleStepOnAudio;
    private bool hasSword;
    // Start is called before the first frame update
    void Start()
    {
        groundLayer = (1 << 8) | (1 << 9) | (1 << 10);
        elasticity = 15;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        collid = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isHurt && !GameController.Instance.isDead && !GameController.Instance.isOver)
        {
            PlayerMove();
        }

    }
    private void Update()
    {
        if (!isHurt && !GameController.Instance.isDead && !GameController.Instance.isOver)
        {
            PlayerJump();

        }
        AnimeMonitor();
        
    }
    private void PlayerMove()
    {
        h = Input.GetAxis("Horizontal");
        if (Mathf.Abs(h)>0.01)
        {
            if (h > 0)
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            }
            else
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            }
            anim.SetFloat("runSpeed", Mathf.Abs(h));
            rb.velocity = new Vector2(h * moveSpeed * Time.fixedDeltaTime, rb.velocity.y);
            
        }

        
    }

    private void PlayerJump()
    {

        Vector2 pos = transform.position;
        RaycastHit2D groundCheck = Physics2D.Raycast(pos, Vector2.down, 0.7f,groundLayer);
        Debug.DrawRay(pos, Vector2.down, Color.red);
        if (Input.GetKeyDown(KeyCode.K) && groundCheck)
        {
            //Debug.Log("12312312312313");
            jumpAudio.Play();
            isLeavingGround = true;
            anim.SetBool("isJumping", true);
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
            collid.sharedMaterial = NoneFriction;
             
        }
    }
    private void AnimeMonitor()
    {
        Vector2 pos = transform.position;
        RaycastHit2D groundCheck = Physics2D.Raycast(pos, Vector2.down, 0.8f, groundLayer);
        if (Mathf.Abs(rb.velocity.x) < 0.1f)
        {
            anim.SetFloat("runSpeed", Mathf.Abs(rb.velocity.x));
        }
        if (rb.velocity.y < 0)
        {
            anim.SetBool("isFalling", true);
            anim.SetBool("isJumping", false);
        }
        if (groundCheck)
        {
            //isLeavingGround = false;
            anim.SetBool("isFalling", false);
        }
        if (Input.GetKeyDown(KeyCode.J) && hasSword)
        {
            anim.Play("attack");
        }
        if (isHurt)
        {
            anim.Play("hurt");
            if (Mathf.Abs(rb.velocity.x) < 0.1)
            {
                //anim.SetFloat("runSpeed", Mathf.Abs(rb.velocity.x));
                anim.Play("stand");
                isHurt = false;
            }
        }
        if (GameController.Instance.isDead)
        {
            anim.Play("dead");

        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 11)
        {
            if (anim.GetBool("isFalling"))
            {
                if (collision.gameObject.tag == "Turtle")
                {
                    turtleStepOnAudio.Play();
                    if (transform.localRotation.y == 0)
                    {
                        collision.gameObject.SendMessage("TurtleReaction", true);
                    }
                    else
                    {
                        collision.gameObject.SendMessage("TurtleReaction", false);
                    }
                    rb.velocity = new Vector2(rb.velocity.x, elasticity);
                }
                else if(collision.gameObject.tag == "Monkey" || collision.gameObject.tag == "Koopa" || collision.gameObject.tag == "Boss")
                {
                    rb.velocity = new Vector2(rb.velocity.x, elasticity);
                    collision.gameObject.SendMessage("Die");
                }
            }
            else
            {
                if(collision.gameObject.tag == "Turtle")
                {
                    //if sprite is headIn , it should be state in
                    //Debug.Log(collision.gameObject.GetComponent<Rigidbody2D>().velocity.x);
                    if(collision.gameObject.GetComponent<SpriteRenderer>().sprite.name == "enemies(alpha)_30")
                    {
                        if (transform.localRotation.y == 0)
                        {
                            collision.gameObject.SendMessage("TurtleReaction", true);
                        }
                        else
                        {
                            collision.gameObject.SendMessage("TurtleReaction", false);
                        }
                        rb.velocity = new Vector2(-3, 5);
                        return;
                    }
                }
                //hurt
                isHurt = true;

                GameController.Instance.lifeValue--;
                if (GameController.Instance.lifeValue <= 0)
                {
                    GameController.Instance.isDead = true;
                    anim.Play("dead");
                    if (!GameController.Instance.isBossPartStart)
                    {

                        Destroy(gameObject);
                    }
                }
                if (transform.localRotation.y == 0)
                {
                    rb.velocity = new Vector2(7, rb.velocity.y+5);
                }
                else
                {
                    rb.velocity = new Vector2(-7, rb.velocity.y+5);
                }
            }
            
        }

        //destroy bricks
        else if (collision.gameObject.layer == 10)
        {
            if (collision.contacts[0].normal.y == -1)
            {
                collision.gameObject.SendMessage("Reaction");
            }
            
                
        }
        if ((collision.gameObject.layer == 8 || collision.gameObject.layer == 9 || collision.gameObject.layer == 10) && collision.contacts[0].normal.y == 1)
        {
            isLeavingGround = false;
            collid.sharedMaterial = friction;

        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Blade")
        {
            GameController.Instance.lifeValue--;
            //GameController.Instance.lifeValueText.text = GameController.Instance.lifeValue.ToString();
            if (GameController.Instance.lifeValue <= 0)
            {
                GameController.Instance.isDead = true;
                anim.Play("dead");
                if (!GameController.Instance.isBossPartStart)
                {

                    Destroy(gameObject);
                }
            }
            Destroy(collision.gameObject);
            if (transform.localRotation.y == 0)
            {
                rb.velocity = new Vector2(5, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(-5, rb.velocity.y);
            }
        }
        else if(collision.tag == "Rose")
        {
            roseAudio.Play();
            GameController.Instance.roseCount++;
            Destroy(collision.gameObject);
            //GameController.Instance.roseValueText.text = GameController.Instance.roseCount.ToString();
        }
        else if(collision.tag == "Heart")
        {
            GameController.Instance.lifeValue = GameController.Instance.lifeValue == 3 ? 3 : GameController.Instance.lifeValue + 1;
            Destroy(collision.gameObject);
        }
        else if (collision.tag == "Ring")
        {
            GameController.Instance.hasGotRing = true;
            Destroy(collision.gameObject);
        }
        else if(collision.tag == "End")
        {
            GameController.Instance.isWin = true;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.E) && collision.tag == "Chest")
        {
            collision.gameObject.SendMessage("Reaction");
        }
    }
    private void GotSword()
    {
        hasSword = true;
    }
}
