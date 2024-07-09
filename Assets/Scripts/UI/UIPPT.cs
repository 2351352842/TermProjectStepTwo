using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPPT : MonoBehaviour
{
    Image image;
    public List<Sprite> sprites = new List<Sprite>();//精灵图片链表
    int index = 0;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        image.sprite = sprites[0];
    }
    public void Left()
    {
        index--;
        if (index < 0)
        {
            index = sprites.Count - 1;
        }
        image.sprite = sprites[index];
    }
    public void Right()
    {
        index++;
        if (index == sprites.Count)
        {
            index = 0;
        }
        image.sprite = sprites[index];
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
