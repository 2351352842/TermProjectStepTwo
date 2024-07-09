using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMove : MonoBehaviour
{
    float xspeed = 0;
    float zspeed = -0.03f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(xspeed, 0, zspeed, Space.World);
        if (transform.position.z < 3)
        {
            transform.position = new Vector3(0, 1, 3);
            zspeed = 0;
            xspeed = 0.05f;
        }
        if (transform.position.x > 3.5f)
        {
            transform.position = new Vector3(3.5f, 1, 3);
            xspeed = Random.Range(-0.03f, -0.01f);
        }
        if (transform.position.x < -3.5f)
        {
            transform.position = new Vector3(-3.5f, 1, 3);
            xspeed = Random.Range(0.01f, 0.03f);
        }
    }
}
