using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateEnemy : MonoBehaviour
{
    public GameObject[] enemys = new GameObject[3];
    float t = 0;
    float time = 0;
    public static bool isBoss = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!isBoss)
        {
            time += Time.deltaTime;
            if (time <= 10)
            {
                t += Time.deltaTime;
                if (t > 0.5f)
                {
                    t = 0;
                    GameObject enemy = Instantiate(enemys[Random.Range(0, 3)]);
                    enemy.transform.position = new Vector3(Random.Range(-4.5f, 4.5f), 1, 7);
                }
            }
            else
            {
                GameObject enemy = Instantiate(enemys[3]);
                enemy.transform.position = new Vector3(0, 1, 7);
                isBoss = true;
                time = 0;
            }
        }


    }
}
