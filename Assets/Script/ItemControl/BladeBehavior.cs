using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeBehavior : MonoBehaviour
{
    private Rigidbody2D rb;
    public Vector2 force = new Vector2(20,20);
    private float speedV;
    [SerializeField]
    private bool isDirectLeft;
    // Start is called before the first frame update
    void Start()
    {
        if(transform.parent.tag == "Koopa" || transform.parent.tag == "Boss")
        {
            speedV = 0;
        }
        else
        {
            speedV = 10;
        }
        rb = GetComponent<Rigidbody2D>();
        DirectionDetect();
        Move();
        Invoke("BladeDestroy", 5);
    }

    // Update is called once per frame
    private void Move()
    {
        transform.SetParent(null);
        //transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        if (isDirectLeft)
        {
            force = new Vector2(-5, speedV);
        }
        else
        {
            force = new Vector2(5, speedV);
        }
        rb.velocity = force;

    }
    private void DirectionDetect()
    {
        //Debug.Log(transform.parent.tag);
        if (transform.parent.transform.localScale.x >= 0)
        {
            isDirectLeft = true;
        }
        else
        {
            isDirectLeft = false;
        }
    }
    private void BladeDestroy()
    {
        Destroy(gameObject);
    }
}
