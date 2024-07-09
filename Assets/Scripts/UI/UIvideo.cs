using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class UIVideo : MonoBehaviour
{
    RawImage rawImage;
    VideoPlayer videoPlayer;
    // Start is called before the first frame update
    void Start()
    {
        rawImage = GetComponent<RawImage>();
        videoPlayer = GetComponent<VideoPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        rawImage.texture = videoPlayer.texture;
    }
}
