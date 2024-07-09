using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAudio : MonoBehaviour
{
    Toggle toggle;
    public AudioSource au;
    // Start is called before the first frame update
    void Start()
    {
        toggle = GetComponent<Toggle>();
        au = au.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        au.mute = toggle.isOn;
    }
}
