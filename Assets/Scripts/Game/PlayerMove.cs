using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        transform.Translate(new Vector3(h, 0, v) * 0.05f, Space.World);
        if (transform.position.x < -4.5f)
        {
            transform.position = new Vector3(-4.5f, 1, transform.position.z);
        }
        if (transform.position.x > 4.5f)
        {
            transform.position = new Vector3(4.5f, 1, transform.position.z);
        }
        if (transform.position.z < -4)
        {
            transform.position = new Vector3(transform.position.x, 1, -4);
        }
        if (transform.position.z > 3.5f)
        {
            transform.position = new Vector3(transform.position.x, 1, 3.5f);
        }
    }
}
