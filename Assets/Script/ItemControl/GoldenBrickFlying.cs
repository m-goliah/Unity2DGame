using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldenBrickFlying : MonoBehaviour
{
    float deltaX;
    float deltaY;
    private float speed = 3f;
    private float direction = -1;
    float t =12;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GameController.Instance.roseCount < 99)
        {
            Move();
        }

    }
    private void Move()
    {
        if (t < 0)
        {
            direction *= -1;
            t = 13;
        }
        deltaX = Time.fixedDeltaTime * speed * direction;
        deltaY = direction * Mathf.Sin((transform.localPosition.x + Mathf.Abs(deltaX)))/10;
        //Debug.Log(deltaY);
        transform.localPosition = new Vector2(transform.localPosition.x + deltaX, transform.localPosition.y + deltaY);
        t -= Time.fixedDeltaTime;
    }
}
