using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    public float generateCD;
    public GameObject EnemyPrefab;
    // Start is called before the first frame update
    void Start()
    {
        generateCD = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        Generate();
    }
    private void Generate()
    {
        if (generateCD < 0)
        {
            Instantiate(EnemyPrefab, transform.position, Quaternion.identity);
            generateCD = 100f;
        }
        generateCD -= Time.deltaTime;
    }
}
