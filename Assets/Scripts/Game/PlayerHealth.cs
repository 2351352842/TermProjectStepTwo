using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Slider hpslider;
    float hp = 100;
    public GameObject playerEx;
    public GameObject over;
    // Start is called before the first frame update
    void Start()
    {
        hpslider.maxValue = 100;
        hpslider.value = 100;
    }
    private void OnTriggerEnter(Collider o)
    {
        if (o.gameObject.tag == "EnemyBullet")
        {
            hp -= 5;
            ShowScore.score += 3;
            Destroy(o.gameObject);
        }
        if (o.gameObject.tag == "Enemy")
        {
            ShowScore.score += 10;
            hp -= 10;
            if (o.gameObject.name.Contains("1"))
            {
                GameObject.Find("AudioManager").GetComponent<AudioManager>().PlayAudio(0);
            }
            else
            {
                GameObject.Find("AudioManager").GetComponent<AudioManager>().PlayAudio(1);
            }
            Destroy(o.gameObject);
        }
        if (o.gameObject.name.Contains("Blood"))
        {
            GameObject.Find("AudioManager").GetComponent<AudioManager>().PlayAudio(6);
            hp = 100;
            Destroy(o.gameObject);

        }

    }
    // Update is called once per frame
    void Update()
    {
        hpslider.value = hp;
        if (hp <= 0)
        {
            Instantiate(playerEx).transform.position = transform.position;
            GameObject.Find("AudioManager").GetComponent<AudioManager>().PlayAudio(3);
            over.SetActive(true);
            Destroy(this.gameObject);
        }
    }
}
