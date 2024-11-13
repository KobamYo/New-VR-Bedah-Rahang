using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using TMPro;

public class VideoManager : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public RawImage rawImage;
    public VideoClip[] videoClips;
    public TextMeshProUGUI videoTitle;
    public string[] videoTitles;

    private int currentIndex = 0;

    void Start()
    {
        if (videoClips.Length > 0 && videoTitles.Length == videoClips.Length)
        {
            PlayVideo(currentIndex);
        }
    }

    public void NextVideo()
    {
        if (videoClips.Length > 0)
        {
            currentIndex = (currentIndex + 1) % videoClips.Length;
            PlayVideo(currentIndex);
        }
    }

    public void PreviousVideo()
    {
        if (videoClips.Length > 0)
        {
            currentIndex--;
            if (currentIndex < 0)
            {
                currentIndex = videoClips.Length - 1;
            }
            PlayVideo(currentIndex);
        }
    }

    private void PlayVideo(int index)
    {
        videoPlayer.clip = videoClips[index];
        videoPlayer.Play();
        videoTitle.text = videoTitles[index];
    }
}
