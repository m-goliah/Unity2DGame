using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GoldenCloudController : MonoBehaviour
{
    private GameObject player;
    public GameObject rose;
    public int faceDirection = 1;
    public float cloudSpeed = -0.2f;
    public Transform leftBoundary;
    public Transform rightBoundary;
    private float RoseGeneTime = 2f;
    private float DistanceChangeDirectionTime = 1f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GameController.Instance.roseCount < 79)
        {
            Move();
            RoseGenerate();
            ChangeDirectionByDistance();
        }

    }

    private void ChangeDirection()
    {
        //向右faceDirection=1  左 -1
        faceDirection = -faceDirection / Mathf.Abs(faceDirection);
        transform.localScale = new Vector2(faceDirection, transform.localScale.y);
        cloudSpeed *= -1;
    }

    private void Move()
    {
        transform.position = new Vector2(transform.position.x + cloudSpeed, transform.position.y);
        if (transform.position.x < leftBoundary.position.x+1 || transform.position.x>rightBoundary.position.x-1)
        {
            ChangeDirection();
        }


    }
    private void RoseGenerate()
    {
        if (RoseGeneTime < 0f)
        {
            var a = Instantiate(rose, transform.position, Quaternion.identity);
            a.GetComponent<Rigidbody2D>().gravityScale = 1;
            RoseGeneTime = 1f;
        }
        RoseGeneTime -= Time.deltaTime;
    }

    private void ChangeDirectionByDistance()
    {
        int randomNumber = Random.Range(0, 2);
        if (DistanceChangeDirectionTime < 0)
        {
            if (Mathf.Abs(player.transform.position.x - transform.position.x) < 0.1f)
            {
                if(randomNumber == 0)
                {
                    ChangeDirection();
                }

                DistanceChangeDirectionTime = 1f;
            }
        }
        DistanceChangeDirectionTime -= Time.deltaTime;
    }
}
