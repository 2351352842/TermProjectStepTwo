using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    Animator ani;
    // Start is called before the first frame update
    void Start()
    {
        ani = transform.GetChild(0).transform.GetComponent<Animator>();
    }

    // Update is called once per frame
    public void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            Invoke("inv", 1f);
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            ani.SetBool("IsOpen", false);
        }
    }
    public void inv()
    {
        ani.SetBool("IsOpen", true);
    }
    void Update()
    {

    }


}

