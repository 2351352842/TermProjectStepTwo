using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject bullet;
    float t = 0;
    AudioSource au;
    public Sprite[] sprites = new Sprite[3];
    SpriteRenderer spriteRenderer;
    int num = 1;
    // Start is called before the first frame update
    void Start()
    {
        au = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnTriggerEnter(Collider o)
    {
        
        if (o.gameObject.name.Contains("goods"))
        {
            if (spriteRenderer.sprite ==sprites[0])

            {
                GameObject.Find("AudioManager").GetComponent<AudioManager>().PlayAudio(5);
                spriteRenderer.sprite = sprites[1];
                num = 2;
                Destroy(o.gameObject);
                return;         
            }
        }
        
        if (o.gameObject.name.Contains("goods"))
        {
            if (spriteRenderer.sprite == sprites[1])
            {
                GameObject.Find("AudioManager").GetComponent<AudioManager>().PlayAudio(5);
                GameObject.Find("AudioManager").GetComponent<AudioManager>().PlayAudio(4);
                spriteRenderer.sprite = sprites[2];
                num = 4;
                Destroy(o.gameObject);
            }
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
        if (t > 0.15f)
        {
            au.Play();
            t = 0;
            if (num == 1)
            {
                GameObject playerBullet = Instantiate(bullet);
                playerBullet.transform.position = transform.GetChild(0).position;
            }
            if (num == 2)
            {
                for (int i = 1; i < 3; i++)
                {
                    GameObject playerBullet = Instantiate(bullet);
                    playerBullet.transform.position = transform.GetChild(i).position;
                }
            }
            if (num == 4)
            {
                for (int i = 1; i < 5; i++)
                {
                    GameObject playerBullet = Instantiate(bullet);
                    playerBullet.transform.position = transform.GetChild(i).position;
                }
            }

        }


    }
}
