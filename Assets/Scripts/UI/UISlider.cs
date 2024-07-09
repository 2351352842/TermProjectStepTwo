using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISlider : MonoBehaviour
{
    Slider audioSlider;
    public AudioSource au;
    public Toggle to;
    // Start is called before the first frame update
    void Start()
    {
        audioSlider = GetComponent<Slider>();
        audioSlider.minValue = 0;
        audioSlider.maxValue = 1;
        audioSlider.value = 0.6f;
        au = au.GetComponent<AudioSource>();
        to = to.GetComponent<Toggle>();
    }

    // Update is called once per frame
    void Update()
    {
        au.volume = audioSlider.value;
        au.mute = to.isOn;
    }
}
