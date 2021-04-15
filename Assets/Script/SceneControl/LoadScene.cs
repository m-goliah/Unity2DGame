using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        transform.GetComponent<Text>().text = " X " + PlaytimesConfig.playTimes.ToString();
        Invoke("LoadMainScene", 2);
    }

    // Update is called once per frame
    void Update()
    {
        
        //transform.GetComponent<Text>().text = " X " + GameController.Instance.playTimes.ToString();
        //Debug.Log(transform.GetComponent<Text>().text);
        //Debug.Log(a);
    }
    private void LoadMainScene()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
