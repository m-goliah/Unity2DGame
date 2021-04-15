using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickReaction : MonoBehaviour
{
    private Collider2D enemy;
    private SpriteRenderer sr;
    public Sprite sprite;
    private bool isHit;
    public GameObject rose;
    public Animator roseAnim;
    public GameObject ring;
    public AudioSource breakAudio, roseAudio, bumpAudio;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if (GameController.Instance.roseCount >= 99 && gameObject.tag == "FlyingGoldenBrick")
        {
            isHit = true;
            sr.sprite = sprite;
        }
    }
    private void Reaction()
    {
        if (gameObject.tag == "Brick")
        {
            breakAudio.Play();
            if (enemy)
            {
                enemy.SendMessage("Die");
            }
            //Debug.Log("destroy");
            sr.sprite = sprite;
            Destroy(gameObject, 0.2f);
        }
        else if (gameObject.tag == "GoldenBrick")
        {
            roseAudio.Play();
            if (!isHit)
            {
                roseAudio.Play();
                if (enemy)
                {
                    enemy.SendMessage("Die");
                }
                isHit = true;
                var a = Instantiate(rose, new Vector2(transform.position.x, transform.position.y), Quaternion.identity, transform);
                a.GetComponent<Animator>().enabled = true;
                a.GetComponent<Animator>().Play("roseUp");
                GameController.Instance.roseCount++;
                Destroy(a,0.6f);
                sr.sprite = sprite;
            }
        }
        else if (gameObject.tag == "FlyingGoldenBrick")
        {
            roseAudio.Play();
            if (GameController.Instance.roseCount < 99)
            {
                if (enemy)
                {
                    enemy.SendMessage("Die");
                }
                var a = Instantiate(rose, new Vector2(transform.position.x, transform.position.y + 1.2f), Quaternion.identity, transform);
                a.GetComponent<Animator>().enabled = true;
                a.GetComponent<Animator>().Play("roseUp");
                GameController.Instance.roseCount++;
                Destroy(a, 0.6f);
            }

        }
        else if(gameObject.tag == "BlueBrick")
        {
            bumpAudio.Play();
            if (!isHit)
            {
                Debug.Log(transform.gameObject.name.Split('_')[1].ToString());
                if (int.Parse(transform.gameObject.name.Split('_')[1].ToString()) == Part4Controller.randomOrder[GameController.Instance.blueBrickHitCount])
                {
                    //变成正确的贴图
                    sr.sprite = sprite;
                    isHit = true;
                    GameController.Instance.blueBrickHitCount++;
                }
                else
                {
                    //没有变化，可以换不同音效
                }
            }
            if (GameController.Instance.blueBrickHitCount >= 4)
            {
                //成功
                Debug.Log("Win!!!!!!!!!!!!!!!!!!!");
                GameObject ringCloud = GameObject.FindWithTag("RingCloud");
                //生成指环
                var a = Instantiate(ring, ringCloud.transform.localPosition,ringCloud.transform.localRotation,ringCloud.transform);
                //a.GetComponent<Animator>().Play("ringSwing");
            }


        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        enemy = collision;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        enemy = null;
    }
}
