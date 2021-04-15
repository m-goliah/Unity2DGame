using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineOn : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator animMachine;
    public GameObject paintings;
    void Start()
    {
        animMachine = GetComponent<Animator>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "player" && Input.GetKeyDown(KeyCode.E))
        {

            animMachine.Play("machineOn");
            paintings.SetActive(true);
            paintings.GetComponent<Animator>().Play("paintOn");
        }
    }
}
