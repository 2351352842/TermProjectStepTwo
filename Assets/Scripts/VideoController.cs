using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VideoController : MonoBehaviour
{
    Button openButton;
    Button closeButton;
    RawImage rawImage;
    // Start is called before the first frame update
    void Start()
    {
        openButton = GameObject.Find("VideoPlayButton").GetComponent<Button>();
        closeButton = GameObject.Find("CloseButton").GetComponent<Button>();
        rawImage = GameObject.Find("Video").GetComponent<RawImage>();
        rawImage.transform.gameObject.SetActive(false);
        openButton.onClick.AddListener(VideoPlay);
        closeButton.onClick.AddListener(CloseVideo);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void VideoPlay()
    {
        rawImage.transform.gameObject.SetActive(true);
    }
    public void CloseVideo()
    {
        rawImage.transform.gameObject.SetActive(false);
    }
}
