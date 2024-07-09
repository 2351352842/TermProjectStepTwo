using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    public Slider hpslider;
    float hp = 100;
    public GameObject bossEx;
    public static int num = 1;
    // Start is called before the first frame update
    void Start()
    {
        hpslider.maxValue = 100 * num;
        hpslider.value = 100 * num;
        hp *= num;
    }
    private void OnTriggerEnter(Collider o)
    {
        if (o.gameObject.name.Contains("PlayerBullet"))
        {
            hp -= 5;
        }
    }
    // Update is called once per frame
    void Update()
    {
        hpslider.value = hp;
        if (hp <= 0)
        {
            Instantiate(bossEx).transform.position = transform.position;
            GameObject.Find("AudioManager").GetComponent<AudioManager>().PlayAudio(2);
            CreateEnemy.isBoss = false;
            num++;
            Destroy(this.gameObject);
        }
    }
}
