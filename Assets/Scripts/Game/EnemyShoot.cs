using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public GameObject[] bullets = new GameObject[3];
    float t = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
        if (t > 0.3f)
        {
            t = 0;
            if (this.name.Contains("1"))
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    GameObject bullet = Instantiate(bullets[0]);
                    bullet.transform.position = transform.GetChild(i).position;
                }
            }
            if (this.name.Contains("2"))
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    GameObject bullet = Instantiate(bullets[1]);
                    bullet.transform.position = transform.GetChild(i).position;
                }
            }
            if (this.name.Contains("3"))
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    GameObject bullet = Instantiate(bullets[2]);
                    bullet.transform.position = transform.GetChild(i).position;
                }
            }

        }
    }
}
