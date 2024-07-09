using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISkill : MonoBehaviour
{
    public Text text;
    Image image;
    bool isCool = false;
    float t = 5;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        image.fillAmount = 0;
        text = text.GetComponent<Text>();

    }
    public void StartSkill()
    {

        if (!isCool)
        {
            image.fillAmount = 1;
            isCool = true;

        }
    }
    // Update is called once per frame
    void Update()
    {
        if (isCool)
        {
            t -= Time.deltaTime;
            if (t >= 0)
            {
                image.fillAmount = t / 5;
                text.text = ((int)t + 1).ToString();
            }
            else
            {
                image.fillAmount = 0;
                t = 5;
                isCool = false;
                text.text = "";
            }
        }
        
    }
}
