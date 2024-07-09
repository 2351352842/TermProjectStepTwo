using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShoot : MonoBehaviour
{
    float t = 0;
    public GameObject[] bullets = new GameObject[3];
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
        if (t > 0.15f)
        {
            t = 0;
            for (int i = 0; i < 2; i++)
            {
                GameObject bullet = Instantiate(bullets[0]);
                bullet.transform.position = transform.GetChild(i).position;
                if (i == 0)
                {
                    bullet.transform.eulerAngles = new Vector3(-90, 0, -30);
                }
                if (i == 1)
                {
                    bullet.transform.eulerAngles = new Vector3(-90, 0, 30);
                }
                Destroy(bullet.GetComponent<EnemyBulletMove>());
                bullet.AddComponent<BulletDestroy>();
                bullet.GetComponent<Rigidbody>().AddForce(transform.GetChild(i).up * 200);

            }
            for (int i = 2; i < 4; i++)
            {
                GameObject bullet = Instantiate(bullets[1]);
                print("2");
                bullet.transform.position = transform.GetChild(i).position;
            }
            for (int i = 4; i < 6; i++)
            {
                GameObject bullet = Instantiate(bullets[2]);
                bullet.transform.position = transform.GetChild(i).position;
            }
        }
    }
}
