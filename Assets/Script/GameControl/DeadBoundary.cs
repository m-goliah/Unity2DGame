using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadBoundary : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "player")
        {
            GameController.Instance.lifeValue = 0;
            GameController.Instance.isDead = true;
        }
        Destroy(collision.gameObject);
    }
}
