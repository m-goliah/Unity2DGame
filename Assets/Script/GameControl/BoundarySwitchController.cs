using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundarySwitchController : MonoBehaviour
{
    private GameObject player;
    private bool isStarted;
    public GameObject rightBoundary;
    public GameObject mainCamera;
    public GameObject virtualCamera;
    private float MoveStepH;
    private float MoveStepV;
    private Vector3 fixedCameraPos;
    private float cameraSize;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("player");
        if(transform.parent.name == "Part1")
        {
            fixedCameraPos = new Vector3(-104.1f, -3.8f, -10f);
            cameraSize = 10f;
        }
        else if(transform.parent.name == "Part2")
        {
            fixedCameraPos = new Vector3(-63.83f, -2.26f, -10f);
            cameraSize = 12.7f;
        }
        else if (transform.parent.name == "Part3")
        {
            fixedCameraPos = new Vector3(7.2f, -5.06f, -10f);
            cameraSize = 10f;
        }
        else if (transform.parent.name == "Part4")
        {
            fixedCameraPos = new Vector3(60.3f, -5.06f, -10f);
            cameraSize = 10f;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (!GameController.Instance.isDead)
        {
            if (!isStarted)
            {
                PartStart();
            }
            else
            {
                PartEnd();
            }
            CameraMove();
        }


    }
    private void PartStart()
    {
        if (transform.position.x < player.transform.position.x)
        {
            GameController.Instance.monkeyKillCount = 0;
            GameController.Instance.koopaKillCount = 0;
            GetComponent<Collider2D>().isTrigger = false;
            transform.parent.Find("RightBoundary").GetComponent<Collider2D>().isTrigger = false;
            GameController.Instance.isPartStart = true;
            isStarted = true;
            virtualCamera.SetActive(false);
        }

    }
    private void PartEnd()
    {
        if (!GameController.Instance.isPartStart && transform.position.x < player.transform.position.x)
        {
            rightBoundary.transform.GetComponent<Collider2D>().isTrigger = true;
            Destroy(gameObject);
        }
    }

    void CameraMove()
    {
        if(player.transform.position.x > transform.position.x && player.transform.position.x < rightBoundary.transform.position.x)
        {
            if(Mathf.Abs(mainCamera.transform.position.x - fixedCameraPos.x)>0.1f)
            {
                if (mainCamera.transform.position.x < fixedCameraPos.x)
                {
                    MoveStepH = 0.1f;
                }
                else
                {
                    MoveStepH = -0.1f;
                }
            }
            if (Mathf.Abs(mainCamera.transform.position.y - fixedCameraPos.y) > 0.1f)
            {
                if (mainCamera.transform.position.y < fixedCameraPos.y)
                {
                    MoveStepV = 0.1f;
                }
                else
                {
                    MoveStepV = -0.1f;
                }
            }
   
            if (mainCamera.GetComponent<Camera>().orthographicSize < cameraSize)
            {
                mainCamera.GetComponent<Camera>().orthographicSize += 0.01f;
            }
            mainCamera.transform.position = new Vector3(mainCamera.transform.position.x + MoveStepH, mainCamera.transform.position.y + MoveStepV, mainCamera.transform.position.z);
            MoveStepH = 0;
            MoveStepV = 0;
            //if (!GameController.Instance.isPartStart && player.transform.position.x < transform.Find("RightBoundary").position.x)
            if (!GameController.Instance.isPartStart)
            {
                virtualCamera.SetActive(true);
            }
        }

        
    }
}
