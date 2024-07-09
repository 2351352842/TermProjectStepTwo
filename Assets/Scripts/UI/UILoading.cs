using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UILoading : MonoBehaviour
{
    Slider slider;
    public Text text;
    float t = 0;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        slider.value = 0;
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
        if (t <= 10)
        {
            slider.value = t * 10;
            text.text = (int)slider.value + "%";
        }
        else
        {
            slider.value = 100;
            text.text = "100%";
            UnityEngine.SceneManagement.SceneManager.LoadScene(3);
        }
    }

}
