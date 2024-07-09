using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public List<AudioClip> audioClips = new List<AudioClip>();
    AudioSource au;
    // Start is called before the first frame update
    void Start()
    {
        au = GetComponent<AudioSource>();
    }
    public void PlayAudio(int index)
    {
        //au.clip = ;
        au.PlayOneShot(audioClips[index]);
        au.Play();
    }
    // Update is called once per frame
    void Update()
    {

    }
}
