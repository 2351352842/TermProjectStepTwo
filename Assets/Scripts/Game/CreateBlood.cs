using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBlood : MonoBehaviour
{
    public GameObject blood;
    float t = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
        if (t > 5f)
        {
            t = 0;
            GameObject good = Instantiate(blood);
            good.transform.position = new Vector3(Random.Range(-4.5f, 4.5f), 1, 7);
        }
    }
}
