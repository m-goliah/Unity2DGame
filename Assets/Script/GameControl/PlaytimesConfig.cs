using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaytimesConfig : MonoBehaviour
{
    public static int playTimes;
    // Start is called before the first frame update
    void Start()
    {
        GameObject.DontDestroyOnLoad(gameObject);
        playTimes = 5;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
