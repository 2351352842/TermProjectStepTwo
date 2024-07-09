using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletMove : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, 0, -0.1f, Space.World);
        if (transform.position.z < -7)
        {
            Destroy(this.gameObject);
        }
    }
}
