using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletMove : MonoBehaviour
{
    public GameObject[] ex = new GameObject[2];
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider o)
    {
        if (o.gameObject.tag=="Enemy")
        {
            if (o.gameObject.name.Contains("1"))
            {
                GameObject.Find("AudioManager").GetComponent<AudioManager>().PlayAudio(0);
            }
            else
            {
                GameObject.Find("AudioManager").GetComponent<AudioManager>().PlayAudio(1);
            }
            ShowScore.score += 10;
            GameObject enemyEx= Instantiate(ex[0]);
            enemyEx.transform.position = o.gameObject.transform.position;
            Destroy(o.gameObject);
            Destroy(this.gameObject);
        }
        if (o.gameObject.tag == "EnemyBullet")
        {
            ShowScore.score += 3;
            GameObject enemyEx = Instantiate(ex[1]);
            enemyEx.transform.position = o.gameObject.transform.position;
            Destroy(o.gameObject);
            Destroy(this.gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(0,0,0.1f,Space.World);
        if (transform.position.z>7)
        {
            Destroy(this.gameObject);//超出屏幕，子弹自毁
        }
    }
}
