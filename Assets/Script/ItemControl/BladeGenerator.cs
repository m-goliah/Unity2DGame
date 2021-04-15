using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeGenerator : MonoBehaviour
{
    //value: 0:hammer 1:fire 2: sord
    private int bladeType;
    private float posOffset;
    public GameObject[] blades;
    private float GenerateTime = 3f;
    // Start is called before the first frame update
    void Start()
    {
        if(gameObject.tag == "Monkey")
        {
            bladeType = 0;
            posOffset = 0.3f;
        }
        else if(gameObject.tag == "Koopa" || gameObject.tag == "Boss")
        {
            //Debug.Log(gameObject.tag);
            bladeType = 1;
            posOffset = 0.3f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Generate();
    }
    private void Generate()
    {
        int randomTime = Random.Range(2, 6);
        if (GenerateTime < 0)
        {
            Instantiate(blades[bladeType], new Vector2(transform.position.x + posOffset, transform.position.y + posOffset), Quaternion.identity,transform);
            GenerateTime = randomTime;
        }
        GenerateTime -= Time.deltaTime;
    }

}
