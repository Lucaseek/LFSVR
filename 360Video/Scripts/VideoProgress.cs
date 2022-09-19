using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoProgress : MonoBehaviour
{
    //Identify video's parameters and control it's progression (pause/unpause, advance/return, volume)
    private VideoPlayer videoPlayer; //Get video player

    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
    }

    
    void Update()
    {
        /*if (videoPlayer.frameCount > 0)
            Debug.Log(videoPlayer.frame + "/" + videoPlayer.frameCount);*/

    }

    //return current video frame
    public float GetVideoFrame()
    {
        return videoPlayer.frame;
    }

    //return total video frames
    public float GetVideoTotalFrames()
    {
        return videoPlayer.frameCount;
    }
    //videoPlayer.SetDirectAudioVolume(0, 0.5f); //set volume code

    public void PauseVideo()
    {
        if(!videoPlayer.isPaused)
        {
            Debug.Log("Video Paused");
            videoPlayer.Pause();
        }
        else
        {
            Debug.Log("Video Playing");
            videoPlayer.Play();
        }
    }
}
