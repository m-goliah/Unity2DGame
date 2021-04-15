using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject player;
    public Transform revivePos;
    public int lifeValue;
    public int playTimes;
    public bool isDead;
    public bool isOver;
    private static GameController instance;
    public static GameController Instance { get => instance; set => instance = value; }
    public int roseCount;
    public bool isPartStart;
    public int monkeyKillCount;
    public int koopaKillCount;
    public Image[] hearts;
    public Text roseValueText;
    public Sprite[] sprites;
    public int blueBrickHitCount;
    public int maxLifeCount;
    public bool hasGotRose;
    //public bool hasGotSuit;
    public bool hasGotRing;
    public bool isBossPartStart;
    public bool isWin;
    public GameObject button;
    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

        playTimes = PlaytimesConfig.playTimes;
        Debug.Log("playTimes  "+ playTimes);
        maxLifeCount = 3;
        lifeValue = maxLifeCount;

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(playTimes);
        if (isDead && !isBossPartStart)
        {
            //Invoke("Revive",2);   ??? swith wrong ???
            Revive();
        }
        ShowLifeImage();
        roseValueText.text = roseCount.ToString();
        if (isWin)
        {
            Invoke("Ending",2);
        }
    }

    private void Revive()
    {
        PlaytimesConfig.playTimes--;
        playTimes--;
        if (playTimes <= 0)
        {
            isOver = true;
            Debug.Log("Game Over");
            //GameOver
            button.SetActive(true);
        }
        else
        {
            //GameObject gene = Instantiate(player, revivePos.position, Quaternion.identity, transform);
            //GameObject.FindWithTag("MainCamera").GetComponent<CinemachineVirtualCamera>().Follow = gene.transform;
            isDead = false;
            lifeValue = maxLifeCount;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
    private void ShowLifeImage()
    {
        for(int i = 0; i < maxLifeCount; i++)
        {

                hearts[i].sprite = i < lifeValue ? sprites[0] : sprites[1];         
        }
    }
    private void Ending()
    {
        //switch to endScene
        Debug.Log("Wedding");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }
}


